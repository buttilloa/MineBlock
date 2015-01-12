using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MineBlock.Blocks;
using MineBlock.Mobs;
using System;
using Microsoft.Xna.Framework.Storage;
using MineBlock.Managers;
using MineBlock.Commands;


namespace MineBlock
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {

        GraphicsDeviceManager graphics; // Graphics
        SpriteBatch spriteBatch; // SpriteBatch
        public static Texture2D terrainsheet, cursor, Weather, Pigsheet, cowsheet, chickensheet, grass, HealthBar, HandGun,Blur, SaveSelectHighlight; // Textures that im too lasy to assign to a Manager 8P
        Texture2D playerSheet, hotbarsheet, hotbarselector; // Player Textures
        public Texture2D TitleScreen, Pointer, SaveSelect, Paused; // Textures acessed by Menu Class
        public static MobManager mobManager = new MobManager(); // Manages Mobs
        public static List<Block[,]> chunks = new List<Block[,]>(); // List of all the chunks
        public static Random randy = new Random(System.Environment.TickCount); // Random?
        public static SpriteFont pericles14, pericles1; // Fonts
        public static PlayerManager player; // Manages Player
        public static Weather weather = new Weather(); // Manages Weather
        public static int selectedSave = 0; // Save Slot
        MenuRef menu = new MenuRef(); // Manages Menus
        public static SaveManager saves = new SaveManager(); // Manages Saves
        public static ConsoleManager console = new ConsoleManager(); // Manages the ingame Console
#if XBOX
        bool GameSaveRequested = false;  // Xbox specific saving Variables
         IAsyncResult result;
#endif
        public static int currentChunkNumber = 0; // Current Chunk

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
#if XBOX
        Components.Add(new GamerServicesComponent(this)); // Xbox Specific Player Manager
#endif
        }
        //Initialize Game
        protected override void Initialize()
        {

            base.Initialize();

        }
        //Load Content
        protected override void LoadContent()
        { 
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //Menus
            Blur = Content.Load<Texture2D>(@"Menus/Blur");
            Paused = Content.Load<Texture2D>(@"Menus/Paused");
            SaveSelect = Content.Load<Texture2D>(@"Menus/SaveSelect");
            SaveSelectHighlight = Content.Load<Texture2D>(@"Menus/SaveSelectHighlight");
            TitleScreen = Content.Load<Texture2D>(@"Menus/TitleScreen");
            //Mobs  
            Pigsheet = Content.Load<Texture2D>(@"Mobs/Pig");
            chickensheet = Content.Load<Texture2D>(@"Mobs/Chicken");
            cowsheet = Content.Load<Texture2D>(@"Mobs/Cow");
            //Blocks
            grass = Content.Load<Texture2D>(@"Blocks/grass");
            terrainsheet = Content.Load<Texture2D>(@"Blocks/terrainsheet");
            //Weapons
            HandGun = Content.Load<Texture2D>(@"Weapons/HandGun1");
            //Player
            HealthBar = Content.Load<Texture2D>(@"Player/HealthBar");
            hotbarsheet = Content.Load<Texture2D>(@"Player/Hotbar");
            hotbarselector = Content.Load<Texture2D>(@"Player/Hotbar_selector");
            playerSheet = Content.Load<Texture2D>(@"Player/Player");
            //Fonts
            pericles14 = Content.Load<SpriteFont>(@"Fonts/Pericles14");
            pericles1 = Content.Load<SpriteFont>(@"Fonts/Pericles1");
            //Misc
            Weather = Content.Load<Texture2D>(@"Misc/Weather");
            Pointer = Content.Load<Texture2D>(@"Misc/Pointer");
            cursor = Content.Load<Texture2D>(@"Misc/cursor");
            //Sounds
            SoundEffects.LoadSounds(Content);
            //Register Player
            player = new PlayerManager(playerSheet, hotbarsheet, hotbarselector);
            menu.loadContent();
#if WINDOWS
            saves.GetDevice(); // Get Save Device
#endif
#if XBOX
if ((!Guide.IsVisible) && (GameSaveRequested == false)) // Request Xbox Storage Device
            {
                GameSaveRequested = true;
                result = StorageDevice.BeginShowSelector(
                        PlayerIndex.One, null, null);
            }
#endif
            if (saves.hasSaved(1)) menu.Savestate1exists = true; // Check for Existing Saves
            if (saves.hasSaved(2)) menu.Savestate2exists = true;
            if (saves.hasSaved(3)) menu.Savestate3exists = true;
            if (saves.hasSaved(4)) menu.Savestate4exists = true;
            if (saves.hasSaved(5)) menu.Savestate5exists = true;

        }
        //Unload Content
        protected override void UnloadContent()
        {


        }
        // Get this chunks information Block
        public _InformationBlock getInfoBlock()
        {
            return (_InformationBlock)chunks[currentChunkNumber][19, 12];
        }
        // Switch Current Chunk
        public static void switchChunk(PlayerManager player, int chunk)
        {
            currentChunkNumber = chunk;
            Console.WriteLine("Changechunk method " + currentChunkNumber);
            //currentChunk = chunks[currentChunkNumber];
            player.updateBlocks(chunks[currentChunkNumber]);
        }
        //checks how the player should switch
        void shouldSwitch()
        {
            if (player.Player.Location.Y > 400)
            {
                if (currentChunkNumber < 90)
                    currentChunkNumber += 10;
                player.Player.Location = new Vector2(380, 0);
            }
            else if (player.Player.Location.X <= -30)
            {
                if (currentChunkNumber % 10 != 0)
                    currentChunkNumber--;
                player.Player.Location = new Vector2(380, 0);
            }
            else if (player.Player.Location.X >= 730)
            {
                if (currentChunkNumber % 10 != 9)
                    currentChunkNumber++;
                player.Player.Location = new Vector2(380, 0);
            }

            player.WantsToChange = false;

            Console.WriteLine("Changing to " + currentChunkNumber);

            player.updateBlocks(chunks[currentChunkNumber]);

        }
        //Update the Games Logic
       protected override void Update(GameTime gameTime)
        {
            if (console.isShown)
                console.getKeyStrokes();
            else if (HandleInputs.isKeyDown("OemTilde")) console.isShown = true;
            if (HandleInputs.isKeyDown("Escape")) // Opens pause menu
            {
                menu.state = MenuRef.GameStates.Paused;
                saves.SaveAll(selectedSave, player, mobManager);
            }
            if (menu.state == MenuRef.GameStates.Playing && !console.isShown)
            {
#if XBOX

if ((GameSaveRequested) && (result.IsCompleted))
            {
                StorageDevice device = StorageDevice.EndShowSelector(result);
                if (device != null && device.IsConnected)
                {
                    saves.setDevice(device);
                    saves.GetContainer("MineBlock" +selectedSave);
                    if (saves.hasSaved())
                    {
                        currentChunk = saves.LoadChunk(currentChunkNumber);
                        chunks.Add(currentChunk);
                        loadTerrainCollum();
                        saves.LoadPlayer(player);
                        saves.LoadMobs(mobManager);
                    }
                    else
                    {
                        GenerateSpawnTerrain();
                        genTerrainCollum();
                   }
                }
                // Reset the request flag
                GameSaveRequested = false;
            }
#endif
                player.update(gameTime);
                mobManager.update(gameTime);
                if (player.Player.Location.Y > 400 || player.WantsToChange)
                    shouldSwitch();
                foreach (Block block in chunks[currentChunkNumber])
                    block.update(chunks[currentChunkNumber]);
                if (randy.Next(0, 2000) == 4)
                    toggleDownfall();
                if (player.WantsToChangeTP && player.ChunkTp != "")
                    manualTeleport();
                checkClicks();
            }
            else menu.update(this);
            base.Update(gameTime);
        }
        public static void toggleDownfall()
        {
            if (!weather.isPercipitationing())
            {
                _InformationBlock Info = (_InformationBlock)chunks[currentChunkNumber][19, 12];
                if (Info.ShouldSnow) weather.Snow();
                else weather.Rain();
                Console.WriteLine("WEATHEREDING!");
            }
        }
        //Manualy Telport
        void manualTeleport()
        {
            player.WantsToChangeTP = false;
            player.drawTeleporterMessage = false;

            player.Player.Location = new Vector2(380, 0);

            currentChunkNumber = Convert.ToInt32(player.ChunkTp);

            player.ChunkTp = "";
            Console.WriteLine("Changing to " + currentChunkNumber);
            chunks[currentChunkNumber] = chunks[currentChunkNumber];
            player.updateBlocks(chunks[currentChunkNumber]);
        }
        //Checks if mouse was clicked
        void checkClicks()
        {
            if (HandleInputs.RightTrigger())
                try
                {
                    if (chunks[currentChunkNumber][(int)player.highlighted.X, (int)player.highlighted.Y].index == 26)
                        player.useChest((Chest)chunks[currentChunkNumber][(int)player.highlighted.X, (int)player.highlighted.Y]);
                    if (player.count[player.selected] > 0 && chunks[currentChunkNumber][(int)player.highlighted.X, (int)player.highlighted.Y].index == 0 || player.count[player.selected] > 0 && chunks[currentChunkNumber][(int)player.highlighted.X, (int)player.highlighted.Y].index == 14)
                    {

                        chunks[currentChunkNumber][(int)player.highlighted.X, (int)player.highlighted.Y] = player.hotbar[player.selected].Place((int)player.highlighted.X, (int)player.highlighted.Y);

                        player.count[player.selected]--;
                        if (player.count[player.selected] == 0)
                            player.hotbar[player.selected] = new Air((player.selected * 40) + 16, 16);
 player.updateBlocks(chunks[currentChunkNumber]);
                    }

                }
                catch (System.IndexOutOfRangeException)
                {
                    Console.WriteLine("STAY IN THE LINES!");
                }
            try
            {
                if (HandleInputs.LeftTrigger() && chunks[currentChunkNumber][(int)player.highlighted.X, (int)player.highlighted.Y].index != 0 && chunks[currentChunkNumber][(int)player.highlighted.X, (int)player.highlighted.Y].canMine)
                {
                    player.addToInv(chunks[currentChunkNumber][(int)player.highlighted.X, (int)player.highlighted.Y].Mine((int)player.highlighted.X, (int)player.highlighted.Y), 1);
                    chunks[currentChunkNumber][(int)player.highlighted.X, (int)player.highlighted.Y] = new Air((int)player.highlighted.X, (int)player.highlighted.Y);
                }

            }
            catch (System.IndexOutOfRangeException)
            {
                Console.WriteLine("STAY IN THE LINES!");
            }

        }
        //Render on Screen
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            spriteBatch.Begin();
            if (menu.state == MenuRef.GameStates.Playing)
            {
                foreach (Block block in chunks[currentChunkNumber])
                    block.Draw(spriteBatch);//Draw Blocks
                player.Draw(spriteBatch);//Draw Player
                mobManager.Draw(spriteBatch); // Draw Mobs
                weather.Draw(spriteBatch);// Draw Weather
                spriteBatch.DrawString(pericles14, "Chunk: " + currentChunkNumber, new Vector2(690, 10), Color.White);// Draw Current Chunk int
                spriteBatch.DrawString(pericles14, getInfoBlock().Biome, new Vector2(690, 24), Color.White); // Draw Current Biome
            }
            else menu.Draw(this, spriteBatch); // Draw Menus
            console.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
