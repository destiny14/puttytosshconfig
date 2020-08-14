namespace PuttyToSshConfig.Shared
{
    public static class Extensions
    {
        public static AddressFamily ToAddressFamily(this int i)
        {
            return (AddressFamily) i;
        }
        
        public static string ToYesNo(this bool i)
        {
            return i ? "yes" : "no";
        }
    }
}