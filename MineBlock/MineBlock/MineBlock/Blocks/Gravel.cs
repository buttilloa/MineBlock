using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace MineBlock.Blocks
{
    class Gravel : Block
    {
        Boolean manualDraw = false;
        int ydub;
        int added =0;
        public Gravel(int XPos, int yPos)
        {
            x = XPos;
            y = yPos;
            index = 19;
            MineTime = 90;
        }
        public override void update(Block[,] blocks)
        {
            if (y < 130)
                if (blocks[x, y + 1].index == 0 || blocks[x, y + 1].index == 14 || blocks[x, y + 1].index == 11)
                {
                    manualDraw = true;
                   
                    ydub = (y*40) + added;
                    if (ydub < (y+1) * 40)
                        added+=4;
                    else
                    {
                        //manualDraw = false;
                        y = y+1;
                        blocks[x, y] = new Gravel(x, y);
                        blocks[x, y - 1] = new Air(x, y - 1);
                    }
                }
            base.update(blocks);
        }

        public override Block Reset(int X, int Y)
        {
            return new Gravel(X, Y);
        }
        public override Block Place(int x, int y)
        {

            return new Gravel(x, y);
        }

        public override Block Mine(int x, int y)
        {

            return new Gravel(x, y);
        }
        public override void Draw(SpriteBatch batch, int startposX, int startposY)
        {
            if (index > 15)
            {
                if (manualDraw)
                {
                    int indexY = index / 16;
                    int indexX = index % 16;

                    batch.Draw(Game1.terrainsheet, new Vector2(startposX + (x * 40), startposY + ydub), new Rectangle(indexX * 40, indexY * 40, 40, 40), Color.White);
                }
                else
                {
                    int indexY = index / 16;
                    int indexX = index % 16;
                    batch.Draw(Game1.terrainsheet, new Vector2(startposX + (x * 40), startposY + (y * 40)), new Rectangle(indexX * 40, indexY * 40, 40, 40), Color.White);
                }
            }
            else
                batch.Draw(Game1.terrainsheet, new Vector2(startposX + (x * 40), startposY + (y * 40)), new Rectangle(index * 40, 0, 40, 40), Color.White);
            handleBlockDmg(batch, startposX, startposY);
        }
    }
}
