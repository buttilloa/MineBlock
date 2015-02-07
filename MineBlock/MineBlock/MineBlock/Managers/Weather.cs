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
        List<Rectangle> snows = new List<Rectangle>();
     
        int SnowTime = 0;
        List<Rectangle> rains = new List<Rectangle>();
#if WINDOWS
        int rainCount = 600;   
        int snowCount = 500;
#endif
#if XBOX
         int rainCount = 200;
         int snowCount = 200;
#endif
        int rainTime = 0;
        public Weather()
        {
           
        }

        public void Rain()
        {
            rainTime = 0;
            rains.Clear();
            for (int i = 0; i < rainCount; i++)
                rains.Add(new Rectangle(Game1.randy.Next(10, 770), Game1.randy.Next(-600, -1), 2, 5));
            //for (int i = 400; i < (rainCount); i++)
            //    rains.Add(new Rectangle(Game1.randy.Next(10, 780), Game1.randy.Next(-600, -1), 2, 5));
            SoundEffects.Rain.Play();
           isRaining = true;
        }
        public bool isPercipitationing(){
            if (isSnowing || isRaining)
                return true;
            return false;
            }
        public void Stop()
        {
            isSnowing = false;
            isRaining = false;
            SoundEffects.Rain.Stop(true);
            SoundEffects.Snow.Stop(true);
        }
        public void Snow()
        {
            SnowTime = 0;
            snows.Clear();
            for (int i = 0; i < snowCount;i++)
                snows.Add(new Rectangle(Game1.randy.Next(10, 770), Game1.randy.Next(-600, -1), 3, 3));
            //for (int i = 400; i < (snowCount); i++)
            //    snows.Add(new Rectangle(Game1.randy.Next(10, 780), Game1.randy.Next(-600, -1), 3, 3));
            SoundEffects.Snow.Play();
            isSnowing = true;
        }
        public void Draw(SpriteBatch batch)
        {
            if (isSnowing)
            {
                for (int i = 0; i < snowCount - 1; i++)
                {
                    if (snows[i].Y < 600)
                        snows[i] = new Rectangle(snows[i].X, snows[i].Y + Game1.randy.Next(1, 4), 3, 3);
                    batch.Draw(Game1.Weather, snows[i], Color.White);
                    foreach (Block block in Game1.chunk)
                        if (block.index != 6 && block.index != 0)
                            if (snows[i].Intersects(new Rectangle(block.x * 40, block.y * 40, 40, 40)))
                            {
                                if (SnowTime < 1600)
                                    snows[i] = new Rectangle(Game1.randy.Next(10, 770), Game1.randy.Next(-20, -1), 2, 5);
                            }
                    if (snows[i].Y > 600 )
                    {
                        if(SnowTime < 1600)
                        snows[i] = new Rectangle(Game1.randy.Next(10, 770), Game1.randy.Next(-20, -1), 3, 3);
                    }

                }
                SnowTime++;
                if (SnowTime > 2000) Stop();
               
            }
            if (isRaining)
            {
                for (int i = 0; i < rainCount - 1; i++)
                {
                    if (rains[i].Y < 600)
                        rains[i] = new Rectangle(rains[i].X, rains[i].Y + Game1.randy.Next(3, 6), 2, 5);
                    batch.Draw(Game1.Weather, rains[i], Color.Blue);
                    for (int x = (int)(Game1.player.Player.Location.X / 40) - 11; x < (Game1.player.Player.Location.X / 40) + 11; x++)
                       for (int y = (int)(Game1.player.Player.Location.Y / 40) - 11; y < (Game1.player.Player.Location.Y / 40) + 11;y++)
                           if (Game1.chunk[x,y].index != 6 && Game1.chunk[x,y].index != 0)
                                    if (rains[i].Intersects(new Rectangle(Game1.chunk[x,y].x * 40, Game1.chunk[x,y].y * 40, 40, 40)))
                                    {
                                        rains[i] = new Rectangle(Game1.randy.Next(10, 770), Game1.randy.Next(-20, -1), 2, 5);
                                        if (rainTime > 1600)
                                            rains[i] = new Rectangle(Game1.randy.Next(10, 770), Game1.randy.Next(-20, -1), 2, 5);
                                    }
                            
                    if (rains[i].Y > 600)
                    {
                        if (rainTime < 1600)
                            rains[i] = new Rectangle(Game1.randy.Next(10, 770), Game1.randy.Next(-20, -1), 2, 5);
                    }

                }
                rainTime++;
              
                if (rainTime > 2000) Stop();

            }
        }
    
    }
}
