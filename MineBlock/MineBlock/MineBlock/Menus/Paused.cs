using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MineBlock.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineBlock.Menus
{
    public class Paused : BaseMenu
    {
        Rectangle Paused1 = new Rectangle(19, 220, 153, 43);
        Rectangle Paused2 = new Rectangle(330, 220, 153, 43);
        Rectangle Paused3 = new Rectangle(628, 220, 153, 43);



        public Paused()
            : base()
        {
            MenuRef.state = MenuRef.GameStates.Paused;
        }
        public override void getTextures()
        {
            Background = Tm.getTexture(Tm.Texture.Paused);

            base.getTextures();
        }
        public override void Update()
        {
            if (Cursor.Intersects(Paused1))
            {
                CursorTouching = 1;
                if (HandleInputs.LeftTrigger())
                {
                    Game1.save.saveAll(Game1.Loadedchunks);
                    Game1.mobManager = new MobManager();
                     MenuRef.SetMenu(new TitleScreen());
                }
            }
            else if (Cursor.Intersects(Paused2))
            {
                CursorTouching = 2;
                if (HandleInputs.LeftTrigger())
                {
                    if (MenuRef.state != MenuRef.GameStates.Playing)
                        MenuRef.SetMenu(MenuRef.getLastMenu());
                    //MenuRef.state = MenuRef.GameStates.Playing;
                    //MenuRef.SetMenu(new TitleScreen());
                }
            }
            else if (Cursor.Intersects(Paused3))
            {
                CursorTouching = 3;
                if (HandleInputs.LeftTrigger())
                {
                    MenuRef.SetMenu(new Options());
                }
            }
            base.Update();
        }
        public override void Draw(SpriteBatch batch)
        {
           
            batch.Draw(Background, new Rectangle(0, 0, GameWindow.Width, GameWindow.Height), Color.White);
            batch.Draw(Pointer, new Rectangle((int)cursorPos.X, (int)cursorPos.Y, 12, 19), Game1.cursorColor);

            batch.DrawString(pericles14, "Exit to Menu", new Vector2(Paused1.X + 13, Paused1.Y + 11), Color.White);
            batch.DrawString(pericles14, "Resume Game", new Vector2(Paused2.X + 13, Paused2.Y + 11), Color.White);
            batch.DrawString(pericles14, "Options", new Vector2(Paused3.X + 22, Paused3.Y + 11), Color.White);
            if (CursorTouching == 1) batch.Draw(SaveSelectHighlight, Paused1, Color.White);
            else if (CursorTouching == 2) batch.Draw(SaveSelectHighlight, Paused2, Color.White);
            else if (CursorTouching == 3) batch.Draw(SaveSelectHighlight, Paused3, Color.White);
            base.Draw(batch);

        }
    }
}
