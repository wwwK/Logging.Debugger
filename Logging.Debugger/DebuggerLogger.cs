using Microsoft.Extensions.Logging;
using System;
using System.Text;

namespace Logging.Debugger
{
    /// <summary>
    /// ��ʾ��װSystem.Diagnostics.Debugger.Log����־
    /// </summary>
    public class DebuggerLogger : ILogger
    {
        /// <summary>
        /// ����
        /// </summary>
        private readonly string name;

        /// <summary>
        /// ������
        /// </summary>
        private readonly Func<string, LogLevel, bool> filter;


        /// <summary>
        /// ��װSystem.Diagnostics.Debugger.Log����־
        /// </summary>
        /// <param name="name">����</param>
        public DebuggerLogger(string name)
            : this(name, null)
        {
        }

        /// <summary>
        /// ��װSystem.Diagnostics.Debugger.Log����־
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="filter">���ݹ�����</param>
        public DebuggerLogger(string name, Func<string, LogLevel, bool> filter)
        {
            this.name = string.IsNullOrEmpty(name) ? nameof(DebuggerLogger) : name;
            this.filter = filter;
        }


        /// <summary>
        /// ����һ����־��Χ
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="state">����</param>
        /// <returns></returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            return NoopDisposable.Instance;
        }

        /// <summary>
        /// ����ָ����־�����Ƿ����
        /// </summary>
        /// <param name="logLevel">��־����</param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None && (this.filter == null || this.filter(this.name, logLevel));
        }

        /// <summary>
        /// �����־
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logLevel">��־����</param>
        /// <param name="eventId">�¼�id</param>
        /// <param name="state">����</param>
        /// <param name="exception">�쳣</param>
        /// <param name="formatter">��ʽ��ί��</param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (this.IsEnabled(logLevel) == false)
            {
                return;
            }

            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            var builder = new StringBuilder()
                .Append($"{this.name}[{eventId.Id}] [{logLevel}]");

            var message = formatter(state, exception);
            if (string.IsNullOrEmpty(message) == false)
            {
                builder.AppendLine().Append(message);
            }

            if (exception != null)
            {
                builder.AppendLine().AppendLine().Append(exception.ToString());
            }

            System.Diagnostics.Debugger.Log((int)logLevel, null, builder.ToString());
        }

        private class NoopDisposable : IDisposable
        {
            public readonly static NoopDisposable Instance = new NoopDisposable();

            public void Dispose()
            {
            }
        }
    }
}
