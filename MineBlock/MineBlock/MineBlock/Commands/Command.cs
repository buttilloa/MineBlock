using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MineBlock.Blocks;
using System.Runtime.Remoting;
using System.Reflection;
using System.Threading;

namespace MineBlock.Commands
{
    public class Command
    {
        public String Desc = "";
        public int index;
        public String usage = "";
        public Command()
        {

        }
        public virtual String Execute(String[] args)
        {
            return "";
        }
    }
    class Setblock : Command
    {

        public Setblock()
        {
            Desc = "Sets a block: usuage: setblock <Block Index> <x> <y> ";
            index = 0;
            usage = "setblock";
        }
        public override string Execute(String[] args)
        {
            args[1] = args[1].ToLower();
            String first = args[1].Substring(0, 1).ToUpper();
            args[1] = first + args[1].Substring(1);
            args[1] = "MineBlock.Blocks." + args[1];

            Type hai = Type.GetType(args[1]);
            ConstructorInfo ctor = hai.GetConstructor(new[] { typeof(int), typeof(int) });
            Block instance = (Block)ctor.Invoke(new object[] { (int)Convert.ToSingle(args[2]), (int)Convert.ToSingle(args[3]) });
            Chunk.SetBlock(Game1.Loadedchunks, (int)Convert.ToSingle(args[2]), (int)Convert.ToSingle(args[3]), instance);
            Chunk.getChunk(Game1.Loadedchunks, (int)Convert.ToSingle(args[2]), (int)Convert.ToSingle(args[3])).organiseBlocks();
            

            return "Changed block" + "[" + (int)Convert.ToSingle(args[2]) + "," + (int)Convert.ToSingle(args[3]) + "] to " + instance;

        }

    }
    class DisplayCoords : Command
    {

        public DisplayCoords()
        {
            Desc = "Shows block coords: usuage: displaycoords <Boolean> ";
            index = 1;
            usage = "displaycoords";
        }
        public override String Execute(String[] args)
        {
            Game1.console.display = Convert.ToBoolean(args[1].ToLower());
            return Game1.console.display ? "Showing Block Coordinates" : "Disabled Block Coordinates";
        }

    }
    class BlockOutline : Command
    {

        public BlockOutline()
        {
            Desc = "Shows block outline: usuage: blockoutline <Boolean> ";
            index = 2;
            usage = "blockoutline";
        }
        public override String Execute(String[] args)
        {
            Game1.console.outlined = Convert.ToBoolean(args[1].ToLower());
            return Game1.console.outlined ? "Block outline enabled" : "Block outline disabled";
        }

    }
    class MobCoords : Command
    {

        public MobCoords()
        {
            Desc = "Shows mob coordinates: usuage: mobcoords <Boolean> ";
            index = 3;
            usage = "mobcoords";
        }
        public override String Execute(String[] args)
        {
            Game1.console.mobCooords = Convert.ToBoolean(args[1].ToLower());
            return Game1.console.mobCooords ? "Mob Coords enabled" : "Mob Coords disabled";
        }

    }
    class Save : Command
    {

        public Save()
        {
            Desc = "Saves current game: usuage: Save";
            index = 4;
            usage = "save";
        }
        public override String Execute(String[] args)
        {
            //Game1.saves.SaveAll(Game1.selectedSave, Game1.player, Game1.mobManager);
            return "Game saved";
        }

    }
    class Load : Command
    {

        public Load()
        {
            Desc = "Load a game: usuage: load <selected save>";
            index = 5;
            usage = "load";
        }
        public override String Execute(String[] args)
        {
            // Game1.chunks = Game1.saves.loadSave(Convert.ToInt32(args[1]), Game1.currentChunkNumber,  Game1.player, Game1.mobManager);
            Managers.MenuRef.state = Managers.MenuRef.GameStates.Playing;
            return "Game Loaded " + args[1];
        }

    }
    class Exit : Command
    {

        public Exit()
        {
            Desc = "Exits the console: usuage: exit";
            index = 6;
            usage = "exit";
        }
        public override String Execute(String[] args)
        {
            Game1.console.isShown = false;
            return "Console exited ";
        }

    }
    class Give : Command
    {

        public Give()
        {
            Desc = "Gives the player an item: usuage: give <block> <hotbarslot> <count>";
            index = 7;
            usage = "give";
        }
        public override String Execute(String[] args)
        {
            args[1] = args[1].ToLower();
            String first = args[1].Substring(0, 1).ToUpper();
            args[1] = first + args[1].Substring(1);
            args[1] = "MineBlock.Blocks." + args[1];

            Type hai = Type.GetType(args[1]);
            ConstructorInfo ctor = hai.GetConstructor(new[] { typeof(int), typeof(int) });
            Block instance = (Block)ctor.Invoke(new object[] { (Convert.ToInt32(args[2]) * 40) + 16, 16 });

            //Game1.player.hotbar[Convert.ToInt32(args[2])] = new Block().returnBlock(Convert.ToInt32(args[1]), (Convert.ToInt32(args[2]) * 40) + 16, 16);
            Game1.player.hotbar[Convert.ToInt32(args[2])] = instance.ItemBlock();//.Reset((Convert.ToInt32(args[2]) * 40) + 16, 16);
            Game1.player.hotbar[Convert.ToInt32(args[2])].Count = Convert.ToInt32(args[3]);
            return "Given player  " + Convert.ToInt32(args[3]) + " of " + instance;//.Reset(0,0); ;
        }

    }
    class Goto : Command
    {

        public Goto()
        {
            Desc = "goto the selected chunk: usuage: goto <chunk>";
            index = 8;
            usage = "goto";
        }
        public override String Execute(String[] args)
        {
            // Game1.switchChunk(Game1.player, Convert.ToInt32(args[1]));
            return "Switched to" + Convert.ToInt32(args[1]);
        }

    } //Deprocated
    class Tp : Command
    {

        public Tp()
        {
            Desc = "Tps to the blocks given: usuage: Tp <x> <y>";
            index = 9;
            usage = "tp";
        }
        public override String Execute(String[] args)
        {


           /* if (args[1][0] == '~')
            {
                args[1] = args[1].PadRight(2, '0');
                args[1] = "" + ((Game1.player.Player.Location.X / 40) + Convert.ToInt32(args[1].Substring(1)));
            }
            if (args[2][0] == '~')
            {
                args[2] = args[2].PadRight(2, '0');
                args[2] = "" +((Game1.player.Player.Location.Y / 40) + Convert.ToInt32(args[2].Substring(1)));
            }
            */
            
            Game1.player.Player.Location = new Microsoft.Xna.Framework.Vector2(Convert.ToSingle(args[1]) * 40, Convert.ToSingle(args[2]) * 40);
            return "Tp to " + new Microsoft.Xna.Framework.Vector2(Convert.ToSingle(args[1]), Convert.ToSingle(args[2]));
        }

    }
    class GetBlock : Command
    {

        public GetBlock()
        {
            Desc = "Gets the blocks indexs given: usuage: getblocks";
            index = 10;
            usage = "getblocks";
        }
        public override String Execute(String[] args)
        {
            Console.WriteLine(new Blocks.Air(0, 0).ToString().Substring(17) + 0);
            for (int i = 0; i < 254; i++)
            {
                Block block = new Block().returnBlock(i, 0, 0);
                if (block.index != 0)
                    Console.WriteLine(block.ToString().Substring(17) + " " + block.index);
            }
            return "Printed Blocks";
        }

    }
    class WriteLine : Command
    {

        public WriteLine()
        {
            Desc = "Writes a line to console given: usagw : writeline <text> ";
            index = 11;
            usage = "writeline";
        }
        public override String Execute(String[] args)
        {
            String output = "";
            foreach (String arg in args)
                output += arg + " ";
            output = output.Substring(10);
            //Game1.console.history.Add(output.ToLower());
            Console.WriteLine(output.ToLower());
            return "Wrote " + output;
        }

    }
    class Write : Command
    {

        public Write()
        {
            Desc = "Writes to console given: usage : write <text> ";
            index = 12;
            usage = "write";
        }
        public override String Execute(String[] args)
        {
            String output = "";
            foreach (String arg in args)
                output += arg + " ";
            output = output.Substring(6);
            Console.Write(output.ToLower());
            return "Wrote " + output;
        }

    }
    class Clear : Command
    {

