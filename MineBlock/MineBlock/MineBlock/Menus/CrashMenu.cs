using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineBlock.Menus
{
    class CrashMenu :BaseMenu
    {
        Texture2D Blank;
        String Exception, Stacktrace;
        public CrashMenu(String message, String Stacktrace) 
        {
            DrawStars = false;
            setError(message, Stacktrace);
        }
        public override void getTextures()
        {
            Blank = Game1.Weather;
            Background = Game1.Instance.options;
            base.getTextures();
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
           
        }
        
    }
}
