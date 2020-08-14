using PuttyToSshConfig.Shared;

namespace PuttyToSshConfig.Putty
{
    public class PuttyConfigEntry
    {
        public string Name { get; set; }
        public AddressFamily AddressFamily { get; set; }
        public string Cipher { get; set; }
        public bool Compression { get; set; }
        public bool X11Forward { get; set; }
        public string X11Display { get; set; }
        public string HostKey { get; set; }
        public string HostName { get; set; }
        public int PortNumber { get; set; }
        public string PublicKeyFile { get; set; }
        public string RekeyBytes { get; set; }
        public bool TcpKeepAlives { get; set; }
        public string UserName { get; set; }
        public bool UserNameFromEnvironment { get; set; }

        public static class Keys
        {
            public static readonly string AddressFamily = "AddressFamily";
            public static readonly string Cipher = "Cipher";
            public static readonly string Compression = "Compression";
            public static readonly string X11Forward = "X11Forward";
            public static readonly string X11Display = "Display";
            public static readonly string HostKey = "HostKey";
            public static readonly string HostName = "HostName";
            public static readonly string PortNumber = "PortNumber";
            public static readonly string PublicKeyFile = "PublicKeyFile";
            public static readonly string RekeyBytes = "RekeyBytes";
            public static readonly string TcpKeepAlives = "TCPKeepAlives";
            public static readonly string UserName = "UserName";
            public static readonly string UserNameFromEnvironment = "UserNameFromEnvironment";
        }
    }
}