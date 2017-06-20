using System;
using System.IO;

namespace Ledger
{
    class creds
    {
       static public string token() //returns discord token
        {
            string credPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            credPath = Path.Combine(credPath, "Ledger");
            string[] lines = File.ReadAllLines(Path.Combine(credPath, "ids.txt"));
            int colpos = lines[0].IndexOf(':');
            return lines[0].Substring(colpos + 1);
        }

        static public string ssid() //returns spreadsheet id
        {
            string credPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            credPath = Path.Combine(credPath, "Ledger");
            string[] lines = File.ReadAllLines(Path.Combine(credPath, "ids.txt"));
            int colpos = lines[1].IndexOf(':');
            return lines[1].Substring(colpos + 1);
        }

        static public string ownid() //returns the owner's id
        {
            string credPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            credPath = Path.Combine(credPath, "Ledger");
            string[] lines = File.ReadAllLines(Path.Combine(credPath, "ids.txt"));
            int colpos = lines[2].IndexOf(':');
            return lines[2].Substring(colpos + 1);
        }
    }
}
