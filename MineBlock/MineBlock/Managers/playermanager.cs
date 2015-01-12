using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MineBlock.Blocks;
using MineBlock.Weapons;

namespace MineBlock
{
    public class PlayerManager
    {
        public Sprite Player;
        Texture2D Guy, hotboarSheet, HotBoarSelector;

        Block[,] blocks = new Block[20, 15];
        public Block[] hotbar = new Block[9];
        public int[] count = new int[9];
        Weapon currentWeapon = new Handgun();
        public List<Bullet> shots= new List<Bullet>();
        public int selected = 0;
        public Vector2 highlighted = new Vector2(0, 0);
        readonly Vector2 gravity = new Vector2(0, 259.8f);
        public Boolean WantsToChangeTP = false;
        public Boolean WantsToChange = false;
        public Boolean drawTeleporterMessage;
        public int Health = 100;
     
        Rectangle Bar = new Rectangle(0, 0, 62, 20);
        Rectangle Bar2;
        Vector2 chestInvLocation;
        Boolean drawChestInv = false;
        Block[] chestInv = new Block[9];
        int[] chestInvCount = new int[9];
        String teleporterMessage;
        public String ChunkTp = "";
        
        public PlayerManager(Texture2D sheet, Texture2D hotbatsheet, Texture2D hotbarselector)
        {
            Guy = sheet;
            hotboarSheet = hotbatsheet;
            HotBoarSelector = hotbarselector;
            for (int i = 0; i < 9; i++)
            {
                hotbar[i] = new Air((i * 40) + 16, 16);
                count[i] = 0;
            }
            hotbar[0] = new Ladder(16, 16);
            count[0] = 16;
            hotbar[1] = new Chest((1 * 40) + 16, 16);
            count[1] = 1;
            hotbar[2] = new PigBlock((2 * 40) + 16, 16);
            count[2] = 1;

            Player = new Sprite(new Vector2(30, 30), Guy, new Rectangle(2, 122, 102, 120), Vector2.Zero);
            Player.AddFrame(new Rectangle(2, 122, 102, 120));
            Player.AddFrame(new Rectangle(2, 122, 102, 120));
            Player.AddFrame(new Rectangle(2, 122, 102, 120));
            Player.AddFrame(new Rectangle(2, 122, 102, 120));


        }
        public void updateBlocks(Block[,] block)
        {
            blocks = block;
        }
        int bottomBlock()
        {
            return blocks[(int)(Player.Location.X / 40) + 1, (int)((Player.Location.Y + 40) / 40) + 1].index;
        }
        public void update(GameTime time)
        {
             Health = (int)MathHelper.Clamp(Health, 0, 100);
            // if (Health < 100)
           //{
         
             //  Health++;
           //}
            try
            {

                if (bottomBlock() == 0 || bottomBlock() == 14 || bottomBlock() == 11 || (bottomBlock() == 83 && HandleInputs.isKeyDown("Down")))
                {
                    float timed = (float)time.ElapsedGameTime.TotalSeconds;
                  
                    Player.Velocity += gravity * timed;

                }
                else
                {
                    if (Player.Velocity.Y > 178)  
                        if(Health>0)
                        Health -= (int)Player.Velocity.Y / 20;
                     
                  
                    Player.Velocity = new Vector2(Player.Velocity.X, 0);
                }
                 if (bottomBlock() == 158)
                 {
                     Commandblock block = (Commandblock)blocks[(int)(Player.Location.X / 40) + 1, (int)((Player.Location.Y + 40) / 40) + 1];
                     block.Activate();
                     block.index = 159;
                     
                 }
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

            if (Player.Location.X <= 2 && Player.Velocity.X <= -1)
                Player.Velocity = new Vector2(0, Player.Velocity.Y);
            if (Player.Location.X >= 798 && Player.Velocity.X >= -1)
                Player.Velocity = new Vector2(0, Player.Velocity.Y);

            highlighted = HandleInputs.moveHighlighter(highlighted);



            try
            {
                if (HandleInputs.isKeyDown("Right") && (RightBlock() == 0 || RightBlock() == 14 || RightBlock() == 83)) Player.Velocity = new Vector2(150, Player.Velocity.Y);
                else if (HandleInputs.isKeyDown("Left") && (LeftBlock() == 0 || LeftBlock() == 14 || LeftBlock() == 83)) Player.Velocity = new Vector2(-150, Player.Velocity.Y);
                else Player.Velocity = new Vector2(0, Player.Velocity.Y);
            }
            catch (System.IndexOutOfRangeException) { }

            if (HandleInputs.isKeyDown("Up") && (isonGround() || isOnLadder()) && Player.Location.Y > 0)
            {
                if (BlockAbove() == 0 || BlockAbove() == 14 || BlockAbove() == 83)
                    Player.Velocity = new Vector2(0, -175);
            }
            if (HandleInputs.isKeyUp("T") && ChunkTp != "")
                if (Convert.ToInt32(ChunkTp) > -1 && Convert.ToInt32(ChunkTp) < Game1.chunks.Count())
                {
                    blocks[(int)(Player.Location.X / 40) + 1, (int)((Player.Location.Y + 40) / 40) + 1].switchTeleporter(true);
                    teleporterMessage = "Yes Sir";
                    WantsToChangeTP = true;
                }
            if (bottomBlock() == 46)
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
            if (drawChestInv)
            {
                if (HandleInputs.NumPadKeys() != 10)
                    takeFromChest(HandleInputs.NumPadKeys());
                if (HandleInputs.isKeyDown("Enter"))
                    addToChest((Chest)blocks[((int)chestInvLocation.X + 70) / 40, ((int)chestInvLocation.Y + 40) / 40], selected);
                if (HandleInputs.isKeyDown("Back"))
                {
                    drawChestInv = false;
                    blocks[((int)chestInvLocation.X + 70) / 40, ((int)chestInvLocation.Y + 40) / 40].index = 26;
                }
            }
            if (HandleInputs.isKeyDown("Space"))
                currentWeapon.Shoot(this, Player.Flip);
            foreach (Bullet shot in shots)
            {
                shot.update(time);
               
            }
            foreach (Bullet shot in shots)
            {
                shot.update(time);
                if (shot.shot.Location.X >= 770 || shot.shot.Location.X <= -30)
                {
                    shots.Remove(shot); 
                    break;
                }
            }
            Player.Update(time);

        }
        public void addToChest(Chest chest, int HotBarslot)
        {
            if (count[HotBarslot] > 0)
                chest.addToInv(hotbar[HotBarslot], 1);
            count[HotBarslot]--;
            if (count[HotBarslot] <= 0)
                hotbar[HotBarslot] = new Air((HotBarslot * 40) + 16, 16);

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
        int RightBlock()
        {
            return blocks[(int)((Player.Location.X + 30) / 40) + 1, (int)((Player.Location.Y + 40) / 40)].index;
        }
        int LeftBlock()
        {
            return blocks[(int)((Player.Location.X + 70) / 40) - 1, (int)((Player.Location.Y + 40) / 40)].index;
        }
        int BlockAbove()
        {
            return blocks[(int)(Player.Location.X / 40) + 1, (int)((Player.Location.Y + 40) / 40) - 1].index;
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
        public void addToInv(Block newBlock, int BlockCount)
        {
            Boolean isInBar = false;
            for (int i = 0; i < 9; i++)
            {
                if (newBlock.index == hotbar[i].index)
                {
                    count[i]++;
                    isInBar = true;
                    break;
                }
            }
            if (!isInBar)
                for (int i = 0; i < 9; i++)
                {
                    if (hotbar[i].index == 0)
                    {
                        hotbar[i] = newBlock.Reset((i * 40) + 16, 16);
                        count[i] += BlockCount;
                        break;
                    }
                }
        }
        public void clearinv()
        {
            for (int i = 0; i < 9; i++)
            {
                hotbar[i] = new Air((i * 40) + 16, 16);
                count[i] = 0;
            }
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
        public void Draw(SpriteBatch batch)
        {
            Player.Draw(batch);
            batch.Draw(hotboarSheet, new Rectangle(10, 10, 362, 42), Color.White);
            for (int i = 0; i < 9; i++)
            {
                if (count[i] == 0) hotbar[i] = new Air((i * 40) + 16, 16);
                if (count[i] > 0)
                {
                    hotbar[i].DrawMini(batch);
                    batch.DrawString(Game1.pericles14, "" + count[i], new Vector2(hotbar[i].x + 5, hotbar[i].y + 3), Color.White);
                }
            }
            batch.Draw(HotBoarSelector, new Rectangle((selected * 40) + 7, 7, 48, 48), Color.White);
            batch.Draw(Game1.cursor, new Rectangle((int)highlighted.X * 40, (int)highlighted.Y * 40, 40, 40), Color.White);
            if (drawTeleporterMessage)
                batch.DrawString(Game1.pericles14, teleporterMessage, new Vector2(320, 200), Color.White);
            Bar = new Rectangle((int)Player.Location.X + 23, (int)Player.Location.Y+10, 60, 10);
            Bar2 = new Rectangle(Bar.X + 5, Bar.Y + 2, Health / 2, 4);
            batch.Draw(Game1.HealthBar, Bar, Color.White);
            batch.Draw(Game1.Weather, Bar2, Health > 50 ? Color.Green : Health > 25 ? Color.Orange : Color.Red);
            batch.DrawString(Game1.pericles1,""+Health, new Vector2(Bar2.X+16,Bar2.Y-20), Color.White);
            currentWeapon.DrawInHand(batch,(int)Player.Location.X, (int)Player.Location.Y,Player.Flip);
            batch.Draw(hotboarSheet, new Vector2(400,12), new Rectangle(2, 2, 39, 40), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            foreach (Bullet shot in shots)
                shot.Draw(batch);
            currentWeapon.DrawInInv(batch,400,12);
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
                        batch.DrawString(Game1.pericles1, "" + chestInvCount[i], new Vector2(xPoss, yPoss), Color.White);
                    }
                }
            }

        }
    }
}
