using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineBlock.Blocks
{
    class IGravityBlock : Block
    {
        public Boolean manualDraw = false;
        public int ydub, added;
        public override void update(List<Chunk> chunks)
        {

            if (!Chunk.getBlockAt(chunks, x, y + 1).isSolid)
            {
                manualDraw = true;

                ydub = (y * Constants.BlockSize) + added;
                if (ydub < (y + 1) * Constants.BlockSize)
                    added += 4;
                else
                {
                    //manualDraw = false;
                    y = y + 1;
                    Chunk.SetBlock(chunks, x, y, this.returnBlock(this.index,x,y));
                    Chunk.SetBlock(chunks, x, y - 1, new Air(x, y - 1));

                }
            }
            base.update(chunks);
        }
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
        {

            if (manualDraw)
            {
                int indexY = index / 16;
                int indexX = index % 16;

                batch.Draw(terrainsheet, CalcRectangleSpecial(x, ydub), new Rectangle(indexX * 40, indexY * 40, 40, 40), Color.White);
            }
            else
            {
                int indexY = index / 16;
                int indexX = index % 16;
                batch.Draw(terrainsheet, CalcRectangle(x, y), new Rectangle(indexX * 40, indexY * 40, 40, 40), Color.White);
            }


            handleBlockDmg(batch);
        }
        public Rectangle CalcRectangleSpecial(int x, int y)
        {
            return new Rectangle((x * Constants.BlockSize), y, Constants.BlockSize, Constants.BlockSize);
        }

    }
}
