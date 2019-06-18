
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aster.Framework.Common.Events
{
    public static class EventHanlerExtends
    {
        public static async Task InvokeAsync<T>(this IEnumerable<T> handlers, Func<T, Task> action, Logger logger) 
            where T : IEventHandler
        {
            if (handlers == null || handlers.Count() == 0)
                return;

            foreach (var handler in handlers)
            {
                try
                {
                    await action(handler);
                }
                catch (Exception ex)
                {
                    logger.Error($"EventHandler:{handler.GetType().FullName}执行失败", ex);
                }
            }
        }
    }
}
