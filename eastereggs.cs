using System;
using Discord.Commands;

namespace Ledger_0._1
{
    class eastereggs
    {
        static public void easter(CommandService commands)
        {
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

            commands.CreateCommand("Jiao").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("Bottoms up!");
                Console.WriteLine(e.User.Name + " is drinking");
            });

            commands.CreateCommand("Mikayla").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage(":flag_gb:");
                Console.WriteLine(e.User.Name + " is english");
            });

            commands.CreateCommand("Heath").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("https://images.moviepilot.com/images/c_limit,q_auto:good,w_600/xbrmi2p5pzto4hiu8b1a/10-insane-facts-you-probably-didn-t-know-about-heath-ledger-s-joker.jpg");
                Console.WriteLine(e.User.Name + " might be onto us");
            });

            commands.CreateCommand("Hobbit").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("We're going to Isenguard!");
                Console.WriteLine(e.User.Name + " is drinking");
            });

            commands.CreateCommand("Negative").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("Positive");
                Console.WriteLine(e.User.Name + " is negative");
            });

            commands.CreateCommand("Positive").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("Negative");
                Console.WriteLine(e.User.Name + " is positive");
            });

            commands.CreateCommand("Vagal").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("MORE KILLING!");
                Console.WriteLine(e.User.Name + " is killing");
            });

            commands.CreateCommand("Mina").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("Still searching. . .");
                Console.WriteLine(e.User.Name + " is searching");
            });

            commands.CreateCommand("Patrons").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("Victims*");
                Console.WriteLine(e.User.Name + " . . . MUWAHAHAHA!!!!!");
            });

            commands.CreateCommand("Owlmod").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("http://i.imgur.com/ACpq7TR.png");
                Console.WriteLine(e.User.Name + " is looking at an owl");
            });

            commands.CreateCommand("Talik").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("Kksssshhhh. Vrãu! vrãu! Nnnnnnnnn. Psssssssss. BANG! THAK! Thuk!");
                Console.WriteLine(e.User.Name + " is wielding a lightsaber.");
            });

            commands.CreateCommand("Bots").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("Beep. Boop. Bork.");
                Console.WriteLine(e.User.Name + " is botting");
            });

            commands.CreateCommand("killerqueen").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("Gunpowder, gelatine\nDynamite with a laser beam\nGuaranteed to blow your mind!");
                Console.WriteLine(e.User.Name + " is emulating a legend");
            });

            commands.CreateCommand("daftpunk").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("Work it harder\nMake it better\n Do it faster\n Makes us stronger");
                Console.WriteLine(e.User.Name + " is getting lucky.");
            });
        }
    }
}
