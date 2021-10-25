using System;
using System.IO;

namespace MeInit
{
    class Program
    {
        static void Main()
        {
            File.WriteAllText("meinit.txt", $"Me init with {Guid.NewGuid()}");
        }
    }
}
