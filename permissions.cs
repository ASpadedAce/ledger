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

        static public void accountant(CommandService commands, UserCredential gcred, string ApplicationName)
        {
            commands.CreateCommand("accountant").Parameter("discordid", ParameterType.Required).Do(async (e) =>                  //accountant permissions
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

                string range = "Users!F:F";
                SpreadsheetsResource.ValuesResource.GetRequest request =
                        service.Spreadsheets.Values.Get(creds.ssid(), range);
                ValueRange response = request.Execute();
                IList<IList<Object>> values = response.Values;

                foreach (var row in values)
                {
                    spcnt++;
                }

                spcnt++;
                if (valid.admin(user.Id.ToString(), "Ravoxan", creds.ssid(), ApplicationName, gcred) != 0)
                {
                    String range2 = "Users!F" + spcnt.ToString() + ":" + "G" + spcnt.ToString();
                    var oblist = new List<object>() { name, "=VLOOKUP(F" + spcnt.ToString() + ",A:B,2,0)" };
                    account.write(range2, oblist, gcred, ApplicationName);

                    await e.Channel.SendMessage("New Accountant Registered");
                    Console.WriteLine("New Accountant Registered by " + user.Id);
                }
                else
                {
                    await e.Channel.SendMessage("You're not the boss of me");
                    Console.WriteLine(user.Name + " has tried to register an accountant");
                }
                spcnt = 0;
            });
        }

        static public void trainer(CommandService commands, UserCredential gcred, string ApplicationName)
        {
            commands.CreateCommand("trainer").Parameter("discordid", ParameterType.Required).Do(async (e) =>                  //trainer permissions
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

                string range = "Users!H:H";
                SpreadsheetsResource.ValuesResource.GetRequest request =
                        service.Spreadsheets.Values.Get(creds.ssid(), range);
                ValueRange response = request.Execute();
                IList<IList<Object>> values = response.Values;

                foreach (var row in values)
                {
                    spcnt++;
                }

                spcnt++;
                if (valid.admin(user.Id.ToString(), "Ravoxan", creds.ssid(), ApplicationName, gcred) != 0)
                {
                    String range2 = "Users!H" + spcnt.ToString() + ":" + "I" + spcnt.ToString();
                    var oblist = new List<object>() { name, "=VLOOKUP(H" + spcnt.ToString() + ",A:B,2,0)" };
                    account.write(range2, oblist, gcred, ApplicationName);

                    await e.Channel.SendMessage("New Trainer Registered");
                    Console.WriteLine("New Trainer Registered by " + user.Id);
                }
                else
                {
                    await e.Channel.SendMessage("You're not the boss of me");
                    Console.WriteLine(user.Name + " has tried to register an trainer");
                }
                spcnt = 0;
            });
        }
    }
}
