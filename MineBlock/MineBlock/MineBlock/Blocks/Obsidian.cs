using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace MineBlock.Blocks
{
    class Obsidian : Block
    {
        public Obsidian(int XPos, int yPos)
        {
            x = XPos;
            y = yPos;
             index = 37;
             canMine = false;
        }
         public override Block Reset(int X, int Y)
         {
             return new Obsidian(X, Y);
         }
         public override Block Place(int x, int y)
         {

             return new Obsidian(x, y);
         }

         public override Block Mine(int x, int y)
         {

             return new Obsidian(x, y);
         }
    }
}
