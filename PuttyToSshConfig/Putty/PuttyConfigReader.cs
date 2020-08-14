using System;
using System.Collections.Generic;
using Microsoft.Win32;
using PuttyToSshConfig.Shared;

namespace PuttyToSshConfig.Putty
{
    public class PuttyConfigReader
    {
        private readonly RegistryKey _currentUserKey;

        public PuttyConfigReader()
        {
            _currentUserKey = Environment.Is64BitOperatingSystem
                ? RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64)
                : RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
        }

        public IEnumerable<PuttyConfigEntry> ReadConfigEntries()
        {
            var configEntries = new List<PuttyConfigEntry>();
            
            var puttySessionsKey = _currentUserKey.OpenSubKey(@"Software\SimonTatham\PuTTY\Sessions",
                RegistryKeyPermissionCheck.ReadSubTree);
            
            if (puttySessionsKey == null) throw new InvalidOperationException("PuTTY is not installed");

            var sessionSubKeyNames = puttySessionsKey.GetSubKeyNames();

            foreach (var subKeyName in sessionSubKeyNames)
            {
                var subKey = puttySessionsKey.OpenSubKey(subKeyName);
                if (subKey == null) continue;

                var addressFamily = GetRegistryKey<int>(subKey,PuttyConfigEntry.Keys.AddressFamily);
                
                var configEntry = new PuttyConfigEntry
                {
                    Name = subKeyName,
                    AddressFamily = addressFamily.ToAddressFamily(),
                    Cipher = (string)subKey.GetValue(PuttyConfigEntry.Keys.Cipher),
                    Compression = GetRegistryKeyAsBool(subKey, PuttyConfigEntry.Keys.Compression),
                    HostKey = GetRegistryKey<string>(subKey, PuttyConfigEntry.Keys.HostKey),
                    HostName = GetRegistryKey<string>(subKey, PuttyConfigEntry.Keys.HostName),
                    PublicKeyFile = GetRegistryKey<string>(subKey, PuttyConfigEntry.Keys.PublicKeyFile),
                    PortNumber = GetRegistryKey<int>(subKey, PuttyConfigEntry.Keys.PortNumber),
                    RekeyBytes = GetRegistryKey<string>(subKey, PuttyConfigEntry.Keys.RekeyBytes),
                    TcpKeepAlives = GetRegistryKeyAsBool(subKey, PuttyConfigEntry.Keys.TcpKeepAlives),
                    UserName = GetRegistryKey<string>(subKey, PuttyConfigEntry.Keys.UserName),
                    UserNameFromEnvironment = GetRegistryKeyAsBool(subKey, PuttyConfigEntry.Keys.UserNameFromEnvironment),
                    X11Display = GetRegistryKey<string>(subKey, PuttyConfigEntry.Keys.X11Display),
                    X11Forward = GetRegistryKeyAsBool(subKey, PuttyConfigEntry.Keys.X11Forward)
                };
                configEntries.Add(configEntry);
            }

            return configEntries;
        }

        private bool GetRegistryKeyAsBool(RegistryKey subKey, string name)
        {
            return GetRegistryKey<int>(subKey, name) == 1;
        }
        
        private T GetRegistryKey<T>(RegistryKey subKey, string name)
        {
            var result = subKey.GetValue(name);
            return (T)result;
        }
    }
}