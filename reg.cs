using System;
using System.Collections.Generic;
using Discord.Commands;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;

namespace Ledger
{
    class reg
    {
//record user data for signup
        static public void first(CommandService commands, UserCredential c, string appname)
        {
            commands.CreateCommand("first").Parameter("name", ParameterType.Required).Do(async (e) =>                  
            {
                var user = e.User;
                string name = e.GetArg("name");
                bool dumbass = false;
                int spcnt = 0;

                var service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = c,
                    ApplicationName = appname,
                });

                string range = "Users!A1:B";
                SpreadsheetsResource.ValuesResource.GetRequest request =
                        service.Spreadsheets.Values.Get(creds.ssid(), range);
                ValueRange response = request.Execute();
                IList<IList<Object>> values = response.Values;

                foreach (var row in values)
                {
                    if (row[0].ToString() == user.Id.ToString())
                        dumbass = true;
                    spcnt++;
                }

                spcnt++;
                if (dumbass == false)
                {
                    String range2 = "Users!A" + spcnt.ToString() + ':' + 'B' + spcnt.ToString();
                    var oblist = new List<object>() { user.Id.ToString(), name };
                    account.write(range2, oblist, c, appname);

                    await e.Channel.SendMessage(user.Name + " has registered with ID " + user.Id);
                    Console.WriteLine(user.Name + " has registered with ID " + user.Id);
                }
                else
                {
                    await e.Channel.SendMessage("Stop, get some help. We've been through this already");
                    Console.WriteLine(user.Name + " has tried to register again");
                }
            });
        }

//record new chracters
        static public void nw(CommandService commands, UserCredential c, string appname)
        {
            commands.CreateCommand("new").Parameter("name", ParameterType.Required).Do(async (e) =>
            {
                var user = e.User;
                string name = e.GetArg("name");
                DateTime localDate = DateTime.Now;
                bool dumbass = false;
                int lin = 0;
                int linenum = 0;

                var service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = c,
                    ApplicationName = appname,
                });

                //records user data to spreadsheet
                string range = "Characters!A1:C";
                SpreadsheetsResource.ValuesResource.GetRequest request =
                        service.Spreadsheets.Values.Get(creds.ssid(), range);

                //tests for previous chracters
                ValueRange response = request.Execute();
                IList<IList<Object>> values = response.Values;

                foreach (var row in values)
                {
                    if (row[1].ToString() == user.Id.ToString() && row[2].ToString() == name)
                        dumbass = true;
                    if (row[1].ToString() == "0")
                        lin = linenum + 1;
                    linenum++;
                }

                if (lin == 0)
                    lin = linenum + 1;

                if (dumbass == false)
                {
                    String range2 = "Characters!A" + lin.ToString() + ':' + 'I' + lin.ToString();
                    var oblist = new List<object>() { localDate.ToString(), user.Id.ToString(), name, 500, 0, 0, "=VLOOKUP((H" + lin.ToString() + "+1),XPScaling!A:B,2,1)-F" + lin.ToString(), "= VLOOKUP(F" + lin.ToString() + ",{ XPScaling!B:B,XPScaling!A:A},2,1)", "= VLOOKUP(B" + lin.ToString() + ", Users!A:B,2,0)"};
                    account.write(range2, oblist, c, appname);

                    await e.Channel.SendMessage(user.Name + " has created a new character, " + name);
                    Console.WriteLine(user.Name + " has created a new character, " + name);
                }
                else
                {
                    await e.Channel.SendMessage("Sorry, no clones allowed.");
                    Console.WriteLine(user.Name + " tried to create a clone ");
                }
            });
        }

//delete chracters
        static public void delete(CommandService commands, UserCredential c, string appname)
        {
            commands.CreateCommand("delete").Parameter("name", ParameterType.Required).Do(async (e) =>
            {
                var user = e.User;
                string name = e.GetArg("name");
                DateTime localDate = DateTime.Now;
                int lin = 0;
                
                lin = valid.charcheck(user.Id.ToString(), name, creds.ssid(), appname, c); //check character

                if (lin == 0)
                    lin = valid.admin(user.Id.ToString(), name, creds.ssid(), appname, c); //check for admin
                
                if (lin != 0)
                {
                    String range = "Characters!A" + lin.ToString() + ':' + 'I' + lin.ToString();
                    var oblist = new List<object>() { "0", "0", "0", "0", "0", "0", "0","0","0" };
                    account.write(range, oblist, c, appname);

                    await e.Channel.SendMessage(user.Name + " has deleted a character, " + name);
                    Console.WriteLine(user.Name + " has deleted a character, " + name);
                }
                else
                {
                    await e.Channel.SendMessage("Sorry a more personal method will be required :gun:");
                    Console.WriteLine(user.Name + " tried to delete a character that was not thiers to delete");
                }
            });
        }

//transferrring characters
        static public void xfer(CommandService commands, UserCredential c, string appname)
        {
            commands.CreateCommand("xfer").Parameter("name1", ParameterType.Required).Parameter("name2", ParameterType.Required).Do(async (e) =>                  //transpheres from one character to another
            {
                var user = e.User;
                string name1 = e.GetArg("name1");
                string name2 = e.GetArg("name2");
                DateTime localDate = DateTime.Now;
                int line = 0;

                line = valid.charcheck(user.Id.ToString(), name1, creds.ssid(), appname, c);
                if (line == 0)
                    line = valid.admin(user.Id.ToString(), name1, creds.ssid(), appname, c);
                name2 = account.unametoid(name2, c, appname);

                if (name2 != null && line != 0)
                {
                    String range2 = "Characters!B" + line.ToString();
                    var oblist = new List<object>() { name2 };
                    account.write(range2, oblist, c, appname);

                    await e.Channel.SendMessage("Your character has been transphered to " + name2);
                    Console.WriteLine(user.Name + "'s character has been given to " + name2);
                }
                else
                {
                    await e.Channel.SendMessage("You can't do that!");
                    Console.WriteLine(user.Name + " tried and failed a character transphere");
                }
            });        
        }

        static public void name(CommandService commands, UserCredential c, string appname)
        {
            commands.CreateCommand("name?").Do(async (e) =>                  //gives what a user's name is registered as
            {
                var user = e.User;
                string name = null;
                DateTime localDate = DateTime.Now;

                name = account.idtouname(user.Id.ToString(), c, appname);

                if (name == null)
                {
                    await e.Channel.SendMessage("I don't know you");
                    Console.WriteLine(user.Name + ", I don't know");
                }
                else
                {
                    await e.Channel.SendMessage("I know you as " + name);
                    Console.WriteLine(user.Name + " looked up " + name + "'s name");
                }
            });
        }
    }
}