using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

//discord
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Discord.Net;
using Discord.API;
using Discord.Rest;
namespace Katie2
{
    [Name("BasicCommands")]
    public class Commands : ModuleBase
    {
        [Command("ping")]
        public async Task Ping()
        {
            await Context.Channel.SendMessageAsync($"<@{Context.User.Id}>");
        }
        [Command("Kill")]
        public async Task Poweroff()
        {
            if (Context.User.Id == 331159190288596993)
            {
                await Context.Channel.SendMessageAsync("K.A.T.I.E shutting down...");
                await Program._client.SetStatusAsync(UserStatus.Offline);
                Environment.Exit(0);
            }
            else
            {
                await Context.Channel.SendMessageAsync("Sorry, $<@{Context.User.Id}> You may not Use that command");
            }
        }
        [Command("About")]
        public async Task About()
        {
            await Context.Channel.SendMessageAsync("```K.A.T.I.E        v.1.0" +
            "Developer(s): Eva#9914```");
        }
        // [Command("kick")]
        /*public async Task KickUser(ulong userId, string reason)
        {
            var user = Program.Program.client.GetUser(userId) as IGuildUser;
            if (((Context.User as IGuildUser).GuildPermissions.KickMembers || Context.User.Id == 366298290377195522) && (Program.client.CurrentUser as IGuildUser).GuildPermissions.KickMembers)
            {
                await user.KickAsync(reason);
            }
            else
            {
                await Context.Channel.SendMessageAsync("One of us doesn't have that permission...");
            }
        }*/
        [Command("Time")]
        public async Task GetTime()
        {
            await Context.Channel.SendMessageAsync("The time in the UK (GMT/BST) Is: " + DateTime.Now.ToString("h:mm:ss tt"));
        }
        [Command("Date")]
        public async Task GetDay()
        {
            await Context.Channel.SendMessageAsync("Today is: " + DateTime.Now.ToString("D"));
        }
        [Command("Wednesday")]
        public async Task WednesdayMyDudes()
        {
            if (DateTime.Now.ToString("D") == "Wed")
            {
                await Context.Channel.SendMessageAsync("It is Wednesday my dude :)");
            }
            else
            {
                await Context.Channel.SendMessageAsync("Unfortunately it is not Wednesday my dude :(");
            }
        }
        //[Command("Help")]
        //public async Task Help()
        //{
        //var helpEmbed = new EmbedBuilder
        //{
        //  Color = Color.Gold
        //};
        //}
        [Command("Source")]
        public async Task GetSourceCode()
        {
            await Context.Channel.SendFileAsync(@"C:\Users\Kerbinorbiter\Documents\K.A.T.I.E\Katie2\Source.cs", "K.A.T.I.E bot source code:");
        }
        [Command("Flip Coin")]
        public async Task CoinFlip(string HeadsOrTails)
        {
            if (HeadsOrTails == "Heads" | HeadsOrTails == "Tails")
            {
                Random rnd = new Random();
                RandomNumberGenerator cryptoRnd = RandomNumberGenerator.Create();
                int output = rnd.Next(1, 3);
                if (output < 2.5)
                {
                    if (HeadsOrTails == "Heads")
                    {
                        await Context.Channel.SendMessageAsync("You Won");
                        return;
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync("You Lost");
                        return;
                    }
                }
                else
                {
                    if (HeadsOrTails == "Heads")
                    {
                        await Context.Channel.SendMessageAsync("You Lost");
                        return;
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync("You Won");
                        return;
                    }
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync("Unknown Result: " + HeadsOrTails + " Please use *Heads* or *Tails*");
            }
        }
        //KSP Command groupings
        /*[Command("OrbVelocity")]
        public async Task OrbitalVeloctyCalc(double Apoapse, double Periapse, double altitude, double planetRadius)
        {
            double apaINTERNAL = Apoapse * 1000; //converts from kelometers into meters
            double pepINTERNAL = Periapse * 1000; //converts from km to m
            double planetRadiusINTERNAL = planetRadius * 1000;
            double internalAlt = planetRadiusINTERNAL+(altitude * 1000);
            double smaINTERNAL = ((apaINTERNAL + planetRadiusINTERNAL) + (pepINTERNAL + planetRadiusINTERNAL)) / 2;
            double gCONST = Math.Pow(6.67, -11);
            double v = Math.Pow(gCONST * (2 / internalAlt - 1 / smaINTERNAL), 0.5);
            double vROUND = Math.Round(v, 1);
            await Context.Channel.SendMessageAsync("Velocity:" + vROUND.ToString() + " m/s");
        }*/
        [Command("Azimuth")]
        public async Task AzimuthCalc(double i, double lat)
        {
            double azm = Math.Round(Math.Tanh(Math.Cos(i) / Math.Cos(lat)), 1);
            await Context.Channel.SendMessageAsync("Azimuth: " + azm.ToString() + " Degrees Heading North");
        }
        [Command("Boostback")]
        public async Task BoostbackCalculation(double startMass, double StageMass, double StageVelocity)
        {
            double massRatio = StageMass / startMass;
            double boostbackEndVel = StageVelocity * massRatio;
            double manuvureVelocity = StageVelocity + boostbackEndVel;
            await Context.Channel.SendMessageAsync("Boostback End Velocity: " + Math.Round(boostbackEndVel, 1).ToString() + " m/s");
            await Context.Channel.SendMessageAsync("Boostback Manuvure DeltaV: " + Math.Round(manuvureVelocity, 1).ToString() + " m/s");
        }
        [Command("Root")]
        public async Task Root(int rootInt, int exponent) //I changed these to ints for you- Carrot
        {
            try
            {
                double rtRES = Math.Pow(rootInt, 1 / exponent); //using a newtonian method
                await Context.Channel.SendMessageAsync("Result of: ROOT" + exponent.ToString() + "(" + rootInt.ToString() + ") = " + rtRES.ToString());
            }
            catch
            {
                await Context.Channel.SendMessageAsync("Error: you must enter an *integer* (Whole Number) for this operation");
            }
        }
        [Command("admin warn")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task Warn(string user, [Remainder]string warnReason)
        {
            /*setup variables such as user id, moderator id and discriminators to allow for the bot to do the following:
             * 1: display a message which states that user x has been warned by user y for doing z
             * 2: log that into a specific channel
             * 3: DM the user being warned that they have been warned in server x by user y for doing z
             */
            try
            {
                var warned = Context.Guild.GetUsersAsync().Result.ToList().FirstOrDefault(x => x.Id == CommandUtils.GetID(user));
                var authority = Context.Message.Author;
                await Context.Channel.SendMessageAsync($"Warn: {warned.Username}#{warned.Discriminator} For: {warnReason}");
            }
            catch (Exception e)
            {
                await Context.Channel.SendMessageAsync(e.ToString());
            }

        }
        [Command("Kick")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task Kick(string user, [Remainder]string kickReason)
        {
            var kicked = Context.Guild.GetUsersAsync().Result.ToList().FirstOrDefault(x => x.Id == CommandUtils.GetID(user));
            var kicker = Context.User;
            await Context.Channel.SendMessageAsync($"Kick: { kicked.Username}#{ kicked.Discriminator} By: **{kicker.Username}#{kicker.Discriminator}** For: {kickReason}");
            await kicked.KickAsync();
        }
        [Command("Bannir")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task Bannir(string user, [Remainder]string bannirReason)
        {
            var bannired = Context.Guild.GetUsersAsync().Result.ToList().FirstOrDefault(x => x.Id == CommandUtils.GetID(user));
            var bannirer = Context.User;
            if(bannired.Id == 81805731128414208 || bannired.Id == 588470417317036062) //bannir vanir
                await Context.Channel.SendFileAsync(@"D:\KATIEBOT\BANNIRVANNIR.png", $"Bannir: **{ bannired.Username}#{ bannired.Discriminator}** By: **{bannirer.Username}#{bannirer.Discriminator}** For: {bannirReason}"); 
            else
                await Context.Channel.SendMessageAsync($"Bannir: **{ bannired.Username}#{ bannired.Discriminator}** By: **{bannirer.Username}#{bannirer.Discriminator}** For: {bannirReason}");
            await bannired.BanAsync();
        }
        [Command("Ban")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task Ban(string user, [Remainder]string banReason)
        {
            var banned = Context.Guild.GetUsersAsync().Result.ToList().FirstOrDefault(x => x.Id == CommandUtils.GetID(user));
            var banner = Context.User;
            await Context.Channel.SendMessageAsync($"Ban: **{ banned.Username}#{ banned.Discriminator}** By: **{banner.Username}#{banner.Discriminator}** For: {banReason}");
            await banned.BanAsync();
        }
        [Command("Challenge Suggest")]
        public async Task Suggestions([Remainder]string challengeSuggestion)
        {
            var channel = Program._client.GetChannel(588128243920535582) as SocketTextChannel;
            var suggestion = challengeSuggestion;
            var userSuggestion = Context.User;
            await channel.SendMessageAsync($"Suggestion by: {userSuggestion.Username}#{userSuggestion.Discriminator}, {challengeSuggestion}");
        }
    }
    public class CommandUtils
    {
        public static ulong GetID(string source)
        {
            ulong Id;
            string IdStr = source
                .Replace("<", "")
                .Replace("@", "")
                .Replace("!", "")
                .Replace("#", "")
                .Replace("&", "")
                .Replace(">", ""); //A user mention is <@Id>, channels are <#Id>, etc. so we're removing those extra chars
            bool ok = ulong.TryParse(IdStr, out Id);
            if (ok)
                return Id;
            else
            {
                throw new FormatException(); //The string wasn't formatted correctly if the ID was invalid
                return 0;
            }
        }
    }
}
