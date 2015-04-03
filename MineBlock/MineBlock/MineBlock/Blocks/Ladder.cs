using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace MineBlock.Blocks
{
    class Ladder : Block
    {
        Boolean manualDraw = false;
        int ydub;
        int added = 0;
        public Ladder(int XPos, int yPos) : base()
        {
            x = XPos;
            y = yPos;
            index = 83;
            MineTime = 30;
            isSolid = false;
        }
        public override void update(Block[,] blocks)
        {
            if (y < 130)
                if (blocks[x, y + 1].index == 0 || blocks[x, y + 1].index == 14 || blocks[x, y + 1].index == 11)
                {
                    manualDraw = true;

                    ydub = (y * 40) + added;
                    if (ydub < (y + 1) * 40)
                        added += 4;
                    else
                    {
                        //manualDraw = false;
                        y = y + 1;
                        blocks[x, y] = new Ladder(x, y);
                        blocks[x, y - 1] = new Air(x, y - 1);
                    }
                }
            base.update(blocks);
        }
         public override Block Reset(int X, int Y)
        {
            return new Ladder(X, Y);
        }
         public override Block Place(int x, int y)
         {

             return new Ladder(x, y);
         }

         public override Block Mine(int x, int y)
         {

             return new Ladder(x, y);
         }
         public override void Draw(SpriteBatch batch)
         {
             if (index > 15)
             {
                 if (manualDraw)
                 {
                     int indexY = index / 16;
                     int indexX = index % 16;

                     batch.Draw(terrainsheet, new Vector2((x * 40), ydub), new Rectangle(indexX * 40, indexY * 40, 40, 40), Color.White);
                 }
                 else
                 {
                     int indexY = index / 16;
                     int indexX = index % 16;
                     batch.Draw(terrainsheet, new Vector2((x * 40), (y * 40)), new Rectangle(indexX * 40, indexY * 40, 40, 40), Color.White);
                 }
             }
             else
                 batch.Draw(terrainsheet, new Vector2((x * 40), (y * 40)), new Rectangle(index * 40, 0, 40, 40), Color.White);
             handleBlockDmg(batch);
         }
    }
}
