using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MineBlock
{
    public class Tm
    {
        private static Texture2D terrainsheet, Tools, Pointer, cursor, Weather, hoverbot, t, Pigsheet, hotbarsheet, cowsheet, chickensheet, grass, HealthBar, HandGun, Blur, SaveSelectHighlight;
        private static Texture2D playerSheet, hotbarselector; // Player Textures
        private static Texture2D TitleScreen, SaveSelect, Paused, options; // Textures acessed by Menu Class
        private static SpriteFont pericles14, pericles1, pericles28; // Fonts
        public enum Texture { terrainsheet, Tools, Pointer, cursor, Blank, hoverbot, t, Pigsheet, hotbarsheet, cowsheet, chickensheet, grass, HealthBar, Blur, SaveSelectHighlight, playerSheet, hotbarselector, TitleScreen, SaveSelect, Paused, options };
        public enum Font { f14, f1, f28 };
        static int calls;

        public static Texture2D getTexture(Texture texture)
        {
            return getTextureFromString(texture.ToString());
        }
        public static Texture2D getTextureFromString(String texture)
        {
            calls++;
            if (options == null) Console.WriteLine("TEXTURES NOT LOADED!");
            Texture2D re;
            switch (texture.ToLower())
            {
                case "terrainsheet": { re = terrainsheet; break; };
                case "tools": { re = Tools; break; };
                case "pointer": { re = Pointer; break; };
                case "cursor": { re = cursor; break; };
                case "blank": { re = Weather; break; };
                case "hoverbot": { re = hoverbot; break; };
                case "t": { re = t; break; };
                case "pigsheet": { re = Pigsheet; break; };
                case "hotbarsheet": { re = hotbarsheet; break; };
                case "cowsheet": { re = cowsheet; break; };
                case "chickensheet": { re = chickensheet; break; };
                case "grass": { re = grass; break; };
                case "healthbar": { re = HealthBar; break; };
                case "blur": { re = Blur; break; };
                case "saveselecthighlight": { re = SaveSelectHighlight; break; };
                case "playersheet": { re = playerSheet; break; };
                case "hotbarselector": { re = hotbarselector; break; };
                case "titlescreen": { re = TitleScreen; break; };
                case "saveselect": { re = SaveSelect; break; };
                case "paused": { re = Paused; break; };
                case "options": { re = options; break; };
                default: { re = t; break; };

            }
            if (re == null) { Console.WriteLine(texture + " wasnt loaded...using default"); return CreateMissingTexture(); }
            return re;

        }
        public static Texture2D CreateMissingTexture()
        {

            t = new Texture2D(Game1.Instance.GraphicsDevice, 1, 1);
            t.SetData<Color>(
            new Color[] { Color.White });
            return t;
        }
        public static int getCalls()
        {
            return calls;
        }
        public static SpriteFont getFont(Font font)
        {
            return getFontFromString(font.ToString());
        }
        public static SpriteFont getFontFromString(String Font)
        {
            calls++;
            switch (Font.ToLower())
            {
                case "f14": { return pericles14; };
                case "f1": { return pericles1; };
                case "f28": { return pericles28; };

                default: { return pericles14; };

            }

        }

        public static void loadContent(ContentManager Content, GraphicsDevice g)
        {
            Blur = Content.Load<Texture2D>(@"Menus/Blur");
            Paused = Content.Load<Texture2D>(@"Menus/Paused");
            SaveSelect = Content.Load<Texture2D>(@"Menus/SaveSelect");
            SaveSelectHighlight = Content.Load<Texture2D>(@"Menus/SaveSelectHighlight");
            TitleScreen = Content.Load<Texture2D>(@"Menus/TitleScreen");

            options = Content.Load<Texture2D>(@"Menus/options");
            //Mobs  
            Pigsheet = Content.Load<Texture2D>(@"Mobs/Pig");
            chickensheet = Content.Load<Texture2D>(@"Mobs/Chicken");
            cowsheet = Content.Load<Texture2D>(@"Mobs/Cow");
            hoverbot = Content.Load<Texture2D>(@"Mobs/HoverBot");
            t = new Texture2D(g, 1, 1);
            t.SetData<Color>(
            new Color[] { Color.White });
            //Blocks
            grass = Content.Load<Texture2D>(@"Blocks/grass");
            terrainsheet = Content.Load<Texture2D>(@"Blocks/terrainsheet");
            //Items
            Tools = Content.Load<Texture2D>(@"Items/tools");
            //Weapons
            HandGun = Content.Load<Texture2D>(@"Weapons/HandGun1");
            //Player
            HealthBar = Content.Load<Texture2D>(@"Player/HealthBar");
            hotbarsheet = Content.Load<Texture2D>(@"Player/Hotbar");
            hotbarselector = Content.Load<Texture2D>(@"Player/Hotbar_selector");
            playerSheet = Content.Load<Texture2D>(@"Player/Player");
            //Fonts
            pericles28 = Content.Load<SpriteFont>(@"Fonts/Pericles28");
            pericles14 = Content.Load<SpriteFont>(@"Fonts/Pericles14");
            pericles1 = Content.Load<SpriteFont>(@"Fonts/Pericles1");
            //Misc
            Weather = Content.Load<Texture2D>(@"Misc/Weather");
            Pointer = Content.Load<Texture2D>(@"Misc/Pointer");
            cursor = Content.Load<Texture2D>(@"Misc/cursor");


        }
    }
}