        public Clear()
        {
            Desc = "Clears the console: usage : clear";
            index = 13;
            usage = "clear";
        }
        public override String Execute(String[] args)
        {
            Game1.console.history.Clear();
            return "Console Cleared";
        }

    }
    class ToggleDownfall : Command
    {

        public ToggleDownfall()
        {
            Desc = "Toggles Downfall: usage : toggledownfall";
            index = 14;
            usage = "toggledownfall";
        }
        public override String Execute(String[] args)
        {
            if (!Game1.weather.isPercipitationing())
                Game1.toggleDownfall(Game1.renderXStart);
            else Game1.weather.Stop();
            return "Toggled Downfall";
        }

    }
    class AddtoCB : Command
    {

        public AddtoCB()
        {
            Desc = "Adds command to command block: usage : addtocb <x> <y> <command>";
            index = 14;
            usage = "addtocb";
        }
        public override String Execute(String[] args)
        {
            try
            {
                Commandblock cb = (Commandblock)Chunk.getBlockAt(Game1.Loadedchunks, Convert.ToInt32(args[1]), Convert.ToInt32(args[2]));
                if (args.Length > 2)
                    cb.command = new String[args.Length - 2];
                for (int i = 3; i < args.Length; i++)
                    cb.command[i - 3] = args[i];

                return "Added Command";
            }
            catch (System.InvalidCastException) { return "Block is not a command block"; }
        }

    }
    class DeleteSave : Command
    {

        public DeleteSave()
        {
            Desc = "Deletes a save: usage : deletesave <save>";
            index = 15;
            usage = "deletesave";
        }
        public override String Execute(String[] args)
        {
            System.IO.Directory.SetCurrentDirectory(@"C:\Users\Anthony\Documents\SavedGames\MineBlock");
            System.IO.Directory.Delete(@"MineBlock" + args[1].ToString(), true);
            return "Save Deleted";
        }

    }
    class Fuckoff : Command  //Depricated 
    {
        Thread fuck;
        public Fuckoff()
        {
            Desc = "Fucken god: usage : fuckoff";
            index = 16;
            usage = "fuckoff";
        }
        public void stuff()
        {
            for (int i = 0; i < Game1.Loadedchunks.Count; i++) //Needs rethinking
                for (int j = 0; j < Game1.Loadedchunks.Count; j++)
                {
                    Chunk.SetBlock(Game1.Loadedchunks, i, j, Chunk.getBlockAt(Game1.Loadedchunks, Game1.randy.Next(0, 200), Game1.randy.Next(0, 130)).Reset(i, j));
                    Thread.Sleep(00001);
                }
            for (int i = 0; i < 200; i++)
                for (int j = 0; j < 130; j++)
                {
                    Chunk.getBlockAt(Game1.Loadedchunks, i, j).x = i;
                    Chunk.getBlockAt(Game1.Loadedchunks, i, j).y = j;
                    Chunk.getBlockAt(Game1.Loadedchunks, i, j).isfucked = true;

                }

            // Game1.player.updateBlocks(Game1.chunk);
        }
        public override String Execute(String[] args)
        {
            Game1.console.isShown = false;
            //SoundEffects.Fuck.Play();
            fuck = new Thread(new ThreadStart(this.stuff));
            fuck.Start();

            return "No you fuck off";
        }

    }
    class TmCalls : Command
    {

        public TmCalls()
        {
            Desc = "Prints number of Texture manager Calls: usage : tmcalls";
            index = 17;
            usage = "tmcalls";
        }

        public override String Execute(String[] args)
        {

            return Tm.getCalls().ToString();
        }

    }
    class RenderDistance : Command
    {

        public RenderDistance()
        {
            Desc = "Edits the amount of blocks drawn: usage : RenderDistance <#>";
            index = 18;
            usage = "renderdistance";
        }

        public override String Execute(String[] args)
        {
            Game1.RenderDistance = Convert.ToInt32(args[1]);
            return "Render Distance set to " + Game1.RenderDistance;
        }

    }
}
