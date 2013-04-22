using System;
using System.Text;
using ModernApp4Me_WinRT.SnSMetaData;

namespace ModernApp4Me_WinRT.SnSLog
{
    /// <summary>
    /// A task which enables to send the logs of the application, along with the device details.
    /// </summary>
    public sealed class SnSSendLogTask
    {
        /*******************************************************/
        /** ATTRIBUTES.
        /*******************************************************/
        private readonly string _message;
        private readonly string _recipient;
        private readonly string _subject;


        /*******************************************************/
        /** METHODS.
        /*******************************************************/
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="recipient"></param>
        /// <param name="subject"></param>
        /// <param name="?"></param>
        public SnSSendLogTask(string message, string recipient, string subject)
        {
            _message = message;
            _recipient = recipient;
            _subject = subject;
        }

        /// <summary>
        /// Sends a log message by e-mail.
        /// </summary>
        public async void SendLog()
        {
            //message composition
            var message = new StringBuilder();
            message.Append("Device model: ").Append(SnSDeviceInfo.Manufacturer).Append(" ").Append(SnSDeviceInfo.Name).Append("\n");
            message.Append("Firmware version: ").Append(SnSDeviceInfo.FirmwareVersion).Append("\n");
            message.Append("----------").Append("\n").Append(_message);

            //display the tasker
            var mailto = new Uri("mailto:?to=" + _recipient + "&subject=" + _subject + "&body=" + message);
            await Windows.System.Launcher.LaunchUriAsync(mailto);
        }
    }
}
