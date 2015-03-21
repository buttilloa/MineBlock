using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MineBlock.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineBlock.Menus
{
    public class SaveSelect : BaseMenu
    {
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
       
        public SaveSelect()
            : base()
        {
            if (Game1.saves.hasSaved(1)) Savestate1exists = true; // Check for Existing Saves
            if (Game1.saves.hasSaved(2)) Savestate2exists = true;
            if (Game1.saves.hasSaved(3)) Savestate3exists = true;
            if (Game1.saves.hasSaved(4)) Savestate4exists = true;
            if (Game1.saves.hasSaved(5)) Savestate5exists = true;
        }
        public override void getTextures()
        {
            Background = Tm.getTexture(Tm.Texture.SaveSelect);
            base.getTextures();
        }
        public override void Update()
        {
          
            if (Cursor.Intersects(Savestate1))
            {
                CursorTouching = 1;
                if (HandleInputs.LeftTrigger())
                {
                    Game1.selectedSave = 1;
                    Game1.chunk = Game1.saves.loadSave(Game1.selectedSave, Game1.currentChunkNumber, Game1.chunk, Game1.player, Game1.mobManager);
                    MenuRef.state = MenuRef.GameStates.Playing;
                }
            }
            else if (Cursor.Intersects(Savestate2))
            {
                CursorTouching = 2;
                if (HandleInputs.LeftTrigger())
                {
                    Game1.selectedSave = 2;
                    Game1.chunk = Game1.saves.loadSave(Game1.selectedSave, Game1.currentChunkNumber, Game1.chunk, Game1.player, Game1.mobManager);
                    MenuRef.state = MenuRef.GameStates.Playing;
                }
            }
            else if (Cursor.Intersects(Savestate3))
            {
                CursorTouching = 3;
                if (HandleInputs.LeftTrigger())
                {
                    Game1.selectedSave = 3;
                    Game1.chunk = Game1.saves.loadSave(Game1.selectedSave, Game1.currentChunkNumber, Game1.chunk, Game1.player, Game1.mobManager);
                    MenuRef.state = MenuRef.GameStates.Playing;
                }
            }
            else if (Cursor.Intersects(Savestate4))
            {
                CursorTouching = 4;
                if (HandleInputs.LeftTrigger())
                {
                    Game1.selectedSave = 4;
                    Game1.chunk = Game1.saves.loadSave(Game1.selectedSave, Game1.currentChunkNumber, Game1.chunk, Game1.player, Game1.mobManager);
                    MenuRef.state = MenuRef.GameStates.Playing;
                }
            }
            else if (Cursor.Intersects(Savestate5))
            {
                CursorTouching = 5;
                if (HandleInputs.LeftTrigger())
                {
                    Game1.selectedSave = 5;
                    Game1.chunk = Game1.saves.loadSave(Game1.selectedSave, Game1.currentChunkNumber, Game1.chunk, Game1.player, Game1.mobManager);
                    MenuRef.state = MenuRef.GameStates.Playing;
                }
            }
            else CursorTouching = 0;

            base.Update();
        }
        public override void Draw(SpriteBatch batch)
        {
            
            batch.Draw(Background, new Rectangle(0, 0, GameWindow.Width, GameWindow.Height), Color.White);
            batch.DrawString(pericles14, "Select the Game to Load", new Vector2(280, 100), Color.White);
            batch.DrawString(pericles14, "Save Game 1", new Vector2(Savestate1.X + 15, Savestate1.Y + 6), Color.White);
            batch.DrawString(pericles1, Savestate1exists ? "Load" : "Create", new Vector2(Savestate1.X + 53, Savestate1.Y + 23), Color.White);
            batch.DrawString(pericles14, "Save Game 2", new Vector2(Savestate2.X + 15, Savestate2.Y + 6), Color.White);
            batch.DrawString(pericles1, Savestate2exists ? "Load" : "Create", new Vector2(Savestate2.X + 53, Savestate2.Y + 23), Color.White);
            batch.DrawString(pericles14, "Save Game 3", new Vector2(Savestate3.X + 15, Savestate3.Y + 6), Color.White);
            batch.DrawString(pericles1, Savestate3exists ? "Load" : "Create", new Vector2(Savestate3.X + 53, Savestate3.Y + 23), Color.White);
            batch.DrawString(pericles14, "Save Game 4", new Vector2(Savestate4.X + 15, Savestate4.Y + 6), Color.White);
            batch.DrawString(pericles1, Savestate4exists ? "Load" : "Create", new Vector2(Savestate4.X + 53, Savestate4.Y + 23), Color.White);
            batch.DrawString(pericles14, "Save Game 5", new Vector2(Savestate5.X + 15, Savestate5.Y + 6), Color.White);
            batch.DrawString(pericles1, Savestate5exists ? "Load" : "Create", new Vector2(Savestate5.X + 53, Savestate5.Y + 23), Color.White);


            if (CursorTouching == 1) batch.Draw(SaveSelectHighlight, Savestate1, Color.White);
            else if (CursorTouching == 2) batch.Draw(SaveSelectHighlight, Savestate2, Color.White);
            else if (CursorTouching == 3) batch.Draw(SaveSelectHighlight, Savestate3, Color.White);
            else if (CursorTouching == 4) batch.Draw(SaveSelectHighlight, Savestate4, Color.White);
            else if (CursorTouching == 5) batch.Draw(SaveSelectHighlight, Savestate5, Color.White);
            batch.Draw(Pointer, new Rectangle((int)cursorPos.X, (int)cursorPos.Y, 12, 19), Game1.cursorColor);
            base.Draw(batch);
        }
    }
}
