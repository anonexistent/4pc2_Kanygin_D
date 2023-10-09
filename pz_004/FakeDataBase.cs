using System.ComponentModel.DataAnnotations;

namespace pz_004
{
    internal class FakeDataBase
    {
        [MaxLength(150)]
        public string connectionPath { get; set; }

        public FakeDataBase(string conPath = "—")
        {
            if (conPath.Length <= 150) connectionPath = conPath;
            else connectionPath = "localhost:80";
        }

        public void SaveData()
        {
            int fakeWork = 0;
            lock (this)
            {
                for (int i = 0; i < sbyte.MaxValue; i++) if (100 > 0*byte.MaxValue) fakeWork++;
                Console.WriteLine($">> data has been save");
            }
        }
    }
}
