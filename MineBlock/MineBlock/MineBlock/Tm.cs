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
        // private static Texture2D terrainsheet, Tools, Pointer, cursor, Weather, hoverbot, t, Pigsheet, PigLeg, hotbarsheet, cowsheet, cowleg, chickensheet, chickenLeg, grass, HealthBar, HandGun, Blur, SaveSelectHighlight;
        // private static Texture2D playerSheet, hotbarselector; // Player Textures
        // private static Texture2D TitleScreen, SaveSelect, Paused, options; // Textures acessed by Menu Class
        private static SpriteFont pericles14, pericles1, pericles28; // Fonts
        private static List<Texture> textures = new List<Texture>();
        public enum Textures { terrainsheet, Tools, Pointer, cursor, Blank, hoverbot, t, Pigsheet, PigLeg, hotbarsheet, cowsheet, cowleg, chickensheet, chickenLeg, grass, HealthBar, Blur, SaveSelectHighlight, playerSheet, hotbarselector, TitleScreen, SaveSelect, Paused, options };
        public enum Font { f14, f1, f28 };
        static int calls;

        public static Texture2D getTexture(Textures texture)
        {
            return getTextureFromString(texture.ToString());

        }

        public static Texture2D getTextureFromString(String texture)
        {
            texture = texture.ToLower();
            calls++;
            if (texture.Length == 0) Console.WriteLine("TEXTURES NOT LOADED!");
            // Texture2D re;
            foreach (Texture tex in textures)
            {
                if (texture == tex.getName().ToLower())
                    return tex.getTexture();
            }
            Console.WriteLine(texture + " wasnt loaded...using default"); return CreateMissingTexture();
            /*switch (texture.ToLower())
             {
                 case "terrainsheet": { re = terrainsheet; break; };
                 case "tools": { re = Tools; break; };
                 case "pointer": { re = Pointer; break; };
                 case "cursor": { re = cursor; break; };
                 case "blank": { re = Weather; break; };
                 case "hoverbot": { re = hoverbot; break; };
                 case "t": { re = t; break; };
                 case "pigsheet": { re = Pigsheet; break; };
                 case "pigleg": { re = PigLeg; break; };
                 case "hotbarsheet": { re = hotbarsheet; break; };
                 case "cowsheet": { re = cowsheet; break; };
                 case "cowleg": { re = cowleg; break; };
                 case "chickensheet": { re = chickensheet; break; };
                 case "chickenleg": { re = chickenLeg; break; };
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
             */

        }
        public static Texture2D CreateMissingTexture()
        {

            Texture2D temp = new Texture2D(Game1.Instance.GraphicsDevice, 2, 2);
            temp.SetData<Color>(
            new Color[] { Color.White, Color.Purple, Color.White, Color.Purple });
            return temp;
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

            //Menus
            textures.Add(new Texture("Blur", Content.Load<Texture2D>(@"Menus/Blur")));
            textures.Add(new Texture("Paused", Content.Load<Texture2D>(@"Menus/Paused")));
            textures.Add(new Texture("SaveSelect", Content.Load<Texture2D>(@"Menus/SaveSelect")));
            textures.Add(new Texture("SaveSelectHighlight", Content.Load<Texture2D>(@"Menus/SaveSelectHighlight")));
            textures.Add(new Texture("TitleScreen", Content.Load<Texture2D>(@"Menus/TitleScreen")));
            textures.Add(new Texture("Start", Content.Load<Texture2D>(@"Menus/Start")));
            textures.Add(new Texture("Settings", Content.Load<Texture2D>(@"Menus/Settings")));

            textures.Add(new Texture("options", Content.Load<Texture2D>(@"Menus/options")));
            //Mobs  
            textures.Add(new Texture("Pigsheet", Content.Load<Texture2D>(@"Mobs/Pig")));
            textures.Add(new Texture("PigLeg", Content.Load<Texture2D>(@"Mobs/PigLeg")));
            textures.Add(new Texture("ChickenSheet", Content.Load<Texture2D>(@"Mobs/Chicken")));
            textures.Add(new Texture("ChickenLeg", Content.Load<Texture2D>(@"Mobs/ChickenLeg")));
            textures.Add(new Texture("CowSheet", Content.Load<Texture2D>(@"Mobs/Cow")));
            textures.Add(new Texture("CowLeg", Content.Load<Texture2D>(@"Mobs/CowLeg")));
            textures.Add(new Texture("HoverBot", Content.Load<Texture2D>(@"Mobs/HoverBot")));
            Texture2D t = new Texture2D(g, 1, 1);
            t.SetData<Color>(
            new Color[] { Color.White });
            textures.Add(new Texture("t", t));
            //Blocks
            textures.Add(new Texture("grass", Content.Load<Texture2D>(@"Blocks/grass")));
            textures.Add(new Texture("terrainsheet", Content.Load<Texture2D>(@"Blocks/terrainsheet")));
            //Items
            textures.Add(new Texture("tools", Content.Load<Texture2D>(@"Items/tools")));
            //Weapons
            textures.Add(new Texture("HandGun1", Content.Load<Texture2D>(@"Weapons/HandGun1")));
            //Player
            textures.Add(new Texture("HealthBar", Content.Load<Texture2D>(@"Player/HealthBar")));
            textures.Add(new Texture("Hotbarsheet", Content.Load<Texture2D>(@"Player/Hotbar")));
            textures.Add(new Texture("Hotbarselector", Content.Load<Texture2D>(@"Player/Hotbar_selector")));
            textures.Add(new Texture("Playersheet", Content.Load<Texture2D>(@"Player/Player")));
            //Fonts
            pericles28 = Content.Load<SpriteFont>(@"Fonts/Pericles28");
            pericles14 = Content.Load<SpriteFont>(@"Fonts/Pericles14");
            pericles1 = Content.Load<SpriteFont>(@"Fonts/Pericles1");
            //Misc
            textures.Add(new Texture("Blank", Content.Load<Texture2D>(@"Misc/Weather")));
            textures.Add(new Texture("Pointer", Content.Load<Texture2D>(@"Misc/Pointer")));
            textures.Add(new Texture("cursor", Content.Load<Texture2D>(@"Misc/cursor")));
            


        }
    }
}
