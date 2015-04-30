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

        Texture2D cowsheet, cowleg;
        float rotation = 0f;
        bool extend = true;
        Vector2 legBend = new Vector2(8, 0);
        public Cow(int xPos, int yPos, int Chunk)
        {
            CurrentChunk = Chunk;
            Position = new Vector2(xPos, yPos);
            name = 2;
            cowsheet = Tm.getTexture(Tm.Textures.cowsheet);
            cowleg = Tm.getTexture(Tm.Textures.cowleg);
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
            if (Dir != 0)
                if (extend)
                {
                    if (rotation > 1) extend = false;
                    else rotation += .15f;
                }
                else
                    if (rotation < 0f) extend = true;
                    else rotation -= .15f;
            else
                if (rotation > 0f) rotation -= .15f;
                else if (rotation < 0f) rotation = 0f;
            batch.Draw(cowsheet, new Vector2(((Position.X * 40) + subPixel.X) - 19, ((Position.Y * 40) + subPixel.Y)), new Rectangle(0, 0, 96, 56), Color.White, 0f, Vector2.Zero, 0.4f, flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            if (flip)
            {
                batch.Draw(cowleg, new Vector2(((Position.X * 40) + subPixel.X) - 19 + 34, ((Position.Y * 40) + subPixel.Y) + 21), new Rectangle(0, 0, 16, 48), Color.White, rotation, legBend, 0.4f, SpriteEffects.FlipHorizontally, 0f);
                batch.Draw(cowleg, new Vector2(((Position.X * 40) + subPixel.X) - 19 + 34, ((Position.Y * 40) + subPixel.Y) + 21), new Rectangle(0, 0, 16, 48), Color.White, -rotation, legBend, 0.4f, SpriteEffects.FlipHorizontally, 0f);

                batch.Draw(cowleg, new Vector2(((Position.X * 40) + subPixel.X) - 19 + 14, ((Position.Y * 40) + subPixel.Y) + 21), new Rectangle(0, 0, 16, 48), Color.White, rotation, legBend, 0.4f, SpriteEffects.FlipHorizontally, 0f);
                batch.Draw(cowleg, new Vector2(((Position.X * 40) + subPixel.X) - 19 + 14, ((Position.Y * 40) + subPixel.Y) + 21), new Rectangle(0, 0, 16, 48), Color.White, -rotation, legBend, 0.4f, SpriteEffects.FlipHorizontally, 0f);
            }
            else
            {
                batch.Draw(cowleg, new Vector2(((Position.X * 40) + subPixel.X) - 19 + 5, ((Position.Y * 40) + subPixel.Y) + 21), new Rectangle(0, 0, 16, 48), Color.White, rotation, legBend, 0.4f, SpriteEffects.None, 0f);
                batch.Draw(cowleg, new Vector2(((Position.X * 40) + subPixel.X) - 19 + 5, ((Position.Y * 40) + subPixel.Y) + 21), new Rectangle(0, 0, 16, 48), Color.White, -rotation, legBend, 0.4f, SpriteEffects.None, 0f);

                batch.Draw(cowleg, new Vector2(((Position.X * 40) + subPixel.X) - 19 + 25, ((Position.Y * 40) + subPixel.Y) + 21), new Rectangle(0, 0, 16, 48), Color.White, rotation, legBend, 0.4f, SpriteEffects.None, 0f);
                batch.Draw(cowleg, new Vector2(((Position.X * 40) + subPixel.X) - 19 + 25, ((Position.Y * 40) + subPixel.Y) + 21), new Rectangle(0, 0, 16, 48), Color.White, -rotation, legBend, 0.4f, SpriteEffects.None, 0f);
            }
        }
    }
}
