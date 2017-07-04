// The MIT License (MIT)
//
// Copyright (c) 2017 Smart&Soft
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace ModernApp4Me.Core.Log
{

    /// <summary>
    /// Just in order to have various loggers.
    /// </summary>
    /// 
    /// <author>Ludovic Roland</author>
    /// <since>2014.03.21</since>
    public abstract class M4MLogger
    {

        public enum M4MLogLevel
        {
            Debug = 0, Info = 1, Warn = 2, Error = 3
        }

        public M4MLogLevel LogLevel { get; set; }

        public abstract void WriteTrace(string log);

        public void Debug(string message, [CallerMemberName] string callerMemberName = "",
                          [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            WriteTrace(BuildLog(M4MLogLevel.Debug, message, callerMemberName, callerFilePath, callerLineNumber));
        }

        public void Info(string message, [CallerMemberName] string callerMemberName = "",
                         [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            WriteTrace(BuildLog(M4MLogLevel.Debug, message, callerMemberName, callerFilePath, callerLineNumber));
        }

        public void Warn(string message, [CallerMemberName] string callerMemberName = "",
                         [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            WriteTrace(BuildLog(M4MLogLevel.Debug, message, callerMemberName, callerFilePath, callerLineNumber));
        }

        public void Warn(string message, Exception exception, [CallerMemberName] string callerMemberName = "",
                         [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            WriteTrace(BuildLog(M4MLogLevel.Debug, message, exception.Message, exception.StackTrace, callerMemberName,
                                callerFilePath, callerLineNumber));
        }

        public void Error(string message, [CallerMemberName] string callerMemberName = "",
                          [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            WriteTrace(BuildLog(M4MLogLevel.Debug, message, callerMemberName, callerFilePath, callerLineNumber));
        }

        public void Error(string message, Exception exception, [CallerMemberName] string callerMemberName = "",
                          [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            WriteTrace(BuildLog(M4MLogLevel.Debug, message, exception.Message, exception.StackTrace, callerMemberName,
                                callerFilePath, callerLineNumber));
        }

        public void Fatal(string message, [CallerMemberName] string callerMemberName = "",
                          [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            WriteTrace(BuildLog(M4MLogLevel.Debug, message, callerMemberName, callerFilePath, callerLineNumber));
        }

        public void Fatal(string message, Exception exception, [CallerMemberName] string callerMemberName = "",
                          [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            WriteTrace(BuildLog(M4MLogLevel.Debug, message, exception.Message, exception.StackTrace, callerMemberName,
                                callerFilePath, callerLineNumber));
        }

        public bool IsDebugEnabled()
        {
            return LogLevel <= M4MLogLevel.Debug;
        }

        public bool IsInfoEnabled()
        {
            return LogLevel <= M4MLogLevel.Info;
        }

        public bool IsWarnEnabled()
        {
            return LogLevel <= M4MLogLevel.Warn;
        }

        public bool IsErrorEnabled()
        {
            return LogLevel <= M4MLogLevel.Error;
        }

        public bool IsFatalEnabled()
        {
            return LogLevel <= M4MLogLevel.Error;
        }

        private StringBuilder BuildLogInternal(M4MLogLevel logLevel, string message, string callerMemberName, string callerFilePath, int callerLineNumber)
        {
            var header = new StringBuilder("\n");

            switch (logLevel)
            {
                case M4MLogLevel.Debug:
                    header.Append("[DEBUG] ");
                    break;

                case M4MLogLevel.Info:
                    header.Append("[INFO] ");
                    break;

                case M4MLogLevel.Warn:
                    header.Append("[WARN] ");
                    break;

                case M4MLogLevel.Error:
                    header.Append("[ERROR] ");
                    break;
            }

            header.Append(DateTime.Now.ToString("HH:mm:ss"));

            if (callerFilePath.Equals("") == false && callerMemberName.Equals("") == false)
            {
                header.Append("\n");
                header.Append("File : '").Append(callerFilePath).Append("'\n");
                header.Append("Method : '").Append(callerMemberName).Append("'");

                if (callerLineNumber != -1)
                {
                    header.Append(" at line '").Append(callerLineNumber).Append("'");
                }

                header.Append("\n" + message + "\n");
            }

            return header;
        }

        protected string BuildLog(M4MLogLevel logLevel, string message, string callerMemberName, string callerFilePath, int callerLineNumber)
        {
            return BuildLogInternal(logLevel, message, callerMemberName, callerFilePath, callerLineNumber).ToString();
        }

        protected string BuildLog(M4MLogLevel logLevel, string message, string exceptionMessage, string stackTrace, string callerMemberName, string callerFilePath, int callerLineNumber)
        {
            var header = BuildLogInternal(logLevel, message, callerMemberName, callerFilePath, callerLineNumber);
            header.Append("\n" + exceptionMessage).Append("\n" + stackTrace);

            return header.ToString();
        }

    }

}
