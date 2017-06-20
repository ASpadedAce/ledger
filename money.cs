using Discord.Commands;
using System;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using System.Collections.Generic;


namespace Ledger
{
    class money
    {

//check money in account
        static public void bank(CommandService commands, UserCredential gcred, string ApplicationName)
        {
            commands.CreateCommand("bank").Parameter("name", ParameterType.Required).Do(async (e) =>                  //prints out banking information
            {
                var user = e.User;
                string name = e.GetArg("name");
                string ammount = account.bank(name, user.Id.ToString(), gcred, ApplicationName);

                if ((valid.admin(user.Id.ToString(),name, creds.ssid(), ApplicationName, gcred) == 0 && valid.charcheck(user.Id.ToString(),name, creds.ssid(), ApplicationName, gcred) == 0) || ammount == null)
                {
                    await e.Channel.SendMessage("This is either not your character or this character does not exist.");
                    Console.WriteLine(user.Name + " has tried to look up the balance of " + name + " and failed");
                }
                else
                {
                    await e.Channel.SendMessage(ammount + " gold");
                    Console.WriteLine(user.Name + " looked up " + name + "'s balance");
                }
            });
        }

//Withdrawl money
        static public void withdrawal(CommandService commands, UserCredential gcred, string ApplicationName)
        {
            commands.CreateCommand("withdrawal").Parameter("name", ParameterType.Required).Parameter("ammount", ParameterType.Required).Do(async (e) =>                  //allows for a user to spend a certain ammount of money
            {
                var user = e.User;
                string name = e.GetArg("name");
                string ammount = e.GetArg("ammount");
                DateTime localDate = DateTime.Now;
                int line = 0;

                line = valid.charcheck(user.Id.ToString(), name, creds.ssid(), ApplicationName, gcred);
                if (line == 0)
                    line = valid.admin(user.Id.ToString(), name, creds.ssid(), ApplicationName, gcred);

                if (line != 0 && valid.dectest(ammount))
                {
                    String range2 = "Characters!D" + line.ToString();  // update next row
                    var oblist = new List<object>() { "= " + account.bank(name, user.Id.ToString(), gcred, ApplicationName) + " - " + ammount};
                    account.write(range2, oblist, gcred, ApplicationName);

                    await e.Channel.SendMessage("Your transaction for " + name + " has been processed (" + account.bank(name, user.Id.ToString(), gcred, ApplicationName) + ")");
                    Console.WriteLine(user.Name + "'s transaction for " + name + " has been processed (" + account.bank(name, user.Id.ToString(), gcred, ApplicationName) + ")");
                }
                else
                {
                    await e.Channel.SendMessage("What are you? A rogue?");
                    Console.WriteLine(user.Name + " tried to rip someone off");
                }
            });
        }

//set the ammount in someone's account
        static public void decree(CommandService commands, UserCredential gcred, string ApplicationName)
        {
            commands.CreateCommand("decree").Parameter("name", ParameterType.Required).Parameter("ammount", ParameterType.Required).Do(async (e) =>                  //sets gold ina user's account
            {
                var user = e.User;
                string name = e.GetArg("name");
                string ammount = e.GetArg("ammount");
                DateTime localDate = DateTime.Now;
                int line = 0;

                line = valid.admin(user.Id.ToString(), name, creds.ssid(), ApplicationName, gcred);

                if (line != 0 && valid.dectest(ammount))
                {
                    String range2 = "Characters!D" + line.ToString();
                    var oblist = new List<object>() { ammount };
                    account.write(range2, oblist, gcred, ApplicationName);

                    await e.Channel.SendMessage(name + "'s bank has been set to " + ammount + " gold");
                    Console.WriteLine(user.Name + " has set " + name + "'s account to " + ammount + " gold");
                }
                else
                {
                    await e.Channel.SendMessage("Trying to get rich. . . Respectable.");
                    Console.WriteLine(user.Name + " tried to set someone's money");
                }
            });
        }

//pays everyone thier salaries
        static public void payday(CommandService commands, UserCredential gcred, string ApplicationName)
        {
             commands.CreateCommand("payday").Do(async (e) =>                  //updates dayly payout
            {
                var user = e.User;
                DateTime localDate = DateTime.Now;
                int spcnt = 0;

                if (valid.admin(user.Id.ToString(), "Ravoxan", creds.ssid(), ApplicationName, gcred) != 0)
                {
                    var service = new SheetsService(new BaseClientService.Initializer()
                    {
                        HttpClientInitializer = gcred,
                        ApplicationName = ApplicationName,
                    });

                    string range = "Characters!A1:E";
                    SpreadsheetsResource.ValuesResource.GetRequest request =
                            service.Spreadsheets.Values.Get(creds.ssid(), range);
                    ValueRange response = request.Execute();
                    IList<IList<Object>> values = response.Values;

                    foreach (var row in values)
                    {
                        String range2 = "Characters!D" + (spcnt + 1).ToString();  
                        var oblist = new List<object>() { "= " + row[3].ToString() + " + " + row[4].ToString() };
                        account.write(range2, oblist, gcred, ApplicationName);
                        Console.WriteLine(row[0] + " has been payed");
                        spcnt++;
                    }

                    await e.Channel.SendMessage("Payments Distributed");
                    Console.WriteLine("Payout Finished");
                }
                else
                {
                    await e.Channel.SendMessage("Nice Try");
                    Console.WriteLine(user.Name + " tried to do a payout but failed");
                }
            });
        }

//iterates and ammount to or from everones' accounts
        static public void classaction(CommandService commands, UserCredential gcred, string ApplicationName)
        {
            commands.CreateCommand("classaction").Parameter("type", ParameterType.Required).Parameter("ammount", ParameterType.Required).Do(async (e) =>                  //adds an ammount to all users
            {
                var user = e.User;
                DateTime localDate = DateTime.Now;
                string ammount = e.GetArg("ammount");
                string type = e.GetArg("type");
                int spcnt = 0;
                int t = 0;

                if (type == "add" || type == "Add" || type == "ADD")
                    t = 1;
                if (type == "sub" || type == "Sub" || type == "SUB")
                    t = 2;

                if (valid.admin(user.Id.ToString(), "Ravoxan", creds.ssid(), ApplicationName, gcred) != 0 && valid.dectest(ammount) && t != 0)
                {
                    if (t == 2)
                        type = " - ";
                    if (t == 1)
                        type = " + ";

                    var service = new SheetsService(new BaseClientService.Initializer()
                    {
                        HttpClientInitializer = gcred,
                        ApplicationName = ApplicationName,
                    });

                    string range = "Characters!A1:E";
                    SpreadsheetsResource.ValuesResource.GetRequest request =
                            service.Spreadsheets.Values.Get(creds.ssid(), range);

                    ValueRange response = request.Execute();
                    IList<IList<Object>> values = response.Values;

                    foreach (var row in values)
                    {
                        String range2 = "Characters!D" + (spcnt + 1).ToString();
                        ValueRange valueRange = new ValueRange();
                        var oblist = new List<object>() { "= " + row[3].ToString() + type + e.GetArg("ammount") };
                        account.write(range2, oblist, gcred, ApplicationName);
                        spcnt++;
                    }

                    await e.Channel.SendMessage(type + e.GetArg("ammount") + " to everyone's account");
                    Console.WriteLine(user.Name + type + e.GetArg("ammount") + " to everyone's account");
                    spcnt++;
                }
                else
                {
                    await e.Channel.SendMessage("Nope");
                    Console.WriteLine(user.Name + type + e.GetArg("ammount") + " to everyone's accounts");
                }
            });
        }

//adds or subs froma  single account
        static public void teller(CommandService commands, UserCredential gcred, string ApplicationName)
        {
            commands.CreateCommand("teller").Parameter("type", ParameterType.Required).Parameter("name", ParameterType.Required).Parameter("ammount", ParameterType.Required).Do(async (e) =>                  //adds to single user
            {
                var user = e.User;
                DateTime localDate = DateTime.Now;
                int lnnum = 0;
                string name = e.GetArg("name");
                string ammount = e.GetArg("ammount");
                string type = e.GetArg("type");
                int t = 0;

                lnnum = valid.admin(user.Id.ToString(), name, creds.ssid(), ApplicationName, gcred);

                if (type == "add" || type == "Add" || type == "ADD")
                    t = 1;
                if (type == "sub" || type == "Sub" || type == "SUB")
                    t = 2;

                if (lnnum !=0 && valid.dectest(ammount))
                {
                    if (t == 2)
                        type = " - ";
                    if (t == 1)
                        type = " + ";


                    String range2 = "Characters!D" + lnnum.ToString();
                    ValueRange valueRange = new ValueRange();
                    var oblist = new List<object>() { "= " + account.bank(name, user.Id.ToString(), gcred, ApplicationName) + type + e.GetArg("ammount") };
                    account.write(range2, oblist, gcred, ApplicationName);

                    await e.Channel.SendMessage(type + e.GetArg("ammount") + " to " + name + "'s account");
                    Console.WriteLine(user.Name + type + e.GetArg("ammount") + " to " + name + "'s account");
                }
                else
                {
                    await e.Channel.SendMessage("Really? REALLY?!");
                    Console.WriteLine(user.Name + type + e.GetArg("ammount") + " to everyone's accounts");
                }
            });
        }

