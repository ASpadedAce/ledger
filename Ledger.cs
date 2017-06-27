using Discord;
using Discord.Commands;
using System;
using Google.Apis.Auth.OAuth2;

namespace Ledger
{
    public class Ledger
    {
        DiscordClient client;                                               //discord virtual client access
        CommandService commands;                                            //discord command access
        UserCredential gcred;                                               //user credentials via google
        static string ApplicationName = "Ledger";                           //name for google registration

        public Ledger()
        {

//grab ids
            startup.id();

//google credential grab
            gcred = startup.google();

//discord auth
            client = new DiscordClient(input =>                         //call login routine
            { input.LogLevel = LogSeverity.Info; input.LogHandler = Login; });
            client.UsingCommands(input =>                               //command parameters
            { input.PrefixChar = '&';input.AllowMentionPrefix = true; });

            commands = client.GetService<CommandService>();             //start command service

//General commands
            gc.hello(commands);
            gc.help(commands);

//Registration commands
            reg.first(commands, gcred, ApplicationName);
            reg.nw(commands, gcred, ApplicationName);
            reg.delete(commands, gcred, ApplicationName);
            reg.xfer(commands, gcred, ApplicationName);
            reg.name(commands, gcred, ApplicationName);

//Money Managment Commands

//login routine

            client.ExecuteAndWait(async () =>                       //join server command
            {
                await client.Connect((creds.token()), TokenType.Bot);
            });
        }

        private void Login(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}