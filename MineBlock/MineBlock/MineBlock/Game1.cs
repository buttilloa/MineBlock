using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MineBlock.Blocks;
using MineBlock.Commands;
using MineBlock.Items;
using MineBlock.Managers;
using System;


namespace MineBlock
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {

        GraphicsDeviceManager graphics; // Graphics
        SpriteBatch spriteBatch; // SpriteBatch
        public static Texture2D terrainsheet, Tools, Pointer, cursor, Weather, hoverbot, Pigsheet, hotbarsheet, cowsheet, chickensheet, grass, HealthBar, HandGun, Blur, SaveSelectHighlight; // Textures that im too lasy to assign to a Manager 8P
        public Texture2D playerSheet, hotbarselector; // Player Textures
        public Texture2D TitleScreen, SaveSelect, Paused, options; // Textures acessed by Menu Class
        public static MobManager mobManager = new MobManager(); // Manages Mobs
        //public static List<Block[,]> chunks = new List<Block[,]>(); // List of all the chunks
        public static Block[,] chunk = new Block[200, 130];
        public static Random randy = new Random(System.Environment.TickCount); // Random?
        public static SpriteFont pericles14, pericles1, pericles28; // Fonts
        public static PlayerManager player; // Manages Player
        public static Weather weather = new Weather(); // Manages Weather
        public static int selectedSave = 0; // Save Slot
        public static MenuRef menu = new MenuRef(); // Manages Menus
        public static SaveManager saves = new SaveManager(); // Manages Saves
        public static ConsoleManager console = new ConsoleManager(); // Manages the ingame Console
        public float zoom = 0.0f;
        public static Color cursorColor = Color.White;
#if XBOX
        bool GameSaveRequested = false;  // Xbox specific saving Variables
         IAsyncResult result;
#endif
        public static int currentChunkNumber = 0; // Current Chunk

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferMultiSampling = true;
            //graphics.PreferredBackBufferHeight = 1050;
            //graphics.PreferredBackBufferWidth = 1680;
            // Console.WriteLine("Height" + graphics.PreferredBackBufferHeight);
            // graphics.IsFullScreen = true;
            this.Window.Title = "Colonization";
            this.graphics.SynchronizeWithVerticalRetrace = false;
            this.IsFixedTimeStep = false;

#if XBOX
        Components.Add(new GamerServicesComponent(this)); // Xbox Specific Player Manager
#endif
            Components.Add(new FrameRateCounter(this));
          
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
            options = Content.Load<Texture2D>(@"Menus/options");
            //Mobs  
            Pigsheet = Content.Load<Texture2D>(@"Mobs/Pig");
            chickensheet = Content.Load<Texture2D>(@"Mobs/Chicken");
            cowsheet = Content.Load<Texture2D>(@"Mobs/Cow");
            hoverbot = Content.Load<Texture2D>(@"Mobs/HoverBot");
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
            mobManager.addBot(GraphicsDevice);
        }
        //Unload Content
        protected override void UnloadContent()
        {


        }
        // Get this chunks information Block
        /*public _InformationBlock getInfoBlock()// depricated
        {
            //int chunkx = ((int)player.Player.Location.X/40) % 10;
            //int chunky = ((int)player.Player.Location.Y / 40) / 10; 
            return (_InformationBlock)chunk[19, 12];
        } 
         */
        //Update the Games Logic
        protected override void Update(GameTime gameTime)
        {
            try
            {
                // if (HandleInputs.isKeyDown("P")) this.Exit();
                if (console.isShown)
                    console.getKeyStrokes();
                else if (HandleInputs.isKeyDown("OemTilde")) console.isShown = true;
                if (HandleInputs.isKeyDown("Escape")) // Opens pause menu
                {
                    menu.state = MenuRef.GameStates.Paused;
                    //saves.SaveAll(selectedSave, player, mobManager);
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
                    if (HandleInputs.isKeyDown("Up")) zoom += .01f;
                    if (HandleInputs.isKeyDown("Down")) zoom -= .01f;
                    player.update(gameTime);
                    mobManager.update(gameTime);
                    //   if (player.Player.Location.Y > 400 || player.WantsToChange)
                    //       shouldSwitch();
                    foreach (Block block in chunk)
                        if (block.x > (player.Player.Location.X / 40) - 11 && block.x < (player.Player.Location.X / 40) + 12)
                            if (block.y > (player.Player.Location.Y / 40) - 11 && block.y < (player.Player.Location.Y / 40) + 11)
                                block.update(chunk);
                    //if (randy.Next(0, 2000) == 4)
                    //   toggleDownfall();
                    // if (player.WantsToChangeTP && player.ChunkTp != "")
                    //     manualTeleport();
                    checkClicks(gameTime);
                }
                else menu.update(this);
                base.Update(gameTime);
            }
            catch (Exception e)
            {
                menu.setError(e.Message, e.StackTrace);
                menu.state = MenuRef.GameStates.Error;

                //Console.WriteLine(e.Data);


            }
        }
        public static void toggleDownfall()
        {
            if (!weather.isPercipitationing())
            {
                //_InformationBlock Info = (_InformationBlock)chunk[19, 12];
                //if (Info.ShouldSnow) weather.Snow();
                //else weather.Rain();
                weather.Rain();
                Console.WriteLine("WEATHEREDING!");
            }
        }
         
        //Manualy Telport
        /* void manualTeleport()
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
         */
        //Checks if mouse was clicked
        float minetimer = -1;
        void checkClicks(GameTime gametime)
        {
            if (!player.playerinv.isdisplayed)
            {
                if (HandleInputs.RightTrigger())
                {
                    if (player.hotbar[player.selected].Blockindex != -1)
                    {
                        if (chunk[(int)player.highlighted.X, (int)player.highlighted.Y].index == 26)
                            player.useChest((Chest)chunk[(int)player.highlighted.X, (int)player.highlighted.Y]);
                        if (player.hotbar[player.selected].Count > 0 && chunk[(int)player.highlighted.X, (int)player.highlighted.Y].index == 0 || player.hotbar[player.selected].Count > 0 && chunk[(int)player.highlighted.X, (int)player.highlighted.Y].index == 14)
                        {

                            chunk[(int)player.highlighted.X, (int)player.highlighted.Y] = player.hotbar[player.selected].ReturnBlock().Place((int)player.highlighted.X, (int)player.highlighted.Y);

                            player.hotbar[player.selected].Count--;
                            if (player.hotbar[player.selected].Count == 0)
                                player.hotbar[player.selected] = new Air((player.selected * 40) + 16, 16).ItemBlock();
                            player.updateBlocks(chunk);
                        }

                    }
                }

                if (HandleInputs.LeftTrigger())
                {

                    if (chunk[(int)player.highlighted.X, (int)player.highlighted.Y].index != 0 && chunk[(int)player.highlighted.X, (int)player.highlighted.Y].canMine)
                    {

                        Tool currentTool = null;
                        if (player.hotbar[player.selected] is Tool)
                            currentTool = (Tool)player.hotbar[player.selected];
                        float minetime = chunk[(int)player.highlighted.X, (int)player.highlighted.Y].MineTime;
                        if (minetimer == -1)
                            minetimer = 0;
                        if (minetimer > -1 && minetimer < minetime)
                        {
                            float extradmg = 0;
                            if (player.hotbar[player.selected].Blockindex < 0 && chunk[(int)player.highlighted.X, (int)player.highlighted.Y].preferedTool != null) if (chunk[(int)player.highlighted.X, (int)player.highlighted.Y].preferedTool.index == player.hotbar[player.selected].index) extradmg = 1f * (currentTool.upgrade + 5f);
                            minetimer += 1f + extradmg;
                            chunk[(int)player.highlighted.X, (int)player.highlighted.Y].damage += 1f + extradmg;
                        }
                        if (minetimer >= minetime)
                        {
                            minetimer = -1;
                            player.addToInv(chunk[(int)player.highlighted.X, (int)player.highlighted.Y].Mine((int)player.highlighted.X, (int)player.highlighted.Y), 1);
                            chunk[(int)player.highlighted.X, (int)player.highlighted.Y] = new Air((int)player.highlighted.X, (int)player.highlighted.Y);
                            if (player.hotbar[player.selected] is Tool) currentTool.damage--;
                        }
                    }
                }
            }
        }
        //Render on Screen
        Matrix cameraTransform;
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            //spriteBatch.Begin();
            cameraTransform = Matrix.CreateTranslation(-(Convert.ToInt32(player.Player.Location.X) - 350), -(Convert.ToInt32(player.Player.Location.Y) - 130), zoom);
            //cameraTransform = Matrix.CreateTranslation(-(player.Player.Location.X - 350), -(player.Player.Location.Y - 130), zoom);
            cameraTransform.Translation.Normalize();
            spriteBatch.Begin(SpriteSortMode.Immediate,
                  BlendState.AlphaBlend, SamplerState.AnisotropicClamp, null, null, null,
                  cameraTransform); // moveable objects
            if (menu.state == MenuRef.GameStates.Playing)
            {

                foreach (Block block in chunk)
                {

                    if (block.x > (player.Player.Location.X / 40) - 11 && block.x < (player.Player.Location.X / 40) + 12)
                        if (block.y > (player.Player.Location.Y / 40) - 11 && block.y < (player.Player.Location.Y / 40) + 11)
                            block.Draw(spriteBatch);
                }
                //Draw Blocks
                player.Draw(spriteBatch);//Draw Player
                mobManager.Draw(spriteBatch); // Draw Mobs
               // weather.Draw(spriteBatch);// Draw Weather 
            }

            console.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin(); //static objects
            if (menu.state == MenuRef.GameStates.Playing)
            {
                player.Drawstatic(spriteBatch);
                spriteBatch.DrawString(pericles14, "X: " + (((int)player.Player.Location.X / 40) + 1), new Vector2(690, 10), Color.White);// Draw Current Chunk int
                spriteBatch.DrawString(pericles14, "Y: " + (((int)player.Player.Location.Y / 40) + 1), new Vector2(690, 24), Color.White); // Draw Current Biome


            }
            else menu.Draw(this, spriteBatch);  // Draw Menus
            console.Drawstatic(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
