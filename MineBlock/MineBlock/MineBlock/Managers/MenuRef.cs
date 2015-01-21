using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineBlock.Managers
{
    public class MenuRef
    {
        public Rectangle StartButton =new Rectangle(331, 260, 153, 43);

        public Rectangle Savestate1 = new Rectangle(19, 220, 153, 43);
        public Rectangle Savestate2 = new Rectangle(161, 326, 152, 43);
        public Rectangle Savestate3 = new Rectangle(330, 220, 152, 43);
        public Rectangle Savestate4 = new Rectangle(488, 326, 152, 43);
        public Rectangle Savestate5 = new Rectangle(628, 220, 152, 43);

        public Boolean Savestate1exists = false;
        public Boolean Savestate2exists = false;
        public Boolean Savestate3exists = false;
        public Boolean Savestate4exists = false;
        public Boolean Savestate5exists = false;

        public Rectangle Paused1 = new Rectangle(19, 220, 153, 43);
        public Rectangle Paused2 = new Rectangle(330, 220, 153, 43);
        public Rectangle Paused3 = new Rectangle(628, 220, 153, 43);
        public int CursorTouching = 0;
        Vector2 cursorPos = new Vector2(0, 0);
        public enum GameStates { TitleScreen, SaveSelect, Playing, Paused };
        public GameStates state = GameStates.TitleScreen;
        string[] Splashs;
        int currentSplash =1;
        float size = 1;
        bool increase = true;
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
        }
        public void update(Game1 game1)
        {
            
            if (state == GameStates.TitleScreen)
            {

#if WINDOWS
                cursorPos = new Vector2(HandleInputs.moveHighlighter(cursorPos).X * 40, HandleInputs.moveHighlighter(cursorPos).Y * 40);

                Rectangle Cursor = new Rectangle((int)cursorPos.X, (int)cursorPos.Y, 3, 3);
                if (Cursor.Intersects(StartButton))
                    if (HandleInputs.LeftTrigger())
                    {
                        state = GameStates.SaveSelect;

                    }
#endif
#if XBOX
                cursorPos = HandleInputs.moveHighlighter(cursorPos);
                Rectangle Cursor = new Rectangle((int)cursorPos.X * 40, (int)cursorPos.Y * 40, 3, 3);
                if (Cursor.Intersects(StartButton))
                    if (HandleInputs.LeftTrigger())
                        state = GameStates.SaveSelect;
#endif
            }
            else if (state == GameStates.SaveSelect)
            {
#if WINDOWS
                cursorPos = new Vector2(HandleInputs.moveHighlighter(cursorPos).X * 40, HandleInputs.moveHighlighter(cursorPos).Y * 40);

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
                        state =GameStates.Playing;
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
                cursorPos = new Vector2(HandleInputs.moveHighlighter(cursorPos).X * 40, HandleInputs.moveHighlighter(cursorPos).Y * 40);

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
                        state = GameStates.Paused;
                    }
                }
            }
        }
        public void Draw( Game1 game1,SpriteBatch spriteBatch)
        {
            if (state ==GameStates.TitleScreen)
            {
#if WINDOWS
                spriteBatch.Draw(game1.TitleScreen, new Rectangle(0, 0, game1.Window.ClientBounds.Width, game1.Window.ClientBounds.Height), Color.White);
                spriteBatch.Draw(game1.Pointer, new Rectangle((int)cursorPos.X, (int)cursorPos.Y, 12, 19), Color.White);
#endif
#if XBOX 
                 spriteBatch.Draw(game1.TitleScreen, new Rectangle(0, 0, 800,480), Color.White);
                spriteBatch.Draw(game1.Pointer, new Rectangle((int)cursorPos.X * 40, (int)cursorPos.Y * 40, 12, 19), Color.White);
#endif
                if (increase && size <= 1.1f) size += .005f;
                if (size > 1.1f) increase = false;
                if (!increase && size >= .9f) size -= .005f;
                if (size < .9f) increase = true;
                spriteBatch.DrawString(Game1.pericles14, Splashs[currentSplash], new Vector2(10, 150), Color.White,-.3f,new Vector2(0,0),size,SpriteEffects.None,0f);
            }
            else if (state == GameStates.SaveSelect)
            {
#if WINDOWS
                spriteBatch.Draw(game1.SaveSelect, new Rectangle(0, 0, game1.Window.ClientBounds.Width, game1.Window.ClientBounds.Height), Color.White);
                spriteBatch.Draw(game1.Pointer, new Rectangle((int)cursorPos.X, (int)cursorPos.Y, 12, 19), Color.White);
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
            }
            else if (state == GameStates.Paused)
            {
#if WINDOWS
                spriteBatch.Draw(game1.Paused, new Rectangle(0, 0, game1.Window.ClientBounds.Width, game1.Window.ClientBounds.Height), Color.White);
                spriteBatch.Draw(game1.Pointer, new Rectangle((int)cursorPos.X, (int)cursorPos.Y, 12, 19), Color.White);
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
            }
        }
    }
}
