using System.Collections.Generic;
using System.Text;
using PuttyToSshConfig.Putty;
using PuttyToSshConfig.Shared;

namespace PuttyToSshConfig.Ssh
{
    public static class SshConfigWriter
    {
        public static string WriteSshConfig(IEnumerable<PuttyConfigEntry> puttyConfigs)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine();

            foreach (var puttyConfig in puttyConfigs)
            {
                stringBuilder.AppendLine("Host " + puttyConfig.Name);
                stringBuilder.AppendLine(WriteSubEntry("AddressFamily",
                    puttyConfig.AddressFamily.ToString().ToLowerInvariant()));
                stringBuilder.AppendLine(WriteSubEntry("Compression", puttyConfig.Compression.ToYesNo()));
                stringBuilder.AppendLine(WriteSubEntry("HostName", puttyConfig.HostName));
                stringBuilder.AppendLine(WriteSubEntry("Port", puttyConfig.PortNumber.ToString()));
                var pubkeyFile = puttyConfig.PublicKeyFile;
                if (!string.IsNullOrEmpty(pubkeyFile))
                {
                    stringBuilder.AppendLine(WriteSubEntry("PubkeyAuthentication", "yes"));
                    stringBuilder.AppendLine(WriteSubEntry("IdentityFile", puttyConfig.PublicKeyFile));
                }
                stringBuilder.AppendLine(WriteSubEntry("RekeyLimit", puttyConfig.RekeyBytes));
                stringBuilder.AppendLine(WriteSubEntry("TCPKeepAlive", puttyConfig.TcpKeepAlives.ToYesNo()));
                if (!puttyConfig.UserNameFromEnvironment)
                    stringBuilder.AppendLine(WriteSubEntry("User", puttyConfig.UserName));
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }


        private static string WriteSubEntry(string key, string value)
        {
            return $"\t{key} {value}";
        }
    }
}