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
        public Dirt(int XPos, int yPos) : base()
        {
            x = XPos;
            y = yPos;
            index = 2;
            MineTime = 60;
            preferedTool = new MineBlock.Items.Shovel(0);
        }
        public override void update(List<Chunk> chunks)
        {
            if (Game1.randy.Next(0, 100) == 8 && y != 0)
                if (Chunk.getBlockAt(chunks, x, y - 1).index == 0) Chunk.SetBlock(chunks, x, y, new Grass(Chunk.getBlockAt(chunks, x, y).x, Chunk.getBlockAt(chunks, x, y).y));

            base.update(chunks);
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
