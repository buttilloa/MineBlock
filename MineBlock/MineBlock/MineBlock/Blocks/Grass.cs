using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace MineBlock.Blocks
{
    class Grass : Block
    {
        public Grass(int XPos, int yPos)
        {
            x = XPos;
            y = yPos;
            index = 3;
            special = 0;
            MineTime = 60;
        }
        public Grass(int XPos, int yPos, int orientation)
        {
            x = XPos;
            y = yPos;
            index = 3;
            special = orientation;
            MineTime = 60;

        }
         public override void update(Block[,] blocks)
         {
             if (x < 199 && x > 0 && Game1.randy.Next(0, 100) == 8)
             {
                 if (blocks[x + 1, y].index == 0 && blocks[x - 1, y].index == 0) blocks[x, y] = blocks[x, y].ConvertGrass(x, y, 3);
                 else if (blocks[x + 1, y].index == 0) blocks[x, y] = blocks[x, y].ConvertGrass(x, y, 2);
                 else if (blocks[x - 1, y].index == 0) blocks[x, y] = blocks[x, y].ConvertGrass(x, y, 1);
                 else blocks[x, y] = new Grass(x, y, 0);
                 
                 if (blocks[x, y - 1].index != 0) blocks[x, y] = new Dirt(blocks[x, y].x, blocks[x, y].y);
             }
             base.update(blocks);
         }
        public override void Draw(SpriteBatch batch, int startposX, int startposY)
        {
            batch.Draw(Game1.grass, new Vector2(startposX + (x * 40), startposY + (y * 40)), new Rectangle((special * 40), 0, 40, 40), Color.White);
            handleBlockDmg(batch, startposX, startposY);
        }
        public override Block Reset(int X, int Y)
        {
            return new Grass(X, Y);
        }
        public override Block Place(int x, int y)
        {

            return new Grass(x, y);
        }

        public override Block Mine(int x, int y)
        {

            return new Dirt(x, y);
        }
        public override Block ConvertGrass(int X, int Y, int orientation)
        {
            return new Grass(X, Y, orientation);
        }
       
    }
}
