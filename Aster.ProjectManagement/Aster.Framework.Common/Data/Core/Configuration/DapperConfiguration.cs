using Aster.Framework.Common.Data.Core.Mapper;
using Aster.Framework.Common.Data.Core.Sql;
using Aster.Framework.Common.Data.Mapper;
using Aster.Framework.Common.Data.MySql;
using NLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Aster.Framework.Common.Data.Core.Configuration
{
    public class DapperConfiguration 
    {
        private readonly ConcurrentDictionary<Type, IClassMapper> _classMaps = new ConcurrentDictionary<Type, IClassMapper>();
        public List<Assembly> Assemblies { get; private set; }
        public Type DefaultMapper { get; private set; }
        public string DefaultConnectionStringName { get; private set; }
        public ISqlDialect Dialect { get; private set; }

        private static DapperConfiguration instance;

        

        public static DapperConfiguration Instance
        {
            get
            {
                if(instance==null)
                {
                    instance = new DapperConfiguration();
                }
                return instance;
            }
        }

        private DapperConfiguration()
        {
            AllConnectionStrings = GetAllConnectionStrings();
            Assemblies = GetEntityAssemblies();
            DefaultConnectionStringName = "DefaultConnectionString";
            DefaultMapper = typeof(AutoClassMapper<>);//TODO可以做配置
            Dialect = new MySqlDialect();//TODO可以做配置
        }


        //TODO
        private static IDictionary<string, string> GetAllConnectionStrings()
        {
            IDictionary<string, string> connectionString = new Dictionary<string, string>();


            return connectionString;
        }

        private static List<Assembly> GetEntityAssemblies()
        {
            var dllFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Directory.GetFiles(dllFolder, "Aster.*.dll")
                 .SelectMany(x => Assembly.LoadFrom(x).GetTypes())
                 .Where(x => typeof(IEntity).IsAssignableFrom(x))
                 .Select(x => x.Assembly)
                 .Distinct().ToList();
        }


        public IDictionary<string, string> AllConnectionStrings { get; private set; }

        public IClassMapper GetMap(Type entityType)
        {
            if (_classMaps.TryGetValue(entityType, out IClassMapper map)) return map;
            Type mapType = GetMapType(entityType) ?? DefaultMapper.MakeGenericType(entityType);

            map = Activator.CreateInstance(mapType, new[] { LogManager.GetLogger(entityType.Name) }) as IClassMapper;
            _classMaps[entityType] = map;

            return map;
        }

        public IClassMapper GetMap<T>() where T : class
        {
            return GetMap(typeof(T));
        }

        public Guid GetNextGuid()
        {
            byte[] b = Guid.NewGuid().ToByteArray();
            DateTime dateTime = new DateTime(1900, 1, 1);
            DateTime now = DateTime.Now;
            TimeSpan timeSpan = new TimeSpan(now.Ticks - dateTime.Ticks);
            TimeSpan timeOfDay = now.TimeOfDay;
            byte[] bytes1 = BitConverter.GetBytes(timeSpan.Days);
            byte[] bytes2 = BitConverter.GetBytes((long)(timeOfDay.TotalMilliseconds / 3.333333));
            Array.Reverse(bytes1);
            Array.Reverse(bytes2);
            Array.Copy(bytes1, bytes1.Length - 2, b, b.Length - 6, 2);
            Array.Copy(bytes2, bytes2.Length - 4, b, b.Length - 4, 4);
            return new Guid(b);
        }

        protected virtual Type GetMapType(Type entityType)
        {
            Type getType(Assembly a)
            {
                Type[] types = a.GetTypes();
                return (from type in types
                        let interfaceType = type.GetInterface(typeof(IClassMapper<>).FullName)
                        where
                            interfaceType != null &&
                            interfaceType.GetGenericArguments()[0] == entityType
                        select type).SingleOrDefault();
            }

            Type result = getType(entityType.Assembly);
            if (result != null)
            {
                return result;
            }

            return null;

        }
    }
}
