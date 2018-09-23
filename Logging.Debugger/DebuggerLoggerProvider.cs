using Microsoft.Extensions.Logging;
using System;

namespace Logging.Debugger
{
    /// <summary>
    /// ��ʾDebugger����־�ṩ��
    /// </summary> 
    public class DebuggerLoggerProvider : ILoggerProvider
    {
        /// <summary>
        /// ��־������
        /// </summary>
        private readonly Func<string, LogLevel, bool> filter;

        /// <summary>
        /// Debugger����־�ṩ��
        /// </summary>
        /// <exception cref="PlatformNotSupportedException"></exception>
        public DebuggerLoggerProvider()
            : this(null)
        {
        }

        /// <summary>
        /// Debugger����־�ṩ��
        /// </summary>
        /// <param name="filter">��־������</param>
        /// <exception cref="PlatformNotSupportedException"></exception>
        public DebuggerLoggerProvider(Func<string, LogLevel, bool> filter)
        {
            if (DebuggerLogger.IsPlatformSupported == false)
            {
                throw new PlatformNotSupportedException();
            }

            this.filter = filter;
        }

        /// <summary>
        /// ����Logger
        /// </summary>
        /// <param name="name">����</param>
        /// <exception cref="PlatformNotSupportedException"></exception>
        /// <returns></returns>
        public ILogger CreateLogger(string name)
        {
            return new DebuggerLogger(name, filter);
        }

        /// <summary>
        /// �ͷ���Դ
        /// </summary>
        public void Dispose()
        {
        }
    }
}
