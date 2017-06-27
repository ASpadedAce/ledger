using System;
using Discord.Commands;


namespace Ledger
{
    class gc
    {
        static public void hello(CommandService commands)
        {
            commands.CreateCommand("hello").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("HOI!");
                Console.WriteLine(e.User.Name + " tested to see if i was here");
            });
        }

        static public void help(CommandService commands)
        {
            commands.CreateCommand("help").Do(async (e) =>        //command descriptions
            {
                string ara = "**hello** - Online test\n";
                string ba = "**bank** *charactername* - Checks how much money you have\n";
                string fr = "**first** *yourname* - let's us know who's character is who's\n";
                string nw = "**new** *charactername* - Creates a new character\n";
                string sp = "**spend** *charactername* *ammountofmoney*- spends your money\n";
                string adm = "**admin** *discordid* - adds person as an admin by discord id\n";
                string aa = "**payday** - Adds everyone's daily payment, admin\n";
                string pd = "**addall** *ammount* - Adds and ammount to every account, admin only.\n";
                string sa = "**suball** *ammount* - Subs ammount from all accounts, admin only.\n";
                string ad = "**add** *name* *ammount* - Add ammount to certain character, admin only.\n";
                string sb = "**sub** *name* *ammount* - Subs ammount from certain character, admin only.\n";
                string xf = "**xfer** *charactername* *persontransphereto* - Transpheres a character to another person, must be an admin if it is not your character.\n";
                string del = "**delete** *charactername* - Deletes a character, must be your own character or you must be an admin.\n";
                string sg = "**setgold** *charactername* *goldammount*- Sets the ammount of gold in a character's bank to a specific ammont, admin only\n";
                string xp = "**setxp** *charactername* *xpammount*- Sets the ammount of xp for aa character, admin only.\n";
                string nm = "**name?** - Tells you what the bot knows you as, useful if you forgot your name you put for the first command\n";
                string axp = "**addxp** *charactername* *ammount*- Adds xp to a character, admin only.\n";
                string sxp = "**subxp** *charactername* *ammount* - Subtracts xp from a character, admin only.\n";
                string lvl = "**level?** *charactername* - Tells you the level of a chracter, must be your chracter or you must be an admin.\n";
                string xpq = "**xp?** *charactername* - Tells you how much xp your character has in total, must be your character or you must be an admin.\n";
                string nxt = "**next?** *charactername* - Tells you how much xp your charactrer needs to get to the next level, must by yourt character or you must be an admin.\n";
                await e.Channel.SendMessage(ara + ba + fr + nw + sp + aa + sa + pd + adm + ad + sb + xf + nm + sg + xp + axp + sxp + lvl + xpq + nxt + del);
                Console.WriteLine(e.User.Name + " needs help");
            });
        }
    }
}
