using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace MineBlock.Blocks
{
    class 
        Sand: Block
    {
        Boolean manualDraw = false;
        int ydub;
        int added =0;
        public Sand(int XPos, int yPos) : base()
        {
            x = XPos;
            y = yPos;
            index = 18;
            MineTime = 60;
            preferedTool = new MineBlock.Items.Shovel(0);
        }
        public override void update(Chunk[,] chunks)
        {
            if (y < chunks.GetLength(1)*20)
                if (!Chunk.getBlockAt(chunks, x, y + 1).isSolid)
                {
                    manualDraw = true;

                    ydub = (y * 40) + added;
                    if (ydub < (y + 1) * 40)
                        added += 4;
                    else
                    {
                        //manualDraw = false;
                        y = y + 1;
                        Chunk.SetBlock(chunks, x, y, new Sand(x, y));
                        Chunk.SetBlock(chunks, x, y - 1, new Air(x, y - 1));
                        
                    }
                }
            base.update(chunks);
        }

        public override Block Reset(int X, int Y)
        {
            return new Sand(X, Y);
        }
        public override Block Place(int x, int y)
        {

            return new Sand(x, y);
        }

        public override Block Mine(int x, int y)
        {

            return new Sand(x, y);
        }
        public override void Draw(SpriteBatch batch)
        {
            if (index > 15)
            {
                if (manualDraw)
                {
                    int indexY = index / 16;
                    int indexX = index % 16;

                    batch.Draw(terrainsheet, new Vector2( (x * 40),   ydub), new Rectangle(indexX * 40, indexY * 40, 40, 40), Color.White);
                }
                else
                {
                    int indexY = index / 16;
                    int indexX = index % 16;
                    batch.Draw(terrainsheet, new Vector2(  (x * 40),  (y * 40)), new Rectangle(indexX * 40, indexY * 40, 40, 40), Color.White);
                }
            }
            else
                batch.Draw(terrainsheet, new Vector2(  (x * 40),   (y * 40)), new Rectangle(index * 40, 0, 40, 40), Color.White);
            handleBlockDmg(batch);
        }
    }
}
