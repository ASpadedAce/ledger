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
                string gc = "**General Commands:**\n";
                string ara = "__hello__ - Online test\n";
                string hp = "__help__ - Accesses the help messages.\n";
                string gcommands = gc + ara + hp;

                string reg = "**Registration Commands:**\n";
                string fr = "__first__ *name* - Tells the bot what *name* you (the person **not** chracter) would like to be known by.\n";
                string nw = "__new__ *name* - Creates a new character with the give *name*\n";
                string xf = "__xfer__ *character* *person* - Transpheres a *character* to another *person*, must be an admin if it is not your character.\n";
                string del = "__delete__ *character* - Deletes a *character*, must be your own character or you must be an admin.\n";
                string nm = "__name?__ - Tells you what the bot knows you as, useful **xfer** and **give**\n";
                string regcommands = reg + fr + nw + xf + del + nm;

                string money = "**Money Commands:**\n";
                string ba = "__bank__ *character* - Checks how much money a *character* has. (Your character, admin, accountant)\n";
                string sp = "__withdrawal__ *character* *ammount*- spends an *ammount* of a *character*'s money. (Your character, admin, accountant)\n";
                string aa = "__payday__ - Adds everyone's daily payment. (admin) \n";
                string pd = "__classaction__ *type* *ammount* - Adds(if *type* = add) or subtracts(if *type* = sub) an *ammount* to every account. (admin) \n";
                string ad = "__teller__ *type* *character* *ammount* - Adds(if *type* = add) or subtracts(if *type* = sub) an *ammount* to certain *character*. (admin, accountant)\n";
                string sb = "__decree__ *character* *ammount* - Set's a *character*'s bank account to the *ammount* specified. (admin, accountant)\n";
                string gv = "__give__ *from* *to* *name* *ammount* - Gives an *ammount* between two characters. *from* one character *to* the other, whose ownner's *name* you need to specify. (Your character, admin, accountant) \n";
                string moneycommands = money + ba + sp + aa + pd + ad + sb +gv;


                string exp = "**Experience Commands:**\n";
                string xp = "__setxp__ *character* *ammount*- Sets the *ammount* of xp that a *character* has. (admin, trainer) \n";
                string axp = "__gains__ *type* *character* *ammount*- Adds(if *type* = add) or subtracts(if *type* = sub) an *ammount* of xp to or from a *character*. (admin, trainer) \n";
                string lvl = "__level?__ *character* - Tells you the level of a *character*. (Your character, admin, trainer) \n";
                string xpq = "__xp?__ *character* - Tells you how much xp a *character* has in total. (Your character, admin, trainer) \n";
                string nxt = "__next?__ *character* - Tells you how much xp a *charactrer* needs to get to the next level. (Your character, admin, trainer)\n";
                string expcommands =exp + xp + axp + lvl + xpq  + nxt;

                string per = "**Permission Commands:**\n";
                string adm = "__admin__ *discordid* - adds person as an admin by thier *discord id*\n";
                string atm = "__accountant__ *discordid* - adds person as an accountant by thier *discord id*\n";
                string trn = "__trainer__ *discordid* - adds person as an trainer by thier *discord id*\n";
                string percommands = per + adm + atm + trn;

                await e.Channel.SendMessage(gcommands);
                await e.Channel.SendMessage(regcommands);
                await e.Channel.SendMessage(moneycommands);
                await e.Channel.SendMessage(expcommands);
                await e.Channel.SendMessage(percommands);
                Console.WriteLine(e.User.Name + " needs help");
            });
        }
    }
}