        static public void give(CommandService commands, UserCredential gcred, string ApplicationName)
        {
            commands.CreateCommand("give").Parameter("from", ParameterType.Required).Parameter("to", ParameterType.Required).Parameter("name", ParameterType.Required).Parameter("amount", ParameterType.Required).Do(async (e) =>                  //adds to single user
            {
                var user = e.User;
                DateTime localDate = DateTime.Now;
                int lnnum = 0;
                string from = e.GetArg("from");
                string to = e.GetArg("to");
                int t = 0;

                lnnum = valid.charcheck(user.Id.ToString(), from, creds.ssid(), ApplicationName, gcred);
                if (lnnum == 0)
                    lnnum = valid.admin(user.Id.ToString(), from, creds.ssid(), ApplicationName, gcred);

                t = valid.charcheck(account.unametoid(e.GetArg("name"), gcred,ApplicationName), to, creds.ssid(), ApplicationName, gcred);

                if (t != 0 && lnnum != 0 && valid.dectest(e.GetArg("amount")))
                {
                    Console.WriteLine("test");
                    String range2 = "Characters!D" + lnnum.ToString();
                    var oblist = new List<object>() {"= " + account.bank(from, user.Id.ToString(), gcred, ApplicationName) + " - " + e.GetArg("amount") };
                    account.write(range2, oblist, gcred, ApplicationName);

                    Console.WriteLine("test");

                    range2 = "Characters!D" + t.ToString();
                    oblist = new List<object>() { "= " + account.bank(to, account.unametoid(e.GetArg("name"), gcred, ApplicationName), gcred, ApplicationName) + " + " + e.GetArg("amount") };
                    account.write(range2, oblist, gcred, ApplicationName);

                    Console.WriteLine("test");

                    await e.Channel.SendMessage(e.GetArg("amount") + " has been given to " + to + " by " + from);
                    Console.WriteLine(e.GetArg("amount") + " has been given to " + to + " by " + from);
                }
                else
                {
                    await e.Channel.SendMessage("Trying to steal money now?");
                    Console.WriteLine(user.Name + " tried to steal " +from);
                }
            });
        }
    }
}
