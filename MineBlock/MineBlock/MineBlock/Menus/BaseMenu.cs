using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineBlock.Menus
{
    public class BaseMenu
    {
        protected bool DrawStars = true;
        protected Vector2 cursorPos = new Vector2(0, 0);
        protected Rectangle Cursor;
        List<Rectangle> Stars = new List<Rectangle>();
        protected Rectangle GameWindow;
        protected int CursorTouching = 0;
        protected Texture2D Background, Pointer, SaveSelectHighlight, Blank;
        protected SpriteFont pericles14, pericles1;
        public BaseMenu()
        {
            GameWindow = Game1.Instance.Window.ClientBounds;
            getTextures();
        }
        public virtual void disposeMenu()
        {

        }
        public virtual void getTextures()
        {
            Pointer = Tm.getTexture(Tm.Texture.Pointer);
            pericles1 = Tm.getFont(Tm.Font.f1);
            pericles14 = Tm.getFont(Tm.Font.f14);
            SaveSelectHighlight = Tm.getTexture(Tm.Texture.SaveSelectHighlight);
            Blank = Tm.getTexture(Tm.Texture.Blank);
        }
        public virtual void Update()
        {
            cursorPos = new Vector2(HandleInputs.moveHighlighter(cursorPos).X, HandleInputs.moveHighlighter(cursorPos).Y);
            Cursor = new Rectangle((int)cursorPos.X, (int)cursorPos.Y, 3, 3);

        }
        public void drawStars(SpriteBatch batch)
        {
            if (DrawStars)
            {

                while (Stars.Count < 100)
                    Stars.Add(new Rectangle(Game1.randy.Next(-1000, 790), Game1.randy.Next(0, 180), 1, 1));
                for (int i = 0; i < Stars.Count; i++)
                {
                    Stars[i] = new Rectangle(Stars[i].X + 1, Stars[i].Y, 1, 1);
                    if (Stars[i].X > 800) Stars[i] = new Rectangle(Game1.randy.Next(-800, 0), Game1.randy.Next(0, 180), 1, 1);
                    batch.Draw(Blank, Stars[i], Stars[i].Y > 100 ? Color.Gray : Color.White);
                }
            }
        }
        public virtual void Draw(SpriteBatch batch)
        {
            drawStars(batch);
        }
    }

}

