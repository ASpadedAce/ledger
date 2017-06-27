using System;
using System.Collections.Generic;
using Google.Apis.Auth.OAuth2;
using Discord.Commands;

namespace Ledger
{
    class xp
    {

        //Set's a user's XP to a value
        static public void setxp(CommandService commands, UserCredential gcred, string ApplicationName)
        {
            commands.CreateCommand("setxp").Parameter("name", ParameterType.Required).Parameter("ammount", ParameterType.Required).Do(async (e) =>                  //sets exp
            {
                var user = e.User;
                string name = e.GetArg("name");
                string ammount = e.GetArg("ammount");
                int line = 0;

                line = valid.admin(user.Id.ToString(), name, creds.ssid(), ApplicationName, gcred);
                if (line == 0)
                    line = valid.trainer(user.Id.ToString(), name, creds.ssid(), ApplicationName, gcred);

                if (line != 0 && valid.dectest(ammount))
                {
                    String range2 = "Characters!F" + line.ToString();
                    var oblist = new List<object>() { ammount };
                    account.write(range2, oblist, gcred, ApplicationName);

                    await e.Channel.SendMessage(name + "'s xp has been set to " + ammount);
                    Console.WriteLine(user.Name + " has set " + name + "'s XP to " + ammount);
                }
                else
                {
                    await e.Channel.SendMessage("Trying to get those **GAINS**. . . Respectable.");
                    Console.WriteLine(user.Name + " tried to set someone's xp");
                }
            });
        }

        //Return's a user's xp value
        static public void xpq(CommandService commands, UserCredential gcred, string ApplicationName)
        {
            commands.CreateCommand("xp?").Parameter("name", ParameterType.Required).Do(async (e) =>                  //prints out banking information
            {
                var user = e.User;
                string name = e.GetArg("name");
                string ammount = null;

                ammount = account.xp(name, gcred, ApplicationName);

                if ((valid.charcheck(user.Id.ToString(), name, creds.ssid(), ApplicationName, gcred) == 0 && (valid.admin(user.Id.ToString(), name, creds.ssid(), ApplicationName, gcred) == 0 && valid.trainer(user.Id.ToString(), name, creds.ssid(), ApplicationName, gcred) == 0) || ammount == null))
                {
                    await e.Channel.SendMessage("This is either not your character or this character does not exist.");
                    Console.WriteLine(user.Name + " has tried to look up the XP of " + name + " and failed");
                }
                else
                {
                    await e.Channel.SendMessage(ammount + " XP");
                    Console.WriteLine(user.Name + " looked up " + name + "'s XP");
                }
            });
        }

        //Return's a user's level value
        static public void level(CommandService commands, UserCredential gcred, string ApplicationName)
        {
            commands.CreateCommand("level?").Parameter("name", ParameterType.Required).Do(async (e) =>                  //prints out banking information
            {
                var user = e.User;
                string name = e.GetArg("name");
                string ammount = null;

                ammount = account.level(name, gcred, ApplicationName);

                if ((valid.charcheck(user.Id.ToString(), name, creds.ssid(), ApplicationName, gcred) == 0 && (valid.admin(user.Id.ToString(), name, creds.ssid(), ApplicationName, gcred) == 0 && valid.trainer(user.Id.ToString(), name, creds.ssid(), ApplicationName, gcred) == 0) || ammount == null))
                {
                    await e.Channel.SendMessage("This is either not your character or this character does not exist.");
                    Console.WriteLine(user.Name + " has tried to look up the level of " + name + " and failed");
                }
                else
                {
                    await e.Channel.SendMessage(name + " is level " + ammount);
                    Console.WriteLine(user.Name + " looked up " + name + "'s Level");
                }
            });
        }

        //Return's a user's next level value
        static public void next(CommandService commands, UserCredential gcred, string ApplicationName)
        {
            commands.CreateCommand("next?").Parameter("name", ParameterType.Required).Do(async (e) =>                  //prints out banking information
            {
                var user = e.User;
                string name = e.GetArg("name");
                string ammount = null;

                ammount = account.next(name, gcred, ApplicationName);

                if ((valid.charcheck(user.Id.ToString(), name, creds.ssid(), ApplicationName, gcred) == 0 && (valid.admin(user.Id.ToString(), name, creds.ssid(), ApplicationName, gcred) == 0 && valid.trainer(user.Id.ToString(), name, creds.ssid(), ApplicationName, gcred) == 0) || ammount == null))
                {
                    await e.Channel.SendMessage("This is either not your character or this character does not exist.");
                    Console.WriteLine(user.Name + " has tried to look up the next of " + name + " and failed");
                }
                else
                {
                    await e.Channel.SendMessage(name + " is next " + ammount);
                    Console.WriteLine(user.Name + " looked up " + name + "'s next");
                }
            });
        }

        static public void gains(CommandService commands, UserCredential gcred, string ApplicationName)
        {
            commands.CreateCommand("gains").Parameter("type", ParameterType.Required).Parameter("name", ParameterType.Required).Parameter("ammount", ParameterType.Required).Do(async (e) =>                  //adds to single user
            {
                var user = e.User;
                int lnnum = 0;
                string name = e.GetArg("name");
                string ammount = e.GetArg("ammount");
                string type = e.GetArg("type");
                int t = 0;

                lnnum = valid.admin(user.Id.ToString(), name, creds.ssid(), ApplicationName, gcred);
                if (lnnum == 0)
                    lnnum = valid.trainer(user.Id.ToString(), name, creds.ssid(), ApplicationName, gcred);

                if (type == "add" || type == "Add" || type == "ADD")
                    t = 1;
                if (type == "sub" || type == "Sub" || type == "SUB")
                    t = 2;

                if (lnnum != 0 && valid.dectest(ammount))
                {
                    if (t == 2)
                        type = " - ";
                    if (t == 1)
                        type = " + ";


                    String range2 = "Characters!F" + lnnum.ToString();
                    var oblist = new List<object>() { "= " + account.xp(name, gcred, ApplicationName) + type + e.GetArg("ammount") };
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
    }
}
