using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineBlock.Mobs
{
    public class Cow : Mob
    {

        Texture2D cowsheet;
        public Cow(int xPos, int yPos, int Chunk)
        {
            CurrentChunk = Chunk;
            Position = new Vector2(xPos, yPos);
            name = 2;
           cowsheet= Tm.getTexture(Tm.Texture.cowsheet);
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

                batch.Draw(cowsheet, new Vector2(((Position.X * 40) + subPixel.X) - 19, ((Position.Y * 40) + subPixel.Y)), new Rectangle(0, 0, 96, 100), Color.White, 0f, Vector2.Zero, 0.4f, flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
      
            }
        }
    }
}
