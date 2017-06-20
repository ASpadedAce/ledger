using System;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;
using System.Threading;

namespace Ledger
{
    class startup
    {
        static public void id() //checks for and displays ids
        {
            //event in which file doesn't exist
            string credPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            credPath = Path.Combine(credPath, "Ledger");
            if (!File.Exists(Path.Combine(credPath, "ids.txt")))
            {
                string[] write = { "Discord Token:TOKEN", "Google Spreadsheet id:ID", "Owner Discord id:ID" };
                Directory.CreateDirectory(credPath);
                File.WriteAllLines(Path.Combine(credPath, "ids.txt"), write);
                Console.WriteLine("Please check the directory ~/Ledger for the file ids.txt and input the\nDiscord token, Owner Disocrd ID and spreadsheet id for your bot now.\n Once finnished select this panel again and hit enter");
                Console.Read();
            }
            //prints ids to console
            Console.WriteLine("Discord Token: " + creds.token());
            Console.WriteLine("Google Sheet ID: " + creds.ssid());
            Console.WriteLine("Owner Discord Id: " + creds.ownid());
        }

        static public UserCredential google()
        {
            string credPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            UserCredential credential;
            string[] Scopes = { SheetsService.Scope.Spreadsheets };      //type of access

            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                credPath = Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/sheets.googleapis.com-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }
            return credential;
        }
    }
}
