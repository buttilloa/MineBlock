using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MineBlock.Managers;
using MineBlock.Commands;
namespace MineBlock.Blocks
{
    class Commandblock : Block
    {
        public string[] command;
        public Commandblock(int XPos, int yPos)
        {
            x = XPos;
            y = yPos;
            index = 158;
            command = new string[0];
        }
        public override void update(Block[,] blocks)
        {
            if (index != 158)
                if (Game1.randy.Next(0, 80) == 4)
                    index = 158;
        }
        public override void EntityStandingEvent(object caller)
        {
            if (caller is PlayerManager)
            {
                Activate();
                this.index = 159;
            }
        }
        public virtual void Activate()
        {
         if(index == 158)
         {
             
             foreach (Command cmd in Game1.console.cmds)
                 if (command[0] == cmd.usage.ToUpper())
                 {
                     try
                     {
                         Console.WriteLine(cmd.Execute(command).ToLower());
                     }
                     catch (System.IndexOutOfRangeException)
                     {
                         Console.WriteLine("invalid arguments: " + cmd.Desc);
                     }
                     break;
                 }
         }
        }
        public override Block Place(int x, int y)
        {

            return new Commandblock(x, y);
        }

        public override Block Mine(int x, int y)
        {

            return new Commandblock(x, y);
        }
        public override Block Reset(int X, int Y)
        {
            return new Commandblock(X, Y);
        }
    }
}
