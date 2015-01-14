﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineBlock.Mobs
{
    public class Cow : Mob
    {


        public Cow(int xPos, int yPos, int Chunk)
        {
            CurrentChunk = Chunk;
            Position = new Vector2(xPos, yPos);
            name = 2;
            //sprite = new Sprite(Position, Game1.Pigsheet, new Rectangle(0, 0, 96, 64), Vector2.Zero);
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

                batch.Draw(Game1.cowsheet, new Vector2(((Position.X * 40) + subPixel.X) - 19, ((Position.Y * 40) + subPixel.Y)), new Rectangle(0, 0, 96, 100), Color.White, 0f, Vector2.Zero, 0.4f, flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
      
            }
        }
    }
}