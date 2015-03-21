using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MineBlock.Blocks;
using MineBlock.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineBlock.Managers
{
    public class MenuRef
    {
        public enum GameStates { TitleScreen, SaveSelect, Playing, Paused, Options, Error };
        public static GameStates state = GameStates.TitleScreen;

        private static BaseMenu CurrentMenu;
        private static BaseMenu LastMenu;
        public void Init()
        {
            CurrentMenu = new TitleScreen();
        }
        public static BaseMenu getLastMenu()
        {
            return LastMenu;
        }
        public static void SetMenu(BaseMenu newMenu)
        {
            LastMenu = CurrentMenu;
            CurrentMenu.disposeMenu();
            CurrentMenu = newMenu;
        }
        public static void SetErrorMenu(String Exception, String stacktrace)
        {
            state = GameStates.Error;
            CurrentMenu.disposeMenu();
            CurrentMenu = new CrashMenu(Exception, stacktrace);
        }
        public void update()
        {
            if (state != GameStates.Playing) CurrentMenu.Update();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (state != GameStates.Playing) CurrentMenu.Draw(spriteBatch);
        }


    }
}
