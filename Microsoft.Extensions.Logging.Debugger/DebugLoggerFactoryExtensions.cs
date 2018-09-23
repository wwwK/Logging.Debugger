using Microsoft.Extensions.Logging.Debugger;
using System;

namespace Microsoft.Extensions.Logging
{
    /// <summary>
    /// �ṩ��LoggerFactory����չ
    /// </summary>
    public static class DebugLoggerFactoryExtensions
    {
        /// <summary>
        /// ���Debugger��־�ṩ�ߵ�LoggerFactory
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static ILoggerFactory AddDebugger(this ILoggerFactory factory)
        {
            return factory.AddDebugger(LogLevel.Trace);
        }


        /// <summary>
        /// ���Debugger��־�ṩ�ߵ�LoggerFactory
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="minLevel">��־��С����</param>
        /// <returns></returns>
        public static ILoggerFactory AddDebugger(this ILoggerFactory factory, LogLevel minLevel)
        {
            return factory.AddDebugger((message, logLevel) => logLevel >= minLevel);
        }

        /// <summary>
        /// ���Debugger��־�ṩ�ߵ�LoggerFactory
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static ILoggerFactory AddDebugger(this ILoggerFactory factory, Func<string, LogLevel, bool> filter)
        {
            factory.AddProvider(new DebuggerLoggerProvider(filter));
            return factory;
        }
    }
}