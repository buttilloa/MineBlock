using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MineBlock.Blocks;
using MineBlock.Weapons;
using MineBlock.Managers;
using MineBlock.Items;

namespace MineBlock
{
    public class PlayerManager
    {
        public Sprite Player;
        Texture2D Guy, hotboarSheet, HotBoarSelector, pointer, HealthBar, Blank, Cursor;

        Block[,] blocks;
        public Item[] hotbar = new Item[9];
        //Weapon currentWeapon = new Handgun();
        //public List<Bullet> shots = new List<Bullet>();
        public int selected = 0;
        public Vector2 highlighted = new Vector2(0, 0);
        readonly Vector2 gravity = new Vector2(0, 259.8f);
        public Boolean WantsToChangeTP = false;
        public Boolean WantsToChange = false;
        public Boolean drawTeleporterMessage;
        public Color highlightcolor = Color.White;
        public Color hotbarSelector = Color.White;
        public int Health = 100;

        Rectangle Bar = new Rectangle(0, 0, 62, 20);
        Rectangle Bar2;
        Vector2 chestInvLocation;
        Boolean drawChestInv = false;
        Block[] chestInv = new Block[9];
        int[] chestInvCount = new int[9];
        String teleporterMessage;
        public String ChunkTp = "";
        public Inventory playerinv;
        private SpriteFont pericles14, pericles1;

        public PlayerManager(Texture2D sheet, Texture2D hotbatsheet, Texture2D hotbarselector)
        {
            Guy = sheet;
            hotboarSheet = hotbatsheet;
            HotBoarSelector = hotbarselector;
            for (int i = 0; i < 9; i++)
            {
                hotbar[i] = new Air((i * 40) + 16, 16).ItemBlock();
                
            }
            hotbar[0] = new Ladder(16, 16).ItemBlock();
            hotbar[0].Count = 16;
            hotbar[1] = new Trampoline((1 * 40) + 16, 16).ItemBlock();
            hotbar[1].Count = 1;
            hotbar[2] = new PigBlock((2 * 40) + 16, 16).ItemBlock();
            hotbar[2].Count = 1;
            hotbar[3] = new Pick(5);
            hotbar[4] = new Shovel(5);
            

            Player = new Sprite(new Vector2(30, 30), Guy, new Rectangle(2, 122, 102, 120), Vector2.Zero);
            Player.AddFrame(new Rectangle(2, 122, 102, 120));
            Player.AddFrame(new Rectangle(2, 122, 102, 120));
            Player.AddFrame(new Rectangle(2, 122, 102, 120));
            Player.AddFrame(new Rectangle(2, 122, 102, 120));
            playerinv = new Inventory(hotbatsheet);
            //playerinv.displayinv();
            pericles14 = Tm.getFont(Tm.Font.f14);
            pericles1 = Tm.getFont(Tm.Font.f1);
            pointer = Tm.getTexture(Tm.Texture.Pointer);
            Cursor = Tm.getTexture(Tm.Texture.cursor);
            Blank = Tm.getTexture(Tm.Texture.Blank);
            HealthBar = Tm.getTexture(Tm.Texture.HealthBar);


        }
        public void updateBlocks(Block[,] block)
        {
            blocks = block;
        }
        Block bottomBlock()
        {
            return blocks[(int)((Player.Location.X -10)/ 40) + 1, (int)((Player.Location.Y + 40) / 40) + 1];
        }
        public void update(GameTime time)
        {
            Health = (int)MathHelper.Clamp(Health, 0, 100);
         
            try
            {
               if ((bottomBlock().index == 83 && HandleInputs.isKeyDown("S")) || !bottomBlock().isSolid)
                {
                    float timed = (float)time.ElapsedGameTime.TotalSeconds;
                    Player.Velocity += gravity * timed;

                }
                else
                {
                    if (Player.Velocity.Y > 178)
                        if (Health > 0)
                            Health -= (int)Player.Velocity.Y / 20;
                    Player.Velocity = new Vector2(Player.Velocity.X, 0);
                }
                
                bottomBlock().EntityStandingEvent(this);
            }
            catch (System.IndexOutOfRangeException) { Console.WriteLine("ERROR: NO BLOCK BELOW YOU"); }
            if (Player.Velocity.X > 0)
            {
                Player.Flip = false;
                Player.frames[1] = new Rectangle(120, 127, 106, 114);
                Player.frames[2] = new Rectangle(240, 122, 106, 121);
                Player.frames[3] = new Rectangle(360, 127, 106, 114);
                Player.frames[4] = new Rectangle(480, 122, 106, 121);
            }
            else if (Player.Velocity.X < 0)
            {
                Player.Flip = true;
                Player.frames[1] = new Rectangle(120, 127, 106, 114);
                Player.frames[2] = new Rectangle(240, 122, 106, 121);
                Player.frames[3] = new Rectangle(360, 127, 106, 114);
                Player.frames[4] = new Rectangle(480, 122, 106, 121);
            }
            else if (Player.Velocity.Y == 0 || Player.Velocity.X == 0)
            {

                Player.frames[1] = new Rectangle(2, 122, 102, 120);
                Player.frames[2] = new Rectangle(2, 122, 102, 120);
                Player.frames[3] = new Rectangle(2, 122, 102, 120);
                Player.frames[4] = new Rectangle(2, 122, 102, 120);
            }
           
            if (playerinv.isdisplayed)
                highlighted = HandleInputs.getMousepos();
            else highlighted = HandleInputs.moveHighlighter(highlighted) + ((Player.Location / 40) - new Vector2(8, 3));
            try
            {
                if (HandleInputs.isKeyDown("D") && (!RightBlock().isSolid)) Player.Velocity = new Vector2(150, Player.Velocity.Y);
                else if (HandleInputs.isKeyDown("A") && (!LeftBlock().isSolid)) Player.Velocity = new Vector2(-150, Player.Velocity.Y);
                else Player.Velocity = new Vector2(0, Player.Velocity.Y);
            }
            catch (System.IndexOutOfRangeException) { }

            if (HandleInputs.isKeyDown("W") && (isonGround() || isOnLadder()) && Player.Location.Y > 0)
            {
                if (!BlockAbove().isSolid)
                    Player.Velocity = new Vector2(0, -175);
            }
            if (HandleInputs.isKeyUp("T") && ChunkTp != "")
                if (Convert.ToInt32(ChunkTp) > -1 && Convert.ToInt32(ChunkTp) < Game1.chunk.Length)
                {
                    blocks[(int)(Player.Location.X / 40) + 1, (int)((Player.Location.Y + 40) / 40) + 1].switchTeleporter(true);
                    teleporterMessage = "Yes Sir";
                    WantsToChangeTP = true;
                }
            if (bottomBlock().index == 46)
            {
                blocks[(int)(Player.Location.X / 40) + 1, (int)((Player.Location.Y + 40) / 40) + 1].switchTeleporter(false);
            }
            if (HandleInputs.isKeyDown("T"))
            {
                if (Player.Location.X <= -30 || Player.Location.X >= 730) WantsToChange = true;
                if (blocks[(int)(Player.Location.X / 40) + 1, (int)((Player.Location.Y + 40) / 40) + 1].index == 45)
                {
                    teleporterMessage = "Enter Chunk to Tp to: " + ChunkTp;
                    drawTeleporterMessage = true;
                    if (HandleInputs.isKeyDown("Back")) ChunkTp = "";
#if WINDOWS
                    ChunkTp += HandleInputs.SimNumPad(ChunkTp);
#endif
#if XBOX
                    ChunkTp = HandleInputs.SimNumPad(ChunkTp);
#endif
                }
            }
            selected = HandleInputs.HotBar(selected);
           
            if(playerinv.isdisplayed)
            playerinv.handlemovement();
            Player.Update(time);

        }
        public void addToChest(Chest chest, int HotBarslot)
        {
            if (hotbar[HotBarslot].Count > 0)
                chest.addToInv(hotbar[HotBarslot].ReturnBlock(), 1);
            hotbar[HotBarslot].Count--;
            if (hotbar[HotBarslot].Count <= 0)
                hotbar[HotBarslot] = new Air((HotBarslot * 40) + 16, 16).ItemBlock();

        }
        public void takeFromChest(int slot)
        {
            if (chestInvCount[slot] > 0)
            {
                addToInv(chestInv[slot], 1);
                chestInvCount[slot]--;
            }
            if (chestInvCount[slot] <= 0) chestInv[slot] = new Air(0, 0);
        }
        Block RightBlock()
        {
            return blocks[(int)((Player.Location.X + 30) / 40) + 1, (int)((Player.Location.Y + 40) / 40)];
        }
        Block LeftBlock()
        {
            return blocks[(int)((Player.Location.X + 70) / 40) - 1, (int)((Player.Location.Y + 40) / 40)];
        }
        Block BlockAbove()
        {
            return blocks[(int)(Player.Location.X / 40) + 1, (int)((Player.Location.Y + 40) / 40) - 1];
        }
        Boolean isOnLadder()
        {
            if (blocks[(int)(Player.Location.X / 40) + 1, (int)(Player.Location.Y / 40) + 1].index == 83)
                return true;
            return false;
        }
        Boolean isonGround()
        {
            if (blocks[(int)(Player.Location.X / 40) + 1, (int)(Player.Location.Y / 40) + 2].index != 0)
                return true;
            return false;
        }
        public void Jump()
        {
            Player.Velocity = new Vector2(0, - (175 + Game1.randy.Next(0,10)));
        }
        public void addToInv(Block newBlock, int BlockCount)
        {
            Boolean isInBar = false;
            for (int i = 0; i < 9; i++)
            {
                if (newBlock.index == hotbar[i].Blockindex)
                {
                    hotbar[i].Count++;
                    isInBar = true;
                    break;
                }
            }
            if (!isInBar)
                for (int i = 0; i < 9; i++)
                {
                    if (hotbar[i].Blockindex == 0)
                    {
                        hotbar[i] = newBlock.Reset((i * 40) + 16, 16).ItemBlock();
                        hotbar[i].Count += BlockCount;
                        break;
                    }
                }
            playerinv.addToInv(newBlock, BlockCount);
        }
        public void clearinv()
        {
            for (int i = 0; i < 9; i++)
                hotbar[i] = new Air((i * 40) + 16, 16).ItemBlock();


        }
        public void useChest(Chest chest)
        {
            SoundEffects.ChestOpen.Play();
            chest.index = 27;
            Console.WriteLine("Using Chest");
            chestInv = chest.items;
            chestInvCount = chest.count;
            chestInvLocation = new Vector2((chest.x * 40) - 70, (chest.y * 40) - 40);
            drawChestInv = true;

        }
        public void Drawstatic(SpriteBatch batch)
        {
            batch.Draw(hotboarSheet, new Rectangle(10, 10, 362, 42), Color.White);
            for (int i = 0; i < 9; i++)
            {
                //if (count[i] == 0) hotbar[i] = new Air((i * 40) + 16, 16);
                if (hotbar[i].Count > 0)
                {
                    hotbar[i].DrawMini(batch, (i * 40) + 16, 16);
                   if(hotbar[i].hasCount) batch.DrawString(pericles14, "" + hotbar[i].Count, new Vector2((i * 40) + 21, 19), Color.White);
                }
            }
            batch.Draw(HotBoarSelector, new Rectangle((selected * 40) + 7, 7, 48, 48), hotbarSelector);

            if (drawTeleporterMessage)
                batch.DrawString(pericles14, teleporterMessage, new Vector2(320, 200), Color.White);
            //batch.Draw(hotboarSheet, new Vector2(400, 12), new Rectangle(2, 2, 39, 40), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            //currentWeapon.DrawInInv(batch, 400, 12);
            if (playerinv.isdisplayed)
            {
                playerinv.draw(batch);
                batch.Draw(pointer, new Rectangle((int)highlighted.X, (int)highlighted.Y, 12, 19), Game1.cursorColor);
            }
        }
        public void Draw(SpriteBatch batch)
        {
            Player.Draw(batch);
           
            Bar = new Rectangle((int)Player.Location.X + 23, (int)Player.Location.Y + 10, 60, 10);
            Bar2 = new Rectangle(Bar.X + 5, Bar.Y + 2, Health / 2, 4);
            batch.Draw(HealthBar, Bar, Color.White);
            batch.Draw(Blank, Bar2, Health > 50 ? Color.Green : Health > 25 ? Color.Orange : Color.Red);
            batch.DrawString(pericles1, "" + Health, new Vector2(Bar2.X + 16, Bar2.Y - 20), Color.White);
            //currentWeapon.DrawInHand(batch, (int)Player.Location.X, (int)Player.Location.Y, Player.Flip);
            hotbar[selected].DrawInHand(batch, (int)Player.Location.X, (int)Player.Location.Y, Player.Flip);
            //foreach (Bullet shot in shots)
            //    shot.Draw(batch);

            if (drawChestInv)
            {
                batch.Draw(hotboarSheet, new Rectangle((int)chestInvLocation.X, (int)chestInvLocation.Y, 181, 21), Color.White);
                for (int i = 0; i < 9; i++)
                {
                    if (chestInvCount[i] > 0)
                    {
                        float xPoss = (chestInvLocation.X + (i * 19.9f)) + 3;
                        float yPoss = chestInvLocation.Y + 3;
                        chestInv[i].DrawInChest(batch, xPoss, yPoss);
                        batch.DrawString(pericles1, "" + chestInvCount[i], new Vector2(xPoss, yPoss), Color.White);
                    }
                }
            }
            if (!playerinv.isdisplayed) batch.Draw(Cursor, new Rectangle((int)highlighted.X * 40, (int)highlighted.Y * 40, 40, 40), highlightcolor);

        }
    }
}
