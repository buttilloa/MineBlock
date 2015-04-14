using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineBlock
{
    public class Weather
    {
        bool isRaining = false;
        bool isSnowing = false;
        int startPos;
        List<Rectangle> snows = new List<Rectangle>();

        double SnowTime = 0;
        List<Rectangle> rains = new List<Rectangle>();
#if WINDOWS
        int rainCount = 600;
        int snowCount = 500;
        Texture2D Blank;
#endif
#if XBOX
         int rainCount = 200;
         int snowCount = 200;
#endif
        double rainTime = 0;
        public Weather()
        {
            Blank = Tm.getTexture(Tm.Texture.Blank);
        }

        public void Rain()
        {
            rainTime = 0;
            rains.Clear();
            startPos = Game1.renderXStart*40;
            for (int i = 0; i < rainCount; i++)
                rains.Add(GenParticle(false));
            //for (int i = 400; i < (rainCount); i++)
            //    rains.Add(new Rectangle(Game1.randy.Next(10, 780), Game1.randy.Next(-600, -1), 2, 5));
            SoundEffects.Rain.Play();
            isRaining = true;

        }
        public bool isPercipitationing()
        {
            if (isSnowing || isRaining)
                return true;
            return false;
        }
        public void Stop()
        {
            rains.Clear();
            snows.Clear();
            isSnowing = false;
            isRaining = false;
           
            SoundEffects.Rain.Stop(true);
            SoundEffects.Snow.Stop(true);
        }
        public void Snow()
        {
            SnowTime = 0;
            snows.Clear();
            startPos = Game1.renderXStart*40;
            for (int i = 0; i < snowCount; i++)
                snows.Add(GenParticle(true));//new Rectangle(Game1.randy.Next(10, 770), Game1.randy.Next(-600, -1), 3, 3));
            //for (int i = 400; i < (snowCount); i++)
            //    snows.Add(new Rectangle(Game1.randy.Next(10, 780), Game1.randy.Next(-600, -1), 3, 3));
            SoundEffects.Snow.Play();
            isSnowing = true;
        }
        public Rectangle GenParticle(bool Snow)
        {
          
            if(!Snow)
            return new Rectangle(startPos + Game1.randy.Next(-10, 1000), Game1.randy.Next(-600, -1), 2, 5);
            return new Rectangle(startPos + Game1.randy.Next(-10, 1000), Game1.randy.Next(-600, -1), 3, 3);
        }
        public void update(double elaspedSeconds)
        {
            if (isSnowing)
            {
                for (int i = 0; i < snows.Count; i++)
                {
                    snows[i] = new Rectangle(snows[i].X, snows[i].Y + Game1.randy.Next(2, 4), 2, 5);
                    int x = snows[i].X / 40;
                    int y = (snows[i].Y + 5) / 40;
                    if (x >= 200 && y >= 130) snows[i] = GenParticle(true);
                    if (x > -1 && y > -1)
                    {
                        Block check = Chunk.CalculateChunk(Game1.chunks,x, y);
                        if (check.isSolid || check.index == 53)
                            if (SnowTime < SoundEffects.SnowDuration)
                                snows[i] = GenParticle(true);
                            else snows.RemoveAt(i);
                    }
                }
                SnowTime+= elaspedSeconds;
               
                
                if (snows.Count ==0) Stop();

            }
            else if (isRaining)
            {
                for (int i = 0; i < rains.Count; i++)
                {
                    rains[i] = new Rectangle(rains[i].X, rains[i].Y + Game1.randy.Next(3, 6), 2, 5);
                    int x = rains[i].X / 40;
                    int y = (rains[i].Y + 5) / 40;
                    if (x >= 200 && y >= 130) rains[i] = GenParticle(false);
                    if (x > -1 && y > -1)
                    {
                        Block check = Chunk.CalculateChunk(Game1.chunks, x, y);
                        if (check.isSolid || check.index == 53)
                            if (rainTime < SoundEffects.RainDuration)
                                rains[i] = GenParticle(false);
                            else  rains.RemoveAt(i);
                    }
                }
                rainTime += elaspedSeconds;
                if (rains.Count ==0) Stop();
            }
        }
        public void Draw(SpriteBatch batch)
        {
            if (isSnowing) foreach (Rectangle snow in snows)
                    batch.Draw(Blank, snow, Color.White);
            else if (isRaining) foreach (Rectangle rain in rains)
                    batch.Draw(Blank, rain, Color.Blue);
        }

    }
}
