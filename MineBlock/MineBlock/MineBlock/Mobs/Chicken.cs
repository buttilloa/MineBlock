using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineBlock.Mobs
{
    public class Chicken : Mob
    {

        Texture2D chickensheet;
        public Chicken(int xPos, int yPos, int Chunk)
        {
            CurrentChunk = Chunk;
            Position = new Vector2(xPos, yPos);
            name = 1;
            chickensheet = Tm.getTexture(Tm.Texture.chickensheet);
        }
        public override void update(GameTime time)
        {

            if (Game1.currentChunkNumber == CurrentChunk)
            {
                //sprite.Update(time);
                base.update(time);
            }
        }
        public override void Draw(SpriteBatch batch)
        {
            if (Game1.currentChunkNumber == CurrentChunk)
            {

                batch.Draw(chickensheet, new Vector2(((Position.X * 40) + subPixel.X) - 19, ((Position.Y * 40) + subPixel.Y) + 15), new Rectangle(0, 0, 48, 60), Color.White, 0f, Vector2.Zero, 0.4f, flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
      
            }
        }
    }
}
