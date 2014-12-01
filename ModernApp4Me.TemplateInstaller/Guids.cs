// Guids.cs
// MUST match guids.h
using System;

namespace SmartnSoft.ModernApp4Me_TemplateInstaller
{
    static class GuidList
    {
        public const string guidModernApp4Me_TemplateInstallerPkgString = "8aecc03c-92a5-4450-bc70-dd1094c80936";
        public const string guidModernApp4Me_TemplateInstallerCmdSetString = "66fcac64-d0f1-4951-b509-82b450c2ea2b";

        public static readonly Guid guidModernApp4Me_TemplateInstallerCmdSet = new Guid(guidModernApp4Me_TemplateInstallerCmdSetString);
    };
}