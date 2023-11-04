using Microsoft.Win32;
using System.Text;

namespace pz_017
{
    internal class Program
    {
        static void Printf(object a)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine(a);
            Console.ResetColor();
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:X2}", b);
            return hex.ToString();
        }

        static void Main(string[] args)
        {
            Printf("\tos info:");
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion"))
            {
                if (key != null)
                {
                    Console.WriteLine($"os name: {key.GetValue("ProductName")}");
                    Console.WriteLine($"os version: {key.GetValue("CurrentVersion")}");
                    var a = (Byte[])key.GetValue("DigitalProductId");

                    var b = ByteArrayToString(a);

                    Console.WriteLine("reg key: {0}", b);
                }
            }

            Printf("\n\tbios info:");
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\System\BIOS"))
            {
                if (key != null)
                {
                    Console.WriteLine($"BIOS name: {key.GetValue("SystemProductName")}");
                    Console.WriteLine($"BIOS version: {key.GetValue("SystemVersion")}");
                }
            }

            Printf("\n\tautorun app:");
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run"))
            {
                if (key != null)
                {
                    string[] valueNames = key.GetValueNames();
                    foreach (string valueName in valueNames)
                    {
                        Console.WriteLine($"{valueName}: {key.GetValue(valueName)}");
                    }
                }
            }
        }
    }
}