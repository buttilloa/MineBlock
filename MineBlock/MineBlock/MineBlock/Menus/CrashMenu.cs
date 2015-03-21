using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MineBlock.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineBlock.Menus
{
    class CrashMenu :BaseMenu
    {
        
        String Exception, Stacktrace;
        Rectangle Back = new Rectangle(10, 30, 50, 20);
        public CrashMenu(String message, String Stacktrace) 
        {
            DrawStars = false;
            setError(message, Stacktrace);
        }
        public override void getTextures()
        {
            Blank = Tm.getTexture(Tm.Texture.Blank);
            Background = Tm.getTexture(Tm.Texture.options);
            base.getTextures();
        }
        public override void Update()
        {
            if (Cursor.Intersects(Back))
            {
                CursorTouching = 1;
                if (HandleInputs.LeftTrigger())
                {
                    Game1.mobManager = new MobManager();
                    Game1.player = new PlayerManager(Tm.getTexture(Tm.Texture.playerSheet), Tm.getTexture(Tm.Texture.hotbarsheet), Tm.getTexture(Tm.Texture.hotbarselector));
                    MenuRef.state = MenuRef.GameStates.TitleScreen;
                    MenuRef.SetMenu(new TitleScreen());
                }
            }
            else if (CursorTouching != 0) CursorTouching = 0;
            
            base.Update();
        }
        public override void disposeMenu()
        {
            Exception = "";
            Stacktrace = "";
            base.disposeMenu();
        }
        public void setError(String message, String Stacktrace)
        {
            this.Exception = message;
            this.Stacktrace = Stacktrace;
        }
        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(Background, new Rectangle(0, 0, GameWindow.Width, GameWindow.Height), Color.White);
            batch.DrawString(pericles14, "Unhandled Exception has occured", new Vector2(400 - 160, 50), Color.White);
            batch.Draw(Blank, new Rectangle(5, 100, 790, 300), Color.White);
            batch.DrawString(pericles1, Exception, new Vector2(5, 100), Color.Black);
            batch.DrawString(pericles1, Stacktrace, new Vector2(5, 115), Color.Black);
            batch.DrawString(pericles14, "Back", new Vector2(Back.X,Back.Y), CursorTouching ==1 ? Color.Purple : Color.White);
            batch.Draw(Pointer, new Rectangle((int)cursorPos.X, (int)cursorPos.Y, 12, 19), Game1.cursorColor);
       
           
        }
        
    }
}
