using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace MineBlock.Blocks
{
    class Lava : Block
    {
        public Lava(int XPos, int yPos)
        {
            x = XPos;
            y = yPos;
            index = 11;
            canMine = false;
            isSolid = false;
        }
        public override void update(Block[,] blocks)
        {
            if (Game1.randy.Next(0, 30) == 5)
            {
                if (x > 0)
                {
                    if (blocks[x - 1, y].index == 0)
                        blocks[x - 1, y] = new Lava(x - 1, y);
                    if (blocks[x - 1, y].index == 14)
                        blocks[x, y] = new BedRock(x, y);
                }
                if (x < 199)
                {
                    if (blocks[x + 1, y].index == 0)
                        blocks[x + 1, y] = new Lava(x + 1, y);
                    if (blocks[x + 1, y].index == 14)
                        blocks[x, y] = new Lava(x, y);
                }
                if (y < 129)
                {
                    if (blocks[x, y + 1].index == 0)
                        blocks[x, y + 1] = new Lava(x, y + 1);
                    if (blocks[x, y + 1].index == 14)
                        blocks[x, y] = new Lava(x, y);
                }
                if (y > 0)
                {
                   
                    if (blocks[x, y-1].index == 14)
                        blocks[x, y] = new BedRock(x, y);
                }
            }
        }

        public override Block Reset(int X, int Y)
        {
            return new Lava(X, Y);
        }
        public override Block Place(int x, int y)
        {

            return new Lava(x, y);
        }

        public override Block Mine(int x, int y)
        {

            return new Stone(x, y);
        }
    }
}
