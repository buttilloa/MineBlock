using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace MineBlock.Blocks
{
    class Dirt : Block
    {
        public Dirt(int XPos, int yPos)
        {
            x = XPos;
            y = yPos;
            index = 2;
            MineTime = 60;
        }
        public override void update(Block[,] blocks)
        {
         if (Game1.randy.Next(0, 100) == 8&& y !=0 )
             if (blocks[x, y - 1].index == 0) blocks[x, y] = new Grass(blocks[x, y].x, blocks[x, y].y);

         base.update(blocks);
        }
        public override Block Place(int x, int y)
        {

            return new Dirt(x, y);
        }

        public override Block Mine(int x, int y)
        {

            return new Dirt(x, y);
        }
        public override Block Reset(int X, int Y)
        {
            return new Dirt(X, Y);
        }
    }
}
