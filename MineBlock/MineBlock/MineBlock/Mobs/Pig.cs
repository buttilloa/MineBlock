using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineBlock.Mobs
{
    public class Pig : Mob
    {

        Texture2D PigSheet, Leg;
        Vector2 LegBend = new Vector2(8, 0);
        float rotation = 0f;
        bool extend = true;
        bool NoAi = false;
        public bool addMore = false;
        float moveSpeed;
        public Pig(int xPos, int yPos, int Chunk)
        {
            CurrentChunk = Chunk;
            Position = new Vector2(xPos, yPos);
            name = 3;
            PigSheet = Tm.getTexture(Tm.Texture.Pigsheet);
            Leg = Tm.getTexture(Tm.Texture.PigLeg);
        }
        public Pig(int xPos, int yPos, bool NoAi)
            : base()
        {

            Position = new Vector2(xPos, yPos);
            name = 3;
            PigSheet = Tm.getTexture(Tm.Texture.Pigsheet);
            Leg = Tm.getTexture(Tm.Texture.PigLeg);
            this.NoAi = NoAi;
            Dir = 1;
            subPixel += new Vector2(0, 12);
            moveSpeed = Game1.randy.Next(0, 11) * .1f;
        }
        public override void update(GameTime time)
        {
            if (!NoAi)
                base.update(time);
            else
            {
                if (subPixel.X > 910)
                {
                    subPixel = new Vector2(Game1.randy.Next(-100, 0), subPixel.Y);
                    if (Game1.randy.Next(0, 3) == 1) addMore = true;
                }
                subPixel += new Vector2(1 + moveSpeed, 0);
            }
        }
        public override void Draw(SpriteBatch batch)
        {

            if (Dir != 0)
                if (extend)
                {
                    if (rotation > 1.2f) extend = false;
                    else rotation += .11f;
                }
                else
                    if (rotation < 0f) extend = true;
                    else rotation -= .11f;
            else
                if (rotation > 0f) rotation -= .11f;
                else if (rotation < 0f) rotation = 0f;
            batch.Draw(PigSheet, new Vector2(((Position.X * 40) + subPixel.X) - 19, ((Position.Y * 40) + subPixel.Y) + 15), new Rectangle(0, 0, 96, 40), Color.White, 0f, Vector2.Zero, 0.4f, flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);

            if (flip)
            {
                batch.Draw(Leg, new Vector2(((Position.X * 40) + subPixel.X) - 19 + 37, ((Position.Y * 40) + subPixel.Y) + 15 + 16), new Rectangle(0, 0, 16, 24), Color.White, rotation, LegBend, 0.4f, flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
                batch.Draw(Leg, new Vector2(((Position.X * 40) + subPixel.X) - 19 + 37, ((Position.Y * 40) + subPixel.Y) + 15 + 16), new Rectangle(0, 0, 16, 24), Color.White, -rotation, LegBend, 0.4f, flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);

                batch.Draw(Leg, new Vector2(((Position.X * 40) + subPixel.X) - 19 + 19, ((Position.Y * 40) + subPixel.Y) + 15 + 16), new Rectangle(0, 0, 16, 24), Color.White, rotation, LegBend, 0.4f, flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
                batch.Draw(Leg, new Vector2(((Position.X * 40) + subPixel.X) - 19 + 19, ((Position.Y * 40) + subPixel.Y) + 15 + 16), new Rectangle(0, 0, 16, 24), Color.White, -rotation, LegBend, 0.4f, flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            }
            else
            {
                /*back*/
                batch.Draw(Leg, new Vector2(((Position.X * 40) + subPixel.X) - 19 + 2, ((Position.Y * 40) + subPixel.Y) + 15 + 16), new Rectangle(0, 0, 16, 24), Color.White, rotation, LegBend, 0.4f, flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
                batch.Draw(Leg, new Vector2(((Position.X * 40) + subPixel.X) - 19 + 2, ((Position.Y * 40) + subPixel.Y) + 15 + 16), new Rectangle(0, 0, 16, 24), Color.White, -rotation, LegBend, 0.4f, flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);

                batch.Draw(Leg, new Vector2(((Position.X * 40) + subPixel.X) - 19 + 20, ((Position.Y * 40) + subPixel.Y) + 15 + 16), new Rectangle(0, 0, 16, 24), Color.White, rotation, LegBend, 0.4f, flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
                batch.Draw(Leg, new Vector2(((Position.X * 40) + subPixel.X) - 19 + 20, ((Position.Y * 40) + subPixel.Y) + 15 + 16), new Rectangle(0, 0, 16, 24), Color.White, -rotation, LegBend, 0.4f, flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            }
            
        }
    }
}
