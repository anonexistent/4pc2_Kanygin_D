using System.Diagnostics;

namespace pz_003
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string inMod = Console.ReadLine();
                ProcessModule mod = GetModule(inMod);

                if(mod != null)
                {
                    Console.WriteLine(mod.BaseAddress);
                    Console.WriteLine(mod.ModuleName);
                    Console.WriteLine(mod.FileName);
                }
                else Console.WriteLine("not found");
            }

        }

        private static ProcessModule GetModule(string? inMod)
        {
            ProcessModuleCollection ms = Process.GetCurrentProcess().Modules;
            // descriptor
            if(int.TryParse(inMod, out var module))
            {
                foreach (ProcessModule item in ms)
                {
                    if (item.BaseAddress.ToInt32() == module) return item;
                }
            }
            // short name
            foreach (ProcessModule item in ms)
            {
                if (item.ModuleName.Equals(inMod, StringComparison.OrdinalIgnoreCase)) return item;
            }
            foreach (ProcessModule item in ms)
            {
                if(item.FileName.Equals(inMod, StringComparison.OrdinalIgnoreCase)) return item;
            }

            return null;
        }
    }
}