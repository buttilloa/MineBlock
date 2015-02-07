using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MineBlock.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineBlock.Managers
{
    public class MenuRef
    {
        Rectangle StartButton = new Rectangle(331, 260, 153, 43);
        Rectangle OptionsButton = new Rectangle(579, 291, 153, 43);
        Rectangle plus = new Rectangle(200, 100, 40, 40);
        Rectangle HoverBot = new Rectangle(200, 175, 16, 22); int hoverbotframe =0; bool botfloat = true;
        Rectangle HotbarSelector = new Rectangle(200, 240, 48, 48);
        Rectangle Breakanim = new Rectangle(200, 300, 40, 40); bool Animation = true;
        Rectangle toggleAnim = new Rectangle(242, 320, 40, 20);
        Rectangle MouseCursor = new Rectangle(200, 375, 12, 19);
        Rectangle AllCycle = new Rectangle(390, 420, 20, 19);
        Rectangle Easter = new Rectangle(290, 50, 12, 19);

        Rectangle Savestate1 = new Rectangle(19, 220, 153, 43);
        Rectangle Savestate2 = new Rectangle(161, 326, 152, 43);
        Rectangle Savestate3 = new Rectangle(330, 220, 152, 43);
        Rectangle Savestate4 = new Rectangle(488, 326, 152, 43);
        Rectangle Savestate5 = new Rectangle(628, 220, 152, 43);

        public Boolean Savestate1exists = false;
        public Boolean Savestate2exists = false;
        public Boolean Savestate3exists = false;
        public Boolean Savestate4exists = false;
        public Boolean Savestate5exists = false;

        Rectangle Paused1 = new Rectangle(19, 220, 153, 43);
        Rectangle Paused2 = new Rectangle(330, 220, 153, 43);
        Rectangle Paused3 = new Rectangle(628, 220, 153, 43);
        int CursorTouching = 0;
        Vector2 cursorPos = new Vector2(0, 0);
        public enum GameStates { TitleScreen, SaveSelect, Playing, Paused, Options, Error};
        public GameStates state = GameStates.TitleScreen;
       Color[] colors = new Color[24];
        public Color breakanimcolor = Color.White;
        bool toggleanimation = true;
        Air temp = new Air(200, 300); 
        int HighlightcurrentColor;
        int HoverBotcolor;
        int hotbatSelectorColor;
        int breakanim;
        int cursorcolor;
        int allColor;
        bool canclick = true;
        string[] Splashs;
        int currentSplash = 1;
        float Splashsize = 1;
        bool increase = true;
        String Exception , Stacktrace;
        List<Rectangle> Stars = new List<Rectangle>();
        public void loadContent()
        {
            Splashs = new string[6];
            Splashs[0] = "You looked different through your window";
            Splashs[1] = "I really wish you would stop reading and play";
            Splashs[2] = "GG m8 get rekt";
            Splashs[3] = "Hold up grab the wall";
            Splashs[4] = "Now with blocks!";
            Splashs[5] = "What a wonderful time to be alive!";

            currentSplash = Game1.randy.Next(0, Splashs.Count());
            getcolors();
            for (int i = 0; i < 100; i++)
                Stars.Add(new Rectangle(Game1.randy.Next(-1000,790), Game1.randy.Next(0,180), 1, 1));
            
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
        public void update(Game1 game1)
        {
            
            if (state == GameStates.TitleScreen)
            {
                
#if WINDOWS
                cursorPos = new Vector2(HandleInputs.moveHighlighter(cursorPos).X, HandleInputs.moveHighlighter(cursorPos).Y);
                 Rectangle Cursor = new Rectangle((int)cursorPos.X, (int)cursorPos.Y, 3, 3);
                
                if (Cursor.Intersects(StartButton))
                    if (HandleInputs.LeftTrigger())
                        state = GameStates.SaveSelect;
                if (Cursor.Intersects(OptionsButton))
                    if (HandleInputs.LeftTrigger())
                        state = GameStates.Options;
#endif
#if XBOX
                cursorPos = HandleInputs.moveHighlighter(cursorPos);
                Rectangle Cursor = new Rectangle((int)cursorPos.X * 40, (int)cursorPos.Y * 40, 3, 3);
                if (Cursor.Intersects(StartButton))
                    if (HandleInputs.LeftTrigger())
                        state = GameStates.SaveSelect;
#endif
            }
            else if (state == GameStates.Options)
            {
                cursorPos = new Vector2(HandleInputs.moveHighlighter(cursorPos).X, HandleInputs.moveHighlighter(cursorPos).Y);
                Rectangle Cursor = new Rectangle((int)cursorPos.X, (int)cursorPos.Y, 3, 3);
                  if (hoverbotframe == 88) hoverbotframe = 0; 
                if (hoverbotframe == 61) hoverbotframe = 88;
                if (hoverbotframe == 32) hoverbotframe = 61;
                if (hoverbotframe == 0) hoverbotframe = 32;
               if(!botfloat)
                    HoverBot = new Rectangle(HoverBot.X, HoverBot.Y - 1, HoverBot.Width, HoverBot.Height);
               if (HoverBot.Y == 165) botfloat = true;
                if(botfloat) HoverBot = new Rectangle(HoverBot.X, HoverBot.Y + 1, HoverBot.Width, HoverBot.Height);
                if (HoverBot.Y == 185) botfloat = false;
                
                 if (Cursor.Intersects(plus) && HandleInputs.LeftTrigger() && canclick)
                    Game1.player.highlightcolor = colors[incrementColor(ref HighlightcurrentColor)];

                 if (Cursor.Intersects(HoverBot) && HandleInputs.LeftTrigger() && canclick)
                    Game1.mobManager.bot.lasercolor = colors[incrementColor(ref HoverBotcolor)];
                     
                if (Cursor.Intersects(HotbarSelector) && HandleInputs.LeftTrigger() && canclick)
                    Game1.player.hotbarSelector = colors[incrementColor(ref hotbatSelectorColor)];

                if (Cursor.Intersects(Breakanim) && HandleInputs.LeftTrigger() && canclick)
                    breakanimcolor = colors[incrementColor(ref breakanim)];

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
                    Game1.mobManager.bot.lasercolor = colors[allColor];
                    Game1.player.hotbarSelector = colors[allColor];
                    breakanimcolor = colors[allColor];
                    Game1.cursorColor = colors[allColor];
                }
                if (Cursor.Intersects(Easter) && HandleInputs.LeftTrigger() && canclick)
                {
                    canclick = false;
                    Game1.player.highlightcolor = colors[Game1.randy.Next(0,colors.Length)];
                    Game1.mobManager.bot.lasercolor = colors[Game1.randy.Next(0, colors.Length)];
                    Game1.player.hotbarSelector = colors[Game1.randy.Next(0, colors.Length)];
                    breakanimcolor = colors[Game1.randy.Next(0, colors.Length)];
                    Game1.cursorColor = colors[Game1.randy.Next(0, colors.Length)];
                    
                }

                if (!canclick && Microsoft.Xna.Framework.Input.Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
                    canclick = true;
            }
            else if (state == GameStates.SaveSelect)
            {
#if WINDOWS
                cursorPos = new Vector2(HandleInputs.moveHighlighter(cursorPos).X, HandleInputs.moveHighlighter(cursorPos).Y);

                Rectangle Cursor = new Rectangle((int)cursorPos.X, (int)cursorPos.Y, 3, 3);

                if (Game1.saves.hasSaved(1)) Savestate1exists = true; // Check for Existing Saves
                if (Game1.saves.hasSaved(2)) Savestate2exists = true;
                if (Game1.saves.hasSaved(3)) Savestate3exists = true;
                if (Game1.saves.hasSaved(4)) Savestate4exists = true;
                if (Game1.saves.hasSaved(5)) Savestate5exists = true;
#endif
#if XBOX
                cursorPos = HandleInputs.moveHighlighter(cursorPos);
                Rectangle Cursor = new Rectangle((int)cursorPos.X * 40, (int)cursorPos.Y * 40, 3, 3);
                if (Cursor.Intersects(StartButton))
                    if (HandleInputs.LeftTrigger())
                        state = GameStates.SaveSelect;
#endif
                if (Cursor.Intersects(Savestate1))
                {
                    CursorTouching = 1;
                    if (HandleInputs.LeftTrigger())
                    {
                        Game1.selectedSave = 1;
                        Game1.chunk = Game1.saves.loadSave(Game1.selectedSave, Game1.currentChunkNumber, Game1.chunk, Game1.player, Game1.mobManager);
                        state = GameStates.Playing;
                    }
                }
                else if (Cursor.Intersects(Savestate2))
                {
                    CursorTouching = 2;
                    if (HandleInputs.LeftTrigger())
                    {
                        Game1.selectedSave = 2;
                        Game1.chunk = Game1.saves.loadSave(Game1.selectedSave, Game1.currentChunkNumber, Game1.chunk, Game1.player, Game1.mobManager);
                        state = GameStates.Playing;
                    }
                }
                else if (Cursor.Intersects(Savestate3))
                {
                    CursorTouching = 3;
                    if (HandleInputs.LeftTrigger())
                    {
                        Game1.selectedSave = 3;
                        Game1.chunk = Game1.saves.loadSave(Game1.selectedSave, Game1.currentChunkNumber, Game1.chunk, Game1.player, Game1.mobManager);
                        state = GameStates.Playing;
                    }
                }
                else if (Cursor.Intersects(Savestate4))
                {
                    CursorTouching = 4;
                    if (HandleInputs.LeftTrigger())
                    {
                        Game1.selectedSave = 4;
                        Game1.chunk = Game1.saves.loadSave(Game1.selectedSave, Game1.currentChunkNumber, Game1.chunk, Game1.player, Game1.mobManager);
                        state = GameStates.Playing;
                    }
                }
                else if (Cursor.Intersects(Savestate5))
                {
                    CursorTouching = 5;
                    if (HandleInputs.LeftTrigger())
                    {
                        Game1.selectedSave = 5;
                        Game1.chunk = Game1.saves.loadSave(Game1.selectedSave, Game1.currentChunkNumber, Game1.chunk, Game1.player, Game1.mobManager);
                        state = GameStates.Playing;
                    }
                }
                else CursorTouching = 0;
            }
            else if (state == GameStates.Paused)
            {
#if WINDOWS
                cursorPos = new Vector2(HandleInputs.moveHighlighter(cursorPos).X, HandleInputs.moveHighlighter(cursorPos).Y);

                Rectangle Cursor = new Rectangle((int)cursorPos.X, (int)cursorPos.Y, 3, 3);

                // if (Cursor.Intersects(StartButton))
                //   if (HandleInputs.LeftTrigger())
                //{
                //     state = GameStates.SaveSelect;

                //}
#endif
#if XBOX
                cursorPos = HandleInputs.moveHighlighter(cursorPos);
                Rectangle Cursor = new Rectangle((int)cursorPos.X * 40, (int)cursorPos.Y * 40, 3, 3);
                if (Cursor.Intersects(StartButton))
                    if (HandleInputs.LeftTrigger())
                        state = GameStates.SaveSelect;
#endif
                if (Cursor.Intersects(Paused1))
                {
                    CursorTouching = 1;
                    if (HandleInputs.LeftTrigger())
                    {

                        state = GameStates.TitleScreen;
                    }
                }
                else if (Cursor.Intersects(Paused2))
                {
                    CursorTouching = 2;
                    if (HandleInputs.LeftTrigger())
                    {

                        state = GameStates.Playing;
                    }
                }
                else if (Cursor.Intersects(Paused3))
                {
                    CursorTouching = 3;
                    if (HandleInputs.LeftTrigger())
                    {
                        ;
                        state = GameStates.Options;
                    }
                }
            }
        }
        public void Draw(Game1 game1, SpriteBatch spriteBatch)
        {
            if (state == GameStates.TitleScreen)
            {
                
#if WINDOWS
                spriteBatch.Draw(game1.TitleScreen, new Rectangle(0, 0, game1.Window.ClientBounds.Width, game1.Window.ClientBounds.Height), Color.White);
                spriteBatch.Draw(Game1.Pointer, new Rectangle((int)cursorPos.X, (int)cursorPos.Y, 12, 19), Game1.cursorColor);
#endif
#if XBOX 
                 spriteBatch.Draw(game1.TitleScreen, new Rectangle(0, 0, 800,480), Color.White);
                spriteBatch.Draw(game1.Pointer, new Rectangle((int)cursorPos.X * 40, (int)cursorPos.Y * 40, 12, 19), Color.White);
#endif
                if (increase && Splashsize <= 1.1f) Splashsize += .005f;
                if (Splashsize > 1.1f) increase = false;
                if (!increase && Splashsize >= .9f) Splashsize -= .005f;
                if (Splashsize < .9f) increase = true;
                spriteBatch.DrawString(Game1.pericles14, Splashs[currentSplash], new Vector2(10, 150), Color.White, -.3f, new Vector2(0, 0), Splashsize, SpriteEffects.None, 0f);
                for (int i = 0; i < Stars.Count; i++)
                {
                    Stars[i] = new Rectangle(Stars[i].X + 1, Stars[i].Y, 1, 1);
                    if (Stars[i].X > 800) Stars[i] = new Rectangle(Game1.randy.Next(-800, 0), Game1.randy.Next(0, 180), 1, 1);
                    spriteBatch.Draw(Game1.Weather, Stars[i], Stars[i].Y > 100 ? Color.Gray :Color.White);
                }
            }
            else if (state == GameStates.SaveSelect)
            {
#if WINDOWS
                spriteBatch.Draw(game1.SaveSelect, new Rectangle(0, 0, game1.Window.ClientBounds.Width, game1.Window.ClientBounds.Height), Color.White);
#endif             
#if XBOX 
                 spriteBatch.Draw(game1.TitleScreen, new Rectangle(0, 0, 800,480), Color.White);
                spriteBatch.Draw(game1.Pointer, new Rectangle((int)cursorPos.X * 40, (int)cursorPos.Y * 40, 12, 19), Color.White);
#endif
                spriteBatch.DrawString(Game1.pericles14, "Select the Game to Load", new Vector2(280, 100), Color.White);
                spriteBatch.DrawString(Game1.pericles14, "Save Game 1", new Vector2(Savestate1.X + 15, Savestate1.Y + 6), Color.White);
                spriteBatch.DrawString(Game1.pericles1, Savestate1exists ? "Load" : "Create", new Vector2(Savestate1.X + 53, Savestate1.Y + 23), Color.White);
                spriteBatch.DrawString(Game1.pericles14, "Save Game 2", new Vector2(Savestate2.X + 15, Savestate2.Y + 6), Color.White);
                spriteBatch.DrawString(Game1.pericles1, Savestate2exists ? "Load" : "Create", new Vector2(Savestate2.X + 53, Savestate2.Y + 23), Color.White);
                spriteBatch.DrawString(Game1.pericles14, "Save Game 3", new Vector2(Savestate3.X + 15, Savestate3.Y + 6), Color.White);
                spriteBatch.DrawString(Game1.pericles1, Savestate3exists ? "Load" : "Create", new Vector2(Savestate3.X + 53, Savestate3.Y + 23), Color.White);
                spriteBatch.DrawString(Game1.pericles14, "Save Game 4", new Vector2(Savestate4.X + 15, Savestate4.Y + 6), Color.White);
                spriteBatch.DrawString(Game1.pericles1, Savestate4exists ? "Load" : "Create", new Vector2(Savestate4.X + 53, Savestate4.Y + 23), Color.White);
                spriteBatch.DrawString(Game1.pericles14, "Save Game 5", new Vector2(Savestate5.X + 15, Savestate5.Y + 6), Color.White);
                spriteBatch.DrawString(Game1.pericles1, Savestate5exists ? "Load" : "Create", new Vector2(Savestate5.X + 53, Savestate5.Y + 23), Color.White);


                if (CursorTouching == 1) spriteBatch.Draw(Game1.SaveSelectHighlight, Savestate1, Color.White);
                else if (CursorTouching == 2) spriteBatch.Draw(Game1.SaveSelectHighlight, Savestate2, Color.White);
                else if (CursorTouching == 3) spriteBatch.Draw(Game1.SaveSelectHighlight, Savestate3, Color.White);
                else if (CursorTouching == 4) spriteBatch.Draw(Game1.SaveSelectHighlight, Savestate4, Color.White);
                else if (CursorTouching == 5) spriteBatch.Draw(Game1.SaveSelectHighlight, Savestate5, Color.White);
                spriteBatch.Draw(Game1.Pointer, new Rectangle((int)cursorPos.X, (int)cursorPos.Y, 12, 19), Game1.cursorColor);
                for (int i = 0; i < Stars.Count; i++)
                {
                    Stars[i] = new Rectangle(Stars[i].X, Stars[i].Y, 1, 1);
                    if (Stars[i].X > 800) Stars[i] = new Rectangle(Game1.randy.Next(-800, 0), Game1.randy.Next(0, 180), 1, 1);
                    spriteBatch.Draw(Game1.Weather, Stars[i], Stars[i].Y > 100 ? Color.Gray : Color.White);
                }
            }
            else if (state == GameStates.Paused)
            {
#if WINDOWS
                spriteBatch.Draw(game1.Paused, new Rectangle(0, 0, game1.Window.ClientBounds.Width, game1.Window.ClientBounds.Height), Color.White);
                spriteBatch.Draw(Game1.Pointer, new Rectangle((int)cursorPos.X, (int)cursorPos.Y, 12, 19), Game1.cursorColor);
#endif
#if XBOX 
                 spriteBatch.Draw(game1.Paused, new Rectangle(0, 0, 800,480), Color.White);
                spriteBatch.Draw(game1.Pointer, new Rectangle((int)cursorPos.X * 40, (int)cursorPos.Y * 40, 12, 19), Color.White);
#endif
                spriteBatch.DrawString(Game1.pericles14, "Exit to Menu", new Vector2(Paused1.X + 13, Paused1.Y + 11), Color.White);
                spriteBatch.DrawString(Game1.pericles14, "Resume Game", new Vector2(Paused2.X + 13, Paused2.Y + 11), Color.White);
                spriteBatch.DrawString(Game1.pericles14, "Options", new Vector2(Paused3.X + 22, Paused3.Y + 11), Color.White);
                if (CursorTouching == 1) spriteBatch.Draw(Game1.SaveSelectHighlight, Paused1, Color.White);
                else if (CursorTouching == 2) spriteBatch.Draw(Game1.SaveSelectHighlight, Paused2, Color.White);
                else if (CursorTouching == 3) spriteBatch.Draw(Game1.SaveSelectHighlight, Paused3, Color.White);
                for (int i = 0; i < Stars.Count; i++)
                {
                    Stars[i] = new Rectangle(Stars[i].X + 1, Stars[i].Y, 1, 1);
                    if (Stars[i].X > 800) Stars[i] = new Rectangle(Game1.randy.Next(-800, 0), Game1.randy.Next(0, 180), 1, 1);
                    spriteBatch.Draw(Game1.Weather, Stars[i], Stars[i].Y > 100 ? Color.Gray : Color.White);
                }
            }
            else if(state == GameStates.Options)
            {
                spriteBatch.Draw(game1.options, new Rectangle(0, 0, 800, 520),Color.White);
                spriteBatch.DrawString(Game1.pericles14, "Click to cycle colors", new Vector2(400 - 110, 50), intersects(Easter, cursorPos));

                spriteBatch.DrawString(Game1.pericles14, "Block Highlighter", new Vector2(5, plus.Y + 8), intersects(plus, cursorPos));
                spriteBatch.Draw(Game1.cursor, plus, Game1.player.highlightcolor);
                
                Game1.mobManager.bot.DrawLine(spriteBatch, new Vector2(HoverBot.X + 6, HoverBot.Y + 13), new Vector2(HoverBot.X + 56, 175 + 13));
                spriteBatch.Draw(Game1.hoverbot, HoverBot, new Rectangle(hoverbotframe, 0, 16, 22), Color.White);
                spriteBatch.DrawString(Game1.pericles14, "Hoverbot's laser", new Vector2(5, 175 + 2), intersects(HoverBot, cursorPos));

                spriteBatch.Draw(game1.hotbarselector, HotbarSelector, Game1.player.hotbarSelector);
                spriteBatch.DrawString(Game1.pericles14, "HotBar Selector", new Vector2(5, HotbarSelector.Y + 13), intersects(HotbarSelector, cursorPos));
                
               
                temp.DrawBlank(spriteBatch);
                if (toggleanimation)
                {
                    if(Animation) temp.damage += 2;
                    if (temp.damage > 100) Animation = false;
                    if (!Animation)temp.damage -= 2;
                    if (temp.damage < 0) Animation = true;
                } 
                
                spriteBatch.DrawString(Game1.pericles14, "Break Animation", new Vector2(5, Breakanim.Y + 13), intersects(Breakanim, cursorPos));
                spriteBatch.DrawString(Game1.pericles1, "Toggle", new Vector2(toggleAnim.X, toggleAnim.Y), intersects(toggleAnim, cursorPos));

                spriteBatch.Draw(Game1.Pointer, MouseCursor, Game1.cursorColor);
                spriteBatch.DrawString(Game1.pericles14, "Mouse Pointer", new Vector2(5, MouseCursor.Y + 2), intersects(MouseCursor, cursorPos));
                     
                //spriteBatch.Draw(, AllCycle, intersects(AllCycle,cursorPos));
                spriteBatch.DrawString(Game1.pericles14, "All", new Vector2(AllCycle.X, AllCycle.Y + 2),intersects(AllCycle,cursorPos));
                spriteBatch.Draw(Game1.Pointer, new Rectangle((int)cursorPos.X, (int)cursorPos.Y, 12, 19), Game1.cursorColor);
            }
            else if (state == GameStates.Error)
            {
                spriteBatch.Draw(game1.options, new Rectangle(0, 0, 800, 520), Color.White);
                spriteBatch.DrawString(Game1.pericles14, "Unhandled Exception has occured", new Vector2(400 - 160, 50), intersects(Easter, cursorPos));
                spriteBatch.Draw(Game1.Weather, new Rectangle(5,100,790,300),Color.White);
                spriteBatch.DrawString(Game1.pericles1, Exception, new Vector2(5,100), Color.Black);
                spriteBatch.DrawString(Game1.pericles1, Stacktrace, new Vector2(5, 115), Color.Black);
            }
        }
        Color intersects(Rectangle square,Vector2 mouse)
        {
            if(Easter.Intersects(new Rectangle((int)mouse.X, (int)mouse.Y, 1, 1))) return colors[Game1.randy.Next(0,colors.Length)];
            if (square.Intersects(new Rectangle((int)mouse.X, (int)mouse.Y, 1, 1))) return Color.MediumPurple;
            return Color.White;
        }
        Texture2D ReplaceColor(Texture2D text,Color old,Color newColor)
        {
            Color[] data = new Color[text.Width * text.Height];
            text.GetData(data);
            for (int i = 0; i < data.Length; i++)
                if (data[i] == old)
                    data[i] = newColor;

            text.SetData(data);
            return text;
        }
        Texture2D ReplaceColor(Texture2D text,Color newColor)
        {
            Color[] data = new Color[text.Width * text.Height];
            text.GetData(data);
            for (int i = 0; i < data.Length; i++)
                if (data[i].G >100 &&data[i].R ==0 &&data[i].B == 0 )
                    data[i] = newColor;

            text.SetData(data);
            return text;
        }
        public void setError(String message, String Stacktrace)
        {
            this.Exception = message;
            this.Stacktrace = Stacktrace;
        }
    }
}
