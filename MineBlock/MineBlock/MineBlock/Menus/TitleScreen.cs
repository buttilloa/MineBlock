using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MineBlock.Managers;
using MineBlock.Mobs;
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
        List<Pig> mobs = new List<Pig>();
        string[] Splashs;
        int currentSplash = 1;
        float Splashsize = 1;
        bool increase = true;
        public TitleScreen()
            : base()
        {
            Game1.mobManager = new MobManager();
            Game1.player = new PlayerManager();
            MenuRef.state = MenuRef.GameStates.TitleScreen;
            Splashs = new string[6];
            Splashs[0] = "You looked different through your window";
            Splashs[1] = "I really wish you would stop reading and play";
            Splashs[2] = "GG m8 get rekt";
            Splashs[3] = "Hold up grab the wall";
            Splashs[4] = "Now with blocks!";
            Splashs[5] = "What a wonderful time to be alive!";

            currentSplash = Game1.randy.Next(0, Splashs.Count());

            mobs.Add(new Pig(0, 9, true));
        }
        public override void getTextures()
        {
            Background = Tm.getTexture(Tm.Texture.TitleScreen);
            base.getTextures();
        }
        public override void disposeMenu()
        {
            for (int i = mobs.Count - 1; i > 0; i--)
                mobs.RemoveAt(i);
            base.disposeMenu();
        }
        public override void Update()
        {
            if (Cursor.Intersects(StartButton))
                if (HandleInputs.LeftTrigger())
                {
                    MenuRef.SetMenu(new SaveSelect());
                }
            if (Cursor.Intersects(OptionsButton))
                if (HandleInputs.LeftTrigger())
                {
                    MenuRef.SetMenu(new Options());
                }
            foreach (Pig pig in mobs)
            {
                pig.update(new GameTime());
                if (mobs.Count < 15 && pig.addMore) { mobs.Add(new Pig(0, 9, true)); pig.addMore = false; break; }

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
            foreach (Pig pig in mobs)
                pig.Draw(batch);
            base.Draw(batch);

        }
    }
}
