using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace MineBlock
{
    public class FrameRateCounter : DrawableGameComponent
    {

        SpriteBatch spriteBatch;


        int frameRate = 0;
        int frameCounter = 0;
        TimeSpan elapsedTime = TimeSpan.Zero;
        SpriteFont pericles1;

        public FrameRateCounter(Game game)
            : base(game)
        {
         
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            spriteBatch = new SpriteBatch(GraphicsDevice);  
            pericles1 = Tm.getFont(Tm.Font.f1);
        }

        public override void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromSeconds(1))
            {
                elapsedTime -= TimeSpan.FromSeconds(1);
                frameRate = frameCounter;
                frameCounter = 0;
            }
            if (pericles1 == null) pericles1 = Tm.getFont(Tm.Font.f1);
        }


        public override void Draw(GameTime gameTime)
        {
            frameCounter++;
           
            string fps = string.Format("fps: {0}", frameRate);

            spriteBatch.Begin();

            if(pericles1 != null)spriteBatch.DrawString(pericles1, fps, new Vector2(1, -1), Color.Black);
            if (pericles1 != null) spriteBatch.DrawString(pericles1, fps, new Vector2(0, -2), Color.White);
           
            spriteBatch.End();
        }
    }
}