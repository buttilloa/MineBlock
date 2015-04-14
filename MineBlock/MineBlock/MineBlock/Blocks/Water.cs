using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace MineBlock.Blocks
{
    class Water : Block
    {
        public Water(int XPos, int yPos)
        {
            x = XPos;
            y = yPos;
            index = 14;
            canMine = false;
            isSolid = false;
        }
        public override void update(Chunk[,] chunks)
        {
            if (Game1.randy.Next(0, 10) == 5)
            {
                if (x > 0)
                    if (Chunk.getBlockAt(chunks,x - 1, y).index == 0)
                        Chunk.SetBlock(chunks,x - 1, y, new Water(x - 1, y));
                if (x < chunks.GetLength(0))
                    if (Chunk.getBlockAt(chunks, x + 1, y).index == 0)
                        Chunk.SetBlock(chunks, x + 1, y, new Water(x + 1, y));
                if (y < chunks.GetLength(1))
                    if (Chunk.getBlockAt(chunks, x, y+1).index == 0)
                        Chunk.SetBlock(chunks, x, y+1, new Water(x, y+1));
            }
        }

        public override Block Reset(int X, int Y)
        {
            return new Water(X, Y);
        }
        public override Block Place(int x, int y)
        {

            return new Water(x, y);
        }

        public override Block Mine(int x, int y)
        {

            return new Water(x, y);
        }
    }
}
