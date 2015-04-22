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
        Texture2D grass;
        public Grass(int XPos, int yPos)
        {
            x = XPos;
            y = yPos;
            index = 3;
            special = 0;
            MineTime = 60;
            preferedTool = new MineBlock.Items.Shovel(0);
            grass = Tm.getTexture(Tm.Texture.grass);
        }
        public Grass(int XPos, int yPos, int orientation , float olddamage)
        {
            x = XPos;
            y = yPos;
            index = 3;
            special = orientation;
            damage = olddamage;

        }
        public override void update(List<Chunk> chunks)
        {
            base.update(chunks);
            if (x < 199 && x > 0 && Game1.randy.Next(0, 100) == 8)
            {
                if (Chunk.getBlockAt(chunks, x + 1, y).index == 0 && Chunk.getBlockAt(chunks, x - 1, y).index == 0)
                {
                    if (special != 3) special = 3;
                }
                else if (Chunk.getBlockAt(chunks, x + 1, y).index == 0)
                {
                    if (special != 2) special = 2;
                }
                else if (Chunk.getBlockAt(chunks, x - 1, y).index == 0)
                {
                    if (special != 1) special = 1;
                }
                else if (special != 0) special = 0;

                if (Chunk.getBlockAt(chunks, x, y - 1).index != 0) Chunk.SetBlock(chunks, x, y, new Dirt(Chunk.getBlockAt(chunks, x, y).x, Chunk.getBlockAt(chunks, x, y).y));
            }

        }
        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(grass, new Vector2( (x * 40),  (y * 40)), new Rectangle((special * 40), 0, 40, 40), Color.White);
            handleBlockDmg(batch);
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
        public override Block ConvertGrass(int X, int Y, int orientation,float olddamage)
        {
            return new Grass(X, Y, orientation,olddamage);
        }
       
    }
}
