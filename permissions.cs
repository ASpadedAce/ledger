using System;
using System.Collections.Generic;
using Discord.Commands;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;


namespace Ledger
{
    class permissions
    {
        static public void admin(CommandService commands, UserCredential gcred, string ApplicationName)
        {
            commands.CreateCommand("admin").Parameter("discordid", ParameterType.Required).Do(async (e) =>                  //admin permissions
            {
                var user = e.User;
                string name = e.GetArg("discordid");
                DateTime localDate = DateTime.Now;
                int spcnt = 0;

                var service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = gcred,
                    ApplicationName = ApplicationName,
                });

                string range = "Users!D:D";
                SpreadsheetsResource.ValuesResource.GetRequest request =
                        service.Spreadsheets.Values.Get(creds.ssid(), range);
                ValueRange response = request.Execute();
                IList<IList<Object>> values = response.Values;

                foreach (var row in values)
                {
                    spcnt++;
                }

                spcnt++;
                if (valid.admin(user.Id.ToString(),"Ravoxan", creds.ssid(), ApplicationName, gcred) != 0)
                {
                    String range2 = "Users!D" + spcnt.ToString() + ":" + "E" + spcnt.ToString();
                    var oblist = new List<object>() { name, "=VLOOKUP(D" + spcnt.ToString() + ",A:B,2,0)"};
                    account.write(range2, oblist, gcred, ApplicationName);

                    await e.Channel.SendMessage("New Admin Registered");
                    Console.WriteLine("New Admin Registered:" + user.Id);
                }
                else
                {
                    await e.Channel.SendMessage("You're not the boss of me");
                    Console.WriteLine(user.Name + " has tried to register an admin");
                }
                spcnt = 0;
            });
        }
    }
}
