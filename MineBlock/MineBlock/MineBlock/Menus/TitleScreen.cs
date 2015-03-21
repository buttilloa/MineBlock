using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MineBlock.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineBlock.Menus
{
    public class TitleScreen : BaseMenu
    {
        Rectangle StartButton = new Rectangle(331, 260, 153, 43);
        Rectangle OptionsButton = new Rectangle(579, 291, 153, 43);
        
        string[] Splashs;
        int currentSplash = 1;
        float Splashsize = 1;
        bool increase = true;
        public TitleScreen()
            : base()
        {
            Splashs = new string[6];
            Splashs[0] = "You looked different through your window";
            Splashs[1] = "I really wish you would stop reading and play";
            Splashs[2] = "GG m8 get rekt";
            Splashs[3] = "Hold up grab the wall";
            Splashs[4] = "Now with blocks!";
            Splashs[5] = "What a wonderful time to be alive!";

            currentSplash = Game1.randy.Next(0, Splashs.Count());
           
        }
        public override void getTextures()
        {
            Background = Tm.getTexture(Tm.Texture.TitleScreen);
            base.getTextures();
        }
        public override void Update()
        {
            if (Cursor.Intersects(StartButton))
                if (HandleInputs.LeftTrigger())
                {
                    MenuRef.state = MenuRef.GameStates.SaveSelect;
                    MenuRef.SetMenu(new SaveSelect());
                }
            if (Cursor.Intersects(OptionsButton))
                if (HandleInputs.LeftTrigger())
                {
                    MenuRef.state = MenuRef.GameStates.Options;
                 MenuRef.SetMenu(new Options());
                }
            base.Update();
        }
        public override void Draw(SpriteBatch batch)
        {
           

            batch.Draw(Background, new Rectangle(0, 0, GameWindow.Width, GameWindow.Height), Color.White);
            batch.Draw(Pointer, new Rectangle((int)cursorPos.X, (int)cursorPos.Y, 12, 19), Game1.cursorColor);

            if (increase && Splashsize <= 1.1f) Splashsize += .005f;
            if (Splashsize > 1.1f) increase = false;
            if (!increase && Splashsize >= .9f) Splashsize -= .005f;
            if (Splashsize < .9f) increase = true;
            batch.DrawString(pericles14, Splashs[currentSplash], new Vector2(10, 150), Color.White, -.3f, new Vector2(5, Splashs[currentSplash].Length / 2), Splashsize, SpriteEffects.None, 0f);
            base.Draw(batch);
        }
    }
}
