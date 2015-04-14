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
        public override void update(Chunk[,] chunks)
        {
            if (Game1.randy.Next(0, 30) == 5)
            {
                if (x > 0)
                {
                    if (Chunk.CalculateChunk(chunks, x - 1, y).index == 0)
                        Chunk.PlaceBlock(chunks, x - 1, y, new Lava(x - 1, y));
                    if (Chunk.CalculateChunk(chunks, x - 1, y).index == 14)
                        Chunk.PlaceBlock(chunks, x, y, new BedRock(x, y));
                }
                if (x < chunks.GetLength(0))
                {
                    if (Chunk.CalculateChunk(chunks, x + 1, y).index == 0)
                        Chunk.PlaceBlock(chunks, x + 1, y, new Lava(x + 1, y));
                    if (Chunk.CalculateChunk(chunks, x + 1, y).index == 14)
                        Chunk.PlaceBlock(chunks, x, y, new Lava(x, y));
                }
                if (y < chunks.GetLength(1))
                {
                    if (Chunk.CalculateChunk(chunks, x, y + 1).index == 0)
                        Chunk.PlaceBlock(chunks, x, y + 1, new Lava(x, y + 1));
                    if (Chunk.CalculateChunk(chunks, x, y + 1).index == 14)
                        Chunk.PlaceBlock(chunks, x, y, new Lava(x, y));
                }
                if (y > 0)
                {

                    if (Chunk.CalculateChunk(chunks, x, y - 1).index == 14)
                        Chunk.PlaceBlock(chunks, x, y, new BedRock(x, y));
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
