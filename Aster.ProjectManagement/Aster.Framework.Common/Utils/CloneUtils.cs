using Newtonsoft.Json;

namespace Aster.Framework.Common.Utils
{
    public static class CloneUtils
    {
        public static T Clone<T>(T obj) where T : new()
        {
            if (obj == null) return default(T);

            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj));
        }
    }
}
