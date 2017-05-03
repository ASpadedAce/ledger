using Discord;
using Discord.Commands;
using System;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TestLedger
{
    public class Ledger
    {
        DiscordClient client;                                               //discord virtual client access
        CommandService commands;                                            //discord command access
        static string[] Scopes = { SheetsService.Scope.Spreadsheets };      //type of access
        static string ApplicationName = "Ledger";                           //name for google registration
        int spcnt = 0;                                                      //sheet empty space storage
        string dissec;                                                      //discord bot secret
        string ssid;                                                        //google spreadsheet id
        string ownid;                                                       //owner discord id

        public Ledger()
        {

            //Read In Discord Token and Google Spreadsheets ID

            string credPath = Environment.GetFolderPath(
                   Environment.SpecialFolder.Personal);
            credPath = Path.Combine(credPath, "Ledger");
            if (!File.Exists(Path.Combine(credPath, "ids.txt")))
            {
                string[] write = { "Discord Token:TOKEN", "Google Spreadsheet id:ID", "Owner Discord id:ID" };
                Directory.CreateDirectory(credPath);
                File.WriteAllLines(Path.Combine(credPath, "ids.txt"), write);
                Console.WriteLine("Please check the directory ~/Ledger for the file ids.txt and input the\nDiscord token, Owner Disocrd ID and spreadsheet id for your bot now.\n Once finnished select this panel again and hit enter");
                Console.Read();
            }
            if (File.Exists(Path.Combine(credPath, "ids.txt")))
            {
                string[] lines = File.ReadAllLines(Path.Combine(credPath, "ids.txt"));
                int colpos = lines[0].IndexOf(':');
                dissec = lines[0].Substring(colpos + 1);
                Console.WriteLine("Discord Token: " + dissec);
                colpos = lines[1].IndexOf(':');
                ssid = lines[1].Substring(colpos + 1);
                Console.WriteLine("Google Sheet ID: " + ssid);
                colpos = lines[2].IndexOf(':');
                ownid = lines[2].Substring(colpos + 1);
                Console.WriteLine("Owner Discord Id: " + ownid);
            }

            //discord auth

            client = new DiscordClient(input =>                         //call login routine
            {
                input.LogLevel = LogSeverity.Info;
                input.LogHandler = Login;
            });

            client.UsingCommands(input =>                               //enable commands
            {
                input.PrefixChar = '&';
                input.AllowMentionPrefix = true;
            });

            //google auth

            UserCredential credential;

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

            // General Commands

            commands = client.GetService<CommandService>();             //start command service
                                                                        //commands
            commands.CreateCommand("hello").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("HOI!");
                Console.WriteLine(e.User.Name + " tested to see if i was here");
            });

            commands.CreateCommand("help").Do(async (e) =>        //command descriptions
            {
                string ara = "**hello** - Online test\n";
                string ba = "**bank** *charactername* - Checks how much money you have\n";
                string fr = "**first** *yourname* - let's us know who's character is who's\n";
                string nw = "**new** *charactername* - Creates a new character\n";
                string sp = "**spend** *charactername* *ammountofmoney*- spends your money\n";
                string adm = "**admin** *discordid* - adds person as an admin by discord id\n";
                string aa = "**payday** - Adds everyone's daily payment, admin\n";
                string pd = "**addall** *ammount* - Adds and ammount to every account, admin\n";
                string sa = "**suball** *ammount* - Subs ammount from all accounts, admin\n";
                string ad = "**add** *name* *ammount* - Add ammount to certain character, admin, accountant.\n";
                string sb = "**sub** *name* *ammount* - Subs ammount from certain character, admin, accountant.\n";
                string xf = "**xfer** *charactername* *persontransphereto* - Transpheres an ammount of money.\n";
                string nm = "**name?** - Tells you what the bot knows you as, useful if you forgot your name you put for the first command\n";
                await e.Channel.SendMessage(ara + ba + fr + nw + sp + aa + sa + pd + adm+ ad + sb + xf + nm);
                Console.WriteLine(e.User.Name + " needs help");
            });

            commands.CreateCommand("first").Parameter("name", ParameterType.Required).Do(async (e) =>                  //record user data for signup
            {
                var user = e.User;
                string name = e.GetArg("name");
                DateTime localDate = DateTime.Now;
                bool dumbass = false;

                var service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                //records user data to spreadsheet
                string range = "Users!A1:B";
                SpreadsheetsResource.ValuesResource.GetRequest request =
                        service.Spreadsheets.Values.Get(ssid, range);

                //print range
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
                    String range2 = "Users!A" + spcnt.ToString() + ':' + 'B' + spcnt.ToString();  // update next row
                    ValueRange valueRange = new ValueRange();
                    var oblist = new List<object>() { user.Id.ToString(), name };
                    valueRange.Values = new List<IList<object>> { oblist };

                    SpreadsheetsResource.ValuesResource.UpdateRequest update = service.Spreadsheets.Values.Update(valueRange, ssid, range2);
                    update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
                    UpdateValuesResponse result2 = update.Execute();

                    await e.Channel.SendMessage(user.Name + " has registered with ID " + user.Id);
                    Console.WriteLine(user.Name + " has registered with ID " + user.Id);
                }
                else
                {
                    await e.Channel.SendMessage("Stop, get some help. We've been through this already");
                    Console.WriteLine(user.Name + " has tried to register again");
                }
                spcnt = 0;
            });

            commands.CreateCommand("new").Parameter("name", ParameterType.Required).Do(async (e) =>                  //record new chracters
            {
                var user = e.User;
                string name = e.GetArg("name");
                DateTime localDate = DateTime.Now;
                bool dumbass = false;

                var service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                //records user data to spreadsheet
                string range = "Characters!A1:C";
                SpreadsheetsResource.ValuesResource.GetRequest request =
                        service.Spreadsheets.Values.Get(ssid, range);

                //tests for previous chracters
                ValueRange response = request.Execute();
                IList<IList<Object>> values = response.Values;

                foreach (var row in values)
                {
                    if (row[1].ToString() == user.Id.ToString() && row[2].ToString() == name)
                        dumbass = true;
                    spcnt++;
                }

                spcnt++;
                if (dumbass == false)
                {
                    String range2 = "Characters!A" + spcnt.ToString() + ':' + 'E' + spcnt.ToString();
                    ValueRange valueRange = new ValueRange();
                    var oblist = new List<object>() { localDate.ToString(), user.Id.ToString(), name, 500, 0 };
                    valueRange.Values = new List<IList<object>> { oblist };

                    SpreadsheetsResource.ValuesResource.UpdateRequest update = service.Spreadsheets.Values.Update(valueRange, ssid, range2);
                    update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
                    UpdateValuesResponse result2 = update.Execute();

                    await e.Channel.SendMessage(user.Name + " has created a new character, " + name);
                    Console.WriteLine(user.Name + " has created a new character, " + name);
                }
                else
                {
                    await e.Channel.SendMessage("Sorry, no clones allowed.");
                    Console.WriteLine(user.Name + " tried to create a clone ");
                }

                spcnt = 0;
            });

            commands.CreateCommand("spend").Parameter("name", ParameterType.Required).Parameter("ammount", ParameterType.Required).Do(async (e) =>                  //allows for a user to spend a certain ammount of money
            {
                var user = e.User;
                string name = e.GetArg("name");
                string ammount = e.GetArg("ammount");
                DateTime localDate = DateTime.Now;
                bool valid = false;
                int line = 0;

                var service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                
                string range = "Characters!A1:D";
                SpreadsheetsResource.ValuesResource.GetRequest request =
                        service.Spreadsheets.Values.Get(ssid, range);

                //print range
                ValueRange response = request.Execute();
                IList<IList<Object>> values = response.Values;

                foreach (var row in values)
                {
                    if (row[1].ToString() == user.Id.ToString() && row[2].ToString() == name)
                    {
                        valid = false;
                        if (ammount.First() == '1' || ammount.First() == '2' || ammount.First() == '3' || ammount.First() == '4' || ammount.First() == '5' || ammount.First() == '6' || ammount.First() == '7' || ammount.First() == '8' || ammount.First() == '9' || ammount.First() == '0')
                        {
                            valid = true;
                        }
                        line = spcnt + 1;
                        ammount = "= " + row[3].ToString() + " - " + ammount;
                    }
                    spcnt++;
                }

                spcnt++;
                if (valid == true)
                {
                    String range2 = "Characters!D" + line.ToString();  // update next row
                    ValueRange valueRange = new ValueRange();
                    var oblist = new List<object>() { ammount };
                    valueRange.Values = new List<IList<object>> { oblist };

                    SpreadsheetsResource.ValuesResource.UpdateRequest update = service.Spreadsheets.Values.Update(valueRange, ssid, range2);
                    update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
                    UpdateValuesResponse result2 = update.Execute();

                    await e.Channel.SendMessage("Your transaction for " + name + " has been processed (" + ammount + ")");
                    Console.WriteLine(user.Name + "'s transaction for " + name + " has been processed (" + ammount + ")");
                }
                else
                {
                    await e.Channel.SendMessage("What are you? A rogue?");
                    Console.WriteLine(user.Name + " tried to rip someone off");
                }

                spcnt = 0;
            });

            commands.CreateCommand("bank").Parameter("name", ParameterType.Required).Do(async (e) =>                  //prints out banking information
            {
                var user = e.User;
                string name = e.GetArg("name");
                string ammount = null;
                DateTime localDate = DateTime.Now;
                bool dumbass = true;

                var service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                
                string range = "Characters!A1:D";
                SpreadsheetsResource.ValuesResource.GetRequest request =
                        service.Spreadsheets.Values.Get(ssid, range);

                //finds user entry and records value
                ValueRange response = request.Execute();
                IList<IList<Object>> values = response.Values;

                foreach (var row in values)
                {
                    if (row[1].ToString() == user.Id.ToString() && row[2].ToString() == name)
                    {
                        ammount = row[3].ToString();
                        dumbass = false;
                    }
                }

                if (dumbass == true)
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

            commands.CreateCommand("payday").Do(async (e) =>                  //updates dayly payout
            {
                var user = e.User;
                DateTime localDate = DateTime.Now;
                bool dumbass = true;

                var service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                
                string range = "Users!D:E";
                SpreadsheetsResource.ValuesResource.GetRequest request =
                        service.Spreadsheets.Values.Get(ssid, range);

                
                ValueRange response = request.Execute();
                IList<IList<Object>> values = response.Values;
                //tests for admin permission
                foreach (var row in values)
                {
                    if (row[0].ToString() == user.Id.ToString())
                        dumbass = false;
                    if (ownid == user.Id.ToString())
                        dumbass = false;
                }

                if (dumbass == false)
                {

                    
                    range = "Characters!A1:E";
                    request =
                            service.Spreadsheets.Values.Get(ssid, range);

                    
                    response = request.Execute();
                    values = response.Values;

                    foreach (var row in values)
                    {
                        String range2 = "Characters!D" + (spcnt + 1).ToString();  
                        ValueRange valueRange = new ValueRange();
                        var oblist = new List<object>() { "= " + row[3].ToString() + " + " + row[4].ToString() };
                        valueRange.Values = new List<IList<object>> { oblist };

                        SpreadsheetsResource.ValuesResource.UpdateRequest update = service.Spreadsheets.Values.Update(valueRange, ssid, range2);
                        update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
                        UpdateValuesResponse result2 = update.Execute();

                        Console.WriteLine(row[0] + " has been payed");
                        spcnt++;
                    }

                    await e.Channel.SendMessage("Payments Distributed");
                    Console.WriteLine("Payout Finished");
                    spcnt++;
                    spcnt = 0;
                }
                else
                {
                    await e.Channel.SendMessage("Nice Try");
                    Console.WriteLine(user.Name + " tried to do a payout but failed");
                }
            });

            commands.CreateCommand("addall").Parameter("ammount", ParameterType.Required).Do(async (e) =>                  //adds an ammount to all users
            {
                var user = e.User;
                DateTime localDate = DateTime.Now;
                bool valid = false;
                string ammount = e.GetArg("ammount");
                bool dumbass = true;

                var service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                
                string range = "Users!D:E";
                SpreadsheetsResource.ValuesResource.GetRequest request =
                        service.Spreadsheets.Values.Get(ssid, range);

                
                ValueRange response = request.Execute();
                IList<IList<Object>> values = response.Values;
                
                foreach (var row in values)
                {
                    if (row[0].ToString() == user.Id.ToString())
                        dumbass = false;
                    if (ownid == user.Id.ToString())
                        dumbass = false;
                }
                //valid number entry test
                if (ammount.First() == '1' || ammount.First() == '2' || ammount.First() == '3' || ammount.First() == '4' || ammount.First() == '5' || ammount.First() == '6' || ammount.First() == '7' || ammount.First() == '8' || ammount.First() == '9' || ammount.First() == '0')
                {
                    valid = true;
                }

                if (dumbass == false && valid == true)
                {

                    
                    range = "Characters!A1:E";
                    request =
                            service.Spreadsheets.Values.Get(ssid, range);

                    
                    response = request.Execute();
                    values = response.Values;

                    foreach (var row in values)
                    {
                        String range2 = "Characters!D" + (spcnt + 1).ToString();  
                        ValueRange valueRange = new ValueRange();
                        var oblist = new List<object>() { "= " + row[3].ToString() + " + " + e.GetArg("ammount") };
                        valueRange.Values = new List<IList<object>> { oblist };

                        SpreadsheetsResource.ValuesResource.UpdateRequest update = service.Spreadsheets.Values.Update(valueRange, ssid, range2);
                        update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
                        UpdateValuesResponse result2 = update.Execute();

                        Console.WriteLine(row[0] + " has been payed");
                        spcnt++;
                    }

                    await e.Channel.SendMessage("Added " + e.GetArg("ammount") + " to everyone's account");
                    Console.WriteLine(user.Name + " added " + e.GetArg("ammount") + " to everyone's account");
                    spcnt++;
                    spcnt = 0;
                }
                else
                {
                    await e.Channel.SendMessage("Nope");
                    Console.WriteLine(user.Name + " tried to add " + e.GetArg("ammount") + " to everyone's accounts");
                }
            });

            commands.CreateCommand("suball").Parameter("ammount", ParameterType.Required).Do(async (e) =>                  //subtracts and ammount from all users
            {
                var user = e.User;
                DateTime localDate = DateTime.Now;
                bool valid = false;
                string ammount = e.GetArg("ammount");

                bool dumbass = true;

                var service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                
                string range = "Users!D:E";
                SpreadsheetsResource.ValuesResource.GetRequest request =
                        service.Spreadsheets.Values.Get(ssid, range);

               
                ValueRange response = request.Execute();
                IList<IList<Object>> values = response.Values;

                foreach (var row in values)
                {
                    if (row[0].ToString() == user.Id.ToString())
                        dumbass = false;
                    if (ownid == user.Id.ToString())
                        dumbass = false;
                }

                if (ammount.First() == '1' || ammount.First() == '2' || ammount.First() == '3' || ammount.First() == '4' || ammount.First() == '5' || ammount.First() == '6' || ammount.First() == '7' || ammount.First() == '8' || ammount.First() == '9' || ammount.First() == '0')
                {
                    valid = true;
                }

                if (dumbass == false && valid == true)
                {
                   
                    range = "Characters!A1:E";
                    request =
                            service.Spreadsheets.Values.Get(ssid, range);

                    
                    response = request.Execute();
                    values = response.Values;

                    foreach (var row in values)
                    {
                        String range2 = "Characters!D" + (spcnt + 1).ToString();  
                        ValueRange valueRange = new ValueRange();
                        var oblist = new List<object>() { "= " + row[3].ToString() + " - " + e.GetArg("ammount") };
                        valueRange.Values = new List<IList<object>> { oblist };

                        SpreadsheetsResource.ValuesResource.UpdateRequest update = service.Spreadsheets.Values.Update(valueRange, ssid, range2);
                        update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
                        UpdateValuesResponse result2 = update.Execute();

                        Console.WriteLine(row[0] + " has been payed");
                        spcnt++;
                    }

                    await e.Channel.SendMessage("Subtracted " + e.GetArg("ammount") + " from everyone's account");
                    Console.WriteLine(user.Name + " subtracted " + e.GetArg("ammount") + " from everyone's account");
                    spcnt++;
                    spcnt = 0;
                }
                else
                {
                    await e.Channel.SendMessage("So screwing one person out of thier money wasn't enough for you?");
                    Console.WriteLine(user.Name + " tried to subtract " + e.GetArg("ammount") + " from everyone's accounts");
                }
            });

               commands.CreateCommand("add").Parameter("name", ParameterType.Required).Parameter("ammount", ParameterType.Required).Do(async (e) =>                  //adds to single user
               {
                   var user = e.User;
                   DateTime localDate = DateTime.Now;
                   bool valid = false;
                   int lnnum = 0;
                   string name = e.GetArg("name");
                   string ammount = e.GetArg("ammount");
                   bool dumbass = true;

                   

                   var service = new SheetsService(new BaseClientService.Initializer()
                   {
                       HttpClientInitializer = credential,
                       ApplicationName = ApplicationName,
                   });

                   
                   string range = "Users!D1:E";
                   SpreadsheetsResource.ValuesResource.GetRequest request =
                           service.Spreadsheets.Values.Get(ssid, range);

                  
                   ValueRange response = request.Execute();
                   IList<IList<Object>> values = response.Values;
                   foreach (var row in values)
                   {
                       if (row[0].ToString() == user.Id.ToString())
                           dumbass = false;
                       if (ownid == user.Id.ToString())
                           dumbass = false;
                       
                   }

                   if (ammount.First() == '1' || ammount.First() == '2' || ammount.First() == '3' || ammount.First() == '4' || ammount.First() == '5' || ammount.First() == '6' || ammount.First() == '7' || ammount.First() == '8' || ammount.First() == '9' || ammount.First() == '0')
                   {
                       valid = true;
                   }

                   if (dumbass == false && valid == true)
                   {

                       
                       range = "Characters!A1:E";
                       request =
                               service.Spreadsheets.Values.Get(ssid, range);

                       
                       response = request.Execute();
                       values = response.Values;

                       foreach (var row in values)
                       {
                           if (row[2].ToString() == name)
                           {
                               lnnum = spcnt + 1;
                               ammount ="= " + row[3].ToString() + " + " + ammount;
                           }
                           spcnt++;
                       }
                       spcnt++;

                       if (lnnum != 0)
                       {
                           String range2 = "Characters!D" + lnnum.ToString();  
                           ValueRange valueRange = new ValueRange();
                           var oblist = new List<object>() { ammount };
                           valueRange.Values = new List<IList<object>> { oblist };

                           SpreadsheetsResource.ValuesResource.UpdateRequest update = service.Spreadsheets.Values.Update(valueRange, ssid, range2);
                           update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
                           UpdateValuesResponse result2 = update.Execute();

                           await e.Channel.SendMessage("Your transaction for " + name + " has been processed (" + ammount + ")");
                           Console.WriteLine(user.Name + "'s transaction for " + name + " has been processed (" + ammount + ")");
                       }
                       else
                       {
                           await e.Channel.SendMessage("That character's just a figment of your imagination");
                           Console.WriteLine(user.Name + "'s transaction for " + name + " has failed");
                       }
                   }
                   else
                   {
                       await e.Channel.SendMessage("So you **think** you make money?");
                       Console.WriteLine(user.Name + " tried to add " + e.GetArg("ammount") + " to a character's account");
                   }
                   spcnt = 0;
               });

               commands.CreateCommand("sub").Parameter("name", ParameterType.Required).Parameter("ammount", ParameterType.Required).Do(async (e) =>                  //adds to multiple users
               {
                   var user = e.User;
                   DateTime localDate = DateTime.Now;
                   bool valid = false;
                   int lnnum = 0;
                   string name = e.GetArg("name");
                   string ammount = e.GetArg("ammount");
                   bool dumbass = true;

                   var service = new SheetsService(new BaseClientService.Initializer()
                   {
                       HttpClientInitializer = credential,
                       ApplicationName = ApplicationName,
                   });

                   
                   string range = "Users!D1:E";
                   SpreadsheetsResource.ValuesResource.GetRequest request =
                           service.Spreadsheets.Values.Get(ssid, range);

                   
                   ValueRange response = request.Execute();
                   IList<IList<Object>> values = response.Values;

                   foreach (var row in values)
                   {
                       if (row[0].ToString() == user.Id.ToString())
                           dumbass = false;
                       if (ownid == user.Id.ToString())
                           dumbass = false;
                   }

                   if (ammount.First() == '1' || ammount.First() == '2' || ammount.First() == '3' || ammount.First() == '4' || ammount.First() == '5' || ammount.First() == '6' || ammount.First() == '7' || ammount.First() == '8' || ammount.First() == '9' || ammount.First() == '0')
                   {
                       valid = true;
                   }

                   if (dumbass == false && valid == true)
                   {

                       
                       range = "Characters!A1:E";
                       request =
                               service.Spreadsheets.Values.Get(ssid, range);

                       
                       response = request.Execute();
                       values = response.Values;

                       foreach (var row in values)
                       {
                           if (row[2].ToString() == name)
                           {
                               lnnum = spcnt + 1;
                               ammount = "= " + row[3].ToString() + " - " + ammount;
                           }
                           spcnt++;
                       }
                       spcnt++;

                       if (lnnum != 0)
                       {
                           String range2 = "Characters!D" + lnnum.ToString();
                           ValueRange valueRange = new ValueRange();
                           var oblist = new List<object>() { ammount };
                           valueRange.Values = new List<IList<object>> { oblist };

                           SpreadsheetsResource.ValuesResource.UpdateRequest update = service.Spreadsheets.Values.Update(valueRange, ssid, range2);
                           update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
                           UpdateValuesResponse result2 = update.Execute();

                           await e.Channel.SendMessage("Your transaction for " + name + " has been processed (" + ammount + ")");
                           Console.WriteLine(user.Name + "'s transaction for " + name + " has been processed (" + ammount + ")");
                       }
                       else
                       {
                           await e.Channel.SendMessage("That character's just a figment of your imagination");
                           Console.WriteLine(user.Name + "'s transaction for " + name + " has failed");
                       }
                   }
                   else
                   {
                       await e.Channel.SendMessage("So you **think** you make money?");
                       Console.WriteLine(user.Name + " tried to add " + e.GetArg("ammount") + " to a character's account");
                   }
                   spcnt = 0;
               });

            commands.CreateCommand("xfer").Parameter("name1", ParameterType.Required).Parameter("name2", ParameterType.Required).Do(async (e) =>                  //transpheres from one character to another
            {
                var user = e.User;
                string name1 = e.GetArg("name1");
                string name2 = e.GetArg("name2");
                DateTime localDate = DateTime.Now;
                bool valid = false;
                int line = 0;

                var service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                
                string range = "Characters!A1:D";
                SpreadsheetsResource.ValuesResource.GetRequest request =
                        service.Spreadsheets.Values.Get(ssid, range);

               
                ValueRange response = request.Execute();
                IList<IList<Object>> values = response.Values;

                foreach (var row in values)
                {
                    if (row[1].ToString() == user.Id.ToString() && row[2].ToString() == name1)
                    {
                        valid = true;
                        line = spcnt + 1;
                    }
                    spcnt++;
                }

                range = "Users!A1:B";
                request =
                        service.Spreadsheets.Values.Get(ssid, range);

                
                response = request.Execute();
                values = response.Values;
                if (valid == true)
                {
                    valid = false;
                    foreach (var row in values)
                    {
                        if (row[1].ToString() == name2)
                        {
                            valid = true;
                            name2 = row[0].ToString();
                        }
                        spcnt++;
                    }
                }
                spcnt++;

                if (valid == true)
                {
                    String range2 = "Characters!B" + line.ToString();  
                    ValueRange valueRange = new ValueRange();
                    var oblist = new List<object>() { name2 };
                    valueRange.Values = new List<IList<object>> { oblist };

                    SpreadsheetsResource.ValuesResource.UpdateRequest update = service.Spreadsheets.Values.Update(valueRange, ssid, range2);
                    update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
                    UpdateValuesResponse result2 = update.Execute();

                    await e.Channel.SendMessage("Your character has been transphered to " + name2);
                    Console.WriteLine(user.Name + "'s character has been given to " + name2);
                }
                else
                {
                    await e.Channel.SendMessage("You can't do that!");
                    Console.WriteLine(user.Name + " tried and failed a character transphere");
                }

                spcnt = 0;
            });


            commands.CreateCommand("name?").Do(async (e) =>                  //gives what a users name is registered as
            {
                var user = e.User;
                string name = null;
                DateTime localDate = DateTime.Now;
                bool dumbass = true;

                var service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                
                string range = "Users!A1:D";
                SpreadsheetsResource.ValuesResource.GetRequest request =
                        service.Spreadsheets.Values.Get(ssid, range);

                
                ValueRange response = request.Execute();
                IList<IList<Object>> values = response.Values;

                foreach (var row in values)
                {
                    if (row[0].ToString() == user.Id.ToString())
                    {
                        name = row[1].ToString();
                        dumbass = false;
                    }
                }

                if (dumbass == true)
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

            
            //permissions commands

            commands.CreateCommand("admin").Parameter("discordid", ParameterType.Required).Do(async (e) =>                  //record user data for signup
            {
                var user = e.User;
                string name = e.GetArg("discordid");
                DateTime localDate = DateTime.Now;
                bool dumbass = true;

                var service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                
                string range = "Users!D:D";
                SpreadsheetsResource.ValuesResource.GetRequest request =
                        service.Spreadsheets.Values.Get(ssid, range);

                
                ValueRange response = request.Execute();
                IList<IList<Object>> values = response.Values;

                foreach (var row in values)
                {
                    if (row[0].ToString() == user.Id.ToString())
                        dumbass = false;
                    if (ownid == user.Id.ToString())
                        dumbass = false;
                    spcnt++;
                }

                spcnt++;
                if (dumbass == false)
                {
                    String range2 = "Users!D" + spcnt.ToString(); 
                    ValueRange valueRange = new ValueRange();
                    var oblist = new List<object>() { name };
                    valueRange.Values = new List<IList<object>> { oblist };

                    SpreadsheetsResource.ValuesResource.UpdateRequest update = service.Spreadsheets.Values.Update(valueRange, ssid, range2);
                    update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
                    UpdateValuesResponse result2 = update.Execute();

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

            
            //EasterEggs

            commands.CreateCommand("snowflake").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("Yes, you are special");
                Console.WriteLine(e.User.Name + " is a special snowflake");
            });

            commands.CreateCommand("hathaway").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("https://en.wikipedia.org/wiki/Anne_Hathaway");
                Console.WriteLine(e.User.Name + " is into anne hathaway");
            });

            commands.CreateCommand("rogue").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("/r 20d6");
                Console.WriteLine(e.User.Name + " gotcha");
            });

            commands.CreateCommand("var").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("*Indistinguishable Mandalorian screaming*");
                Console.WriteLine(e.User.Name + " summoned var");
            });

            commands.CreateCommand("Arkan").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("*horny*");
                Console.WriteLine(e.User.Name + " tried to milk arkan");
            });

            commands.CreateCommand("Raissa").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("So. . .many. . . scars");
                Console.WriteLine(e.User.Name + " summoned a ranger");
            });

            commands.CreateCommand("Contributors").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("They're in on it too...");
                Console.WriteLine(e.User.Name + " is in on it too");
            });

            commands.CreateCommand("Nash").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("He's definatly not a beholder");
                Console.WriteLine(e.User.Name + " wants nash, he's definatly not a beholder");
            });

            commands.CreateCommand("Sepia").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("```Fix/nGod damn it brady!/n```");
                Console.WriteLine(e.User.Name + " is reminding you of the incident");
            });

            commands.CreateCommand("dreggo").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("Worse player NA");
                Console.WriteLine(e.User.Name + " is dealing with dreggo");
            });

            commands.CreateCommand("crass").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("YOU ARE AN ASSHOLE");
                Console.WriteLine(e.User.Name + " is a asshole");
            });

            commands.CreateCommand("henry").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("*clicks adoribly*");
                Console.WriteLine(e.User.Name + " is clicking");
            });

            commands.CreateCommand("Cass").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("*Explosion*");
                Console.WriteLine(e.User.Name + " exploded");
            });

            commands.CreateCommand("Mods").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("Shhhhhh. . . We're planning");
                Console.WriteLine(e.User.Name + " might be onto us");
            });

            commands.CreateCommand("Patrons").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("Victims*");
                Console.WriteLine(e.User.Name + " . . . MUWAHAHAHA!!!!!");
            });
            //login routine

            client.ExecuteAndWait(async () =>                       //join server command
            {
                await client.Connect((dissec), TokenType.Bot);
            });
        }

        private void Login(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}