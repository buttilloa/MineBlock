using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineBlock.Menus
{
    public class Options : BaseMenu
    {
        #region Variables
        int HighlightcurrentColor;
        int HoverBotcolor;
        int hotbatSelectorColor;
        int breakanim;
        int cursorcolor;
        int allColor;
        bool canclick = true;
        Rectangle plus = new Rectangle(200, 100, 40, 40);
        Rectangle HoverBot = new Rectangle(200, 175, 16, 22); int hoverbotframe = 0; bool botfloat = true;
        Rectangle HotbarSelector = new Rectangle(200, 240, 48, 48);
        Rectangle Breakanim = new Rectangle(200, 300, 40, 40); bool Animation = true;
        Rectangle toggleAnim = new Rectangle(242, 320, 40, 20);
        Rectangle MouseCursor = new Rectangle(200, 375, 12, 19);
        Rectangle AllCycle = new Rectangle(390, 420, 20, 19);
        Rectangle Easter = new Rectangle(290, 50, 12, 19);
        Color[] colors = new Color[24];
       
        bool toggleanimation = true;
        MineBlock.Blocks.Air temp = new MineBlock.Blocks.Air(200, 300);
        Texture2D cursor,hoverbotTexture,hotbarSelector;
      
        #endregion
        public Options()
            : base()
        {
            getcolors();
            DrawStars = false;
           
        }
        public override void getTextures()
        {
            Background = Game1.Instance.options;
            cursor = Game1.cursor;
            hoverbotTexture = Game1.hoverbot;
            hotbarSelector = Game1.Instance.hotbarselector;
            base.getTextures();
        }
        public void getcolors()
        {
            colors[0] = Color.White;
            colors[1] = Color.DarkGray;
            colors[2] = Color.Blue;
            colors[3] = Color.Green;
            colors[4] = Color.Red;
            colors[5] = Color.Pink;
            colors[6] = Color.Gray;
            colors[7] = Color.Yellow;
            colors[8] = Color.Orange;
            colors[9] = Color.Navy;
            colors[10] = Color.Purple;
            colors[11] = Color.Silver;
            colors[12] = Color.Brown;
            colors[13] = Color.SkyBlue;
            colors[14] = Color.LightGray;
            colors[15] = Color.LightSalmon;
            colors[16] = Color.LightSeaGreen;
            colors[17] = Color.LightPink;
            colors[18] = Color.LightSteelBlue;
            colors[19] = Color.MediumPurple;
            colors[20] = Color.LightGoldenrodYellow;
            colors[21] = Color.LightCoral;
            colors[22] = Color.LightCyan;
            colors[23] = Color.Firebrick;
        }
        int incrementColor(ref int color)
        {
            canclick = false;
            int Max = colors.Length - 1;

            if (color >= Max)
                color = 0;
            else
                color++;
            return color;

        }
        public override void Update()
        {
            if (hoverbotframe == 88) hoverbotframe = 0;
            if (hoverbotframe == 61) hoverbotframe = 88;
            if (hoverbotframe == 32) hoverbotframe = 61;
            if (hoverbotframe == 0) hoverbotframe = 32;
            if (!botfloat)
                HoverBot = new Rectangle(HoverBot.X, HoverBot.Y - 1, HoverBot.Width, HoverBot.Height);
            if (HoverBot.Y == 165) botfloat = true;
            if (botfloat) HoverBot = new Rectangle(HoverBot.X, HoverBot.Y + 1, HoverBot.Width, HoverBot.Height);
            if (HoverBot.Y == 185) botfloat = false;

            if (Cursor.Intersects(plus) && HandleInputs.LeftTrigger() && canclick)
                Game1.player.highlightcolor = colors[incrementColor(ref HighlightcurrentColor)];

            if (Cursor.Intersects(HoverBot) && HandleInputs.LeftTrigger() && canclick)
                Game1.lasercolor = colors[incrementColor(ref HoverBotcolor)];

            if (Cursor.Intersects(HotbarSelector) && HandleInputs.LeftTrigger() && canclick)
                Game1.player.hotbarSelector = colors[incrementColor(ref hotbatSelectorColor)];

            if (Cursor.Intersects(Breakanim) && HandleInputs.LeftTrigger() && canclick)
                Game1.breakanimcolor = colors[incrementColor(ref breakanim)];

            if (Cursor.Intersects(toggleAnim) && HandleInputs.LeftTrigger() && canclick)
            {
                canclick = false;
                toggleanimation = !toggleanimation;
            }
            if (Cursor.Intersects(MouseCursor) && HandleInputs.LeftTrigger() && canclick)
                Game1.cursorColor = colors[incrementColor(ref cursorcolor)];
            if (Cursor.Intersects(AllCycle) && HandleInputs.LeftTrigger() && canclick)
            {
                Game1.player.highlightcolor = colors[incrementColor(ref allColor)];
                Game1.lasercolor = colors[allColor];
                Game1.player.hotbarSelector = colors[allColor];
                Game1.breakanimcolor = colors[allColor];
                Game1.cursorColor = colors[allColor];
            }
            if (Cursor.Intersects(Easter) && HandleInputs.LeftTrigger() && canclick)
            {
                canclick = false;
                Game1.player.highlightcolor = colors[Game1.randy.Next(0, colors.Length)];
                Game1.lasercolor = colors[Game1.randy.Next(0, colors.Length)];
                Game1.player.hotbarSelector = colors[Game1.randy.Next(0, colors.Length)];
                Game1.breakanimcolor = colors[Game1.randy.Next(0, colors.Length)];
                Game1.cursorColor = colors[Game1.randy.Next(0, colors.Length)];

            }

            if (!canclick && Microsoft.Xna.Framework.Input.Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
                canclick = true;

            base.Update();
        }
        public override void Draw(SpriteBatch batch)
        {
           

            batch.Draw(Background, new Rectangle(0, 0, 800, 520), Color.White);
            batch.DrawString(pericles14, "Click to cycle colors", new Vector2(400 - 110, 50), intersects(Easter, cursorPos));

            batch.DrawString(pericles14, "Block Highlighter", new Vector2(5, plus.Y + 8), intersects(plus, cursorPos));
            batch.Draw(cursor, plus, Game1.player.highlightcolor);

            Game1.DrawLine(batch, new Vector2(HoverBot.X + 6, HoverBot.Y + 13), new Vector2(HoverBot.X + 56, 175 + 13));
            batch.Draw(hoverbotTexture, HoverBot, new Rectangle(hoverbotframe, 0, 16, 22), Color.White);
            batch.DrawString(pericles14, "Hoverbot's laser", new Vector2(5, 175 + 2), intersects(HoverBot, cursorPos));

            batch.Draw(hotbarSelector, HotbarSelector, Game1.player.hotbarSelector);
            batch.DrawString(pericles14, "HotBar Selector", new Vector2(5, HotbarSelector.Y + 13), intersects(HotbarSelector, cursorPos));


            temp.DrawBlank(batch);
            if (toggleanimation)
            {
                if (Animation) temp.damage += 2;
                if (temp.damage > 100) Animation = false;
                if (!Animation) temp.damage -= 2;
                if (temp.damage < 0) Animation = true;
            }

            batch.DrawString(pericles14, "Break Animation", new Vector2(5, Breakanim.Y + 13), intersects(Breakanim, cursorPos));
            batch.DrawString(pericles1, "Toggle", new Vector2(toggleAnim.X, toggleAnim.Y), intersects(toggleAnim, cursorPos));

            batch.Draw(Pointer, MouseCursor, Game1.cursorColor);
            batch.DrawString(pericles14, "Mouse Pointer", new Vector2(5, MouseCursor.Y + 2), intersects(MouseCursor, cursorPos));

            //spriteBatch.Draw(, AllCycle, intersects(AllCycle,cursorPos));
            batch.DrawString(pericles14, "All", new Vector2(AllCycle.X, AllCycle.Y + 2), intersects(AllCycle, cursorPos));
            batch.Draw(Pointer, new Rectangle((int)cursorPos.X, (int)cursorPos.Y, 12, 19), Game1.cursorColor);
            base.Draw(batch);
        }
        Color intersects(Rectangle square, Vector2 mouse)
        {
            if (Easter.Intersects(new Rectangle((int)mouse.X, (int)mouse.Y, 1, 1))) return colors[Game1.randy.Next(0, colors.Length)];
            if (square.Intersects(new Rectangle((int)mouse.X, (int)mouse.Y, 1, 1))) return Color.MediumPurple;
            return Color.White;
        }
        Texture2D ReplaceColor(Texture2D text, Color old, Color newColor)
        {
            Color[] data = new Color[text.Width * text.Height];
            text.GetData(data);
            for (int i = 0; i < data.Length; i++)
                if (data[i] == old)
                    data[i] = newColor;

            text.SetData(data);
            return text;
        }
        Texture2D ReplaceColor(Texture2D text, Color newColor)
        {
            Color[] data = new Color[text.Width * text.Height];
            text.GetData(data);
            for (int i = 0; i < data.Length; i++)
                if (data[i].G > 100 && data[i].R == 0 && data[i].B == 0)
                    data[i] = newColor;

            text.SetData(data);
            return text;
        }
    }
}
