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

        Texture2D chickensheet, chickenleg;
        Vector2 LegBend = new Vector2(10, 0);
        float rotation = 0f;
        bool extend = true;
        public Chicken(int xPos, int yPos, int Chunk)
        {
            CurrentChunk = Chunk;
            Position = new Vector2(xPos, yPos);
            name = 1;
            chickensheet = Tm.getTexture(Tm.Textures.chickensheet);
            chickenleg = Tm.getTexture(Tm.Textures.chickenLeg);
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
            if (flip)
            {
                batch.Draw(chickenleg, new Vector2(((Position.X * 40) + subPixel.X) - 19 + 18, ((Position.Y * 40) + subPixel.Y) + 15 + 16.5f), new Rectangle(0, 0, 10, 20), Color.White, rotation, LegBend, 0.4f, SpriteEffects.FlipHorizontally, 0f);
                batch.Draw(chickenleg, new Vector2(((Position.X * 40) + subPixel.X) - 19 + 18, ((Position.Y * 40) + subPixel.Y) + 15 + 16.5f), new Rectangle(0, 0, 10, 20), Color.White, -rotation, LegBend, 0.4f, SpriteEffects.FlipHorizontally, 0f);
            }
            else
            {
                batch.Draw(chickenleg, new Vector2(((Position.X * 40) + subPixel.X) - 19 + 6, ((Position.Y * 40) + subPixel.Y) + 15 + 16.5f), new Rectangle(0, 0, 10, 20), Color.White, rotation, LegBend, 0.4f, SpriteEffects.None, 0f);
                batch.Draw(chickenleg, new Vector2(((Position.X * 40) + subPixel.X) - 19 + 6, ((Position.Y * 40) + subPixel.Y) + 15 + 16.5f), new Rectangle(0, 0, 10, 20), Color.White, -rotation, LegBend, 0.4f, SpriteEffects.None, 0f);

            }
            batch.Draw(chickensheet, new Vector2(((Position.X * 40) + subPixel.X) - 19, ((Position.Y * 40) + subPixel.Y) + 15), new Rectangle(0, 0, 48, 40), Color.White, 0f, Vector2.Zero, 0.4f, flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);



        }
    }
}
