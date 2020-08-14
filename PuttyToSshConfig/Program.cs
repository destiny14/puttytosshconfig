using System;
using System.IO;
using System.Linq;
using PuttyToSshConfig.Putty;
using PuttyToSshConfig.Ssh;

namespace PuttyToSshConfig
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("PuTTY to ssh_config converter v1");
            Console.WriteLine("# Reading putty profiles...");
            try
            {
                var puttyReader = new PuttyConfigReader();
                var puttyConfigs = puttyReader.ReadConfigEntries().ToList();
                Console.WriteLine($@"-> found {puttyConfigs.Count()} configs");
                Console.WriteLine("# Writing ssh_config...");
                var sshConfig = SshConfigWriter.WriteSshConfig(puttyConfigs);
                File.WriteAllText("config", sshConfig);
            }
            catch (Exception exception)
            {
                Console.WriteLine($@"FATAL {exception}\n{exception.Message}\n{exception.StackTrace}");
            }
        }
    }
}