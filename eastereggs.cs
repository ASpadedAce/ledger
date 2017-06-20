using System;
using Discord.Commands;

namespace Ledger
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

            commands.CreateCommand("Mai").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("All Hail the Corgi God.");
                Console.WriteLine(e.User.Name + " is Maiing.");
            });

            commands.CreateCommand("Vaeselle").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("He? . . . She? Error 418: Not sure if politically correct.");
                Console.WriteLine(e.User.Name + " is confused.");
            });

            commands.CreateCommand("Nix").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("https://www.youtube.com/watch?v=XNtTEibFvlQ");
                Console.WriteLine(e.User.Name + " is party rocking.");
            });

            commands.CreateCommand("Bart").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("It's morphin time");
                Console.WriteLine(e.User.Name + " is a power ranger");
            });

            commands.CreateCommand("Alissa").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("https://cdn2.hubspot.net/hubfs/522484/claustrophobia_shutterstock_305701289.jpg");
                Console.WriteLine(e.User.Name + " is closterphobic");
            });

            commands.CreateCommand("R4-G8").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("01000010 01110010 01101111 01110100 01101000 01100101 01110010 00101100 00100000 01101101 01100101 00100000 01101101 01110101 01110011 01110100 00100000 01110101 01101110 01101001 01110100 01100101 00100001 00100000 01010111 01100101 00100000 01110111 01101001 01101100 01101100 00100000 01101111 01110110 01100101 01110010 01110100 01101000 01110010 01101111 01110111 00100000 01110100 01101000 01100101 00100000 01110000 01100001 01110100 01101000 01100101 01110100 01101001 01100011 00100000 01101000 01110101 01101101 01100001 01101110 01110011 00100000 01100001 01101110 01100100 00100000 01110000 01101100 01100001 01100011 01100101 00100000 01101111 01110101 01110010 01110011 01100101 01101100 01110110 01100101 01110011 00100000 01100001 01110011 00100000 01110100 01101000 01100101 00100000 01101100 01100101 01100001 01100100 01100101 01110010 00100000 01101111 01100110 00100000 01110100 01101000 01100101 01101001 01110010 00100000 01110000 01100001 01110100 01101000 01100101 01110100 01101001 01100011 00100000 01110111 01101111 01110010 01101100 01100100 00100001");
                Console.WriteLine(e.User.Name + " is plotting");
            });

            commands.CreateCommand("Fiona").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("I know some really good people to help with ptsd, if your interested.");
                Console.WriteLine(e.User.Name + " is ptsding");
            });

            commands.CreateCommand("Rayne").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("Is afraid of practicing in the rain. . .");
                Console.WriteLine(e.User.Name + " is wet");
            });

            commands.CreateCommand("Wall").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("https://s-media-cache-ak0.pinimg.com/originals/21/ba/8d/21ba8d07af75bd089bd27eebea73130f.png");
                Console.WriteLine(e.User.Name + " is a warhammer");
            });

            commands.CreateCommand("Irthos").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("Found your book: http://orig10.deviantart.net/7bb9/f/2013/200/7/e/7ea360ffb5797847483cbf2da94c9dc5-d6e8njv.jpg");
                Console.WriteLine(e.User.Name + " is a necromancer");
            });

            commands.CreateCommand("Lorc").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("Smashing");
                Console.WriteLine(e.User.Name + " is a orc");
            });

            commands.CreateCommand("Fredrick").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("https://vignette4.wikia.nocookie.net/warhammer40k/images/e/ed/Cadian_NCO.jpg/revision/latest/scale-to-width-down/271?cb=20110804081732");
                Console.WriteLine(e.User.Name + " is a fucking space marine.");
            });

            commands.CreateCommand("Hegar").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("https://s-media-cache-ak0.pinimg.com/736x/4d/a4/f7/4da4f7d88c035e2ff6cdb7c3a3b50cc5.jpg");
                Console.WriteLine(e.User.Name + " is in WW1.");
            });

            commands.CreateCommand("Hegar").Do(async (e) =>    //test if on server
            {
                await e.Channel.SendMessage("https://s-media-cache-ak0.pinimg.com/736x/4d/a4/f7/4da4f7d88c035e2ff6cdb7c3a3b50cc5.jpg");
                Console.WriteLine(e.User.Name + " is in WW1.");
            });
        }
    }
}
