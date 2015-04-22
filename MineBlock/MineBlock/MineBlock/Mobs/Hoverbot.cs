using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MineBlock.Blocks;

namespace MineBlock.Mobs
{
    public class Hoverbot
    {
        enum BotState { mine, place, follow, idle  };
        BotState botstate = BotState.follow;
        public Sprite Botsprite;
        int speed = 150;
        Block target1 = new Block();
        Block target2 = new Block();
        int startx;
        int minetimer;
        bool drawinv = false;
        Vector2 InvLocation;
        Block[] Inv = new Block[9];
        int[] count = new int[9];
        bool canChangeState = true;
        SpriteFont pericles1;
        Texture2D hoverbot, hotbarsheet;
      
        public Hoverbot()
        {
             hoverbot = Tm.getTexture(Tm.Texture.hoverbot);
            Botsprite = new Sprite(new Vector2(10, 10), hoverbot, new Rectangle(0, 0, 15, 21), Vector2.Zero);
            Botsprite.scale = 1;
            Botsprite.AddFrame(new Rectangle(32, 0, 15, 21));
            Botsprite.AddFrame(new Rectangle(61, 0, 15, 21));
            Botsprite.AddFrame(new Rectangle(88, 0, 15, 21));
           
            for (int i = 0; i < 9; i++)
            {
                Inv[i] = new Air(((int)Botsprite.Location.X + (i * 20)) + 3, (int)Botsprite.Location.Y + 3);
                count[i] = 0;
            }
           
              hotbarsheet = Tm.getTexture(Tm.Texture.hotbarsheet);
              pericles1 = Tm.getFont(Tm.Font.f1);
        }
       
        
        public void update(GameTime time)
        {

            if (!drawinv)
                if (Math.Abs(Vector2.Distance(Botsprite.Location, Game1.player.highlighted * 40)) <= 40)
                {
                    drawinv = true;
                    minetimer = 0;
                }
            if (drawinv)
            {
                if (minetimer <= 60)
                    minetimer++;
                if (minetimer > 60)
                {
                    minetimer = -1;
                    if (Math.Abs(Vector2.Distance(Botsprite.Location, Game1.player.highlighted * 40)) > 40)
                    drawinv = false;
                }
            }
            if (target1.index != -1)
                flytoLocation(new Vector2(target1.x * 40, target1.y * 40));
            else if (botstate != BotState.idle) flytoLocation(Game1.player.Player.Location);
            if (HandleInputs.isKeyDown("N") && botstate == BotState.follow)
                getTarget1Block(true);
            if (HandleInputs.isKeyDown("M") && botstate == BotState.follow)
                getTarget2Block(true);
            if (HandleInputs.isKeyDown("J") && botstate == BotState.follow)
                getTarget1Block(false);
            if (HandleInputs.isKeyDown("K") && botstate == BotState.follow)
                getTarget2Block(false);
            if (HandleInputs.isKeyDown("C") && canChangeState)
            {
                canChangeState = false;
                if (botstate != BotState.follow)
                { botstate = BotState.follow;
                target1 = new Block(); target2 = new Block();
                }
                else if (botstate == BotState.follow)
                    botstate = BotState.idle;
           }
            if (botstate == BotState.idle) {Botsprite.Velocity = Vector2.Zero; Botsprite.Rotation = 0; }
            if (!canChangeState)
                if (HandleInputs.isKeyUp("C")) canChangeState = true;
            if (botstate == BotState.mine)
                mineBlock();
            if (botstate == BotState.place)
                PlaceBlock();
            Botsprite.Update(time);
        }

        public void flytoLocation(Vector2 location)
        {
            Botsprite.Velocity = (location - Botsprite.Location) * 2f;
            Botsprite.Velocity.Normalize();
            if (Botsprite.Velocity == new Vector2(0, 0)) Botsprite.Location = new Vector2(0, 0);
            Botsprite.Velocity = new Vector2(MathHelper.Clamp(Botsprite.Velocity.X, -speed, speed), MathHelper.Clamp(Botsprite.Velocity.Y, -speed, speed));
            if (Botsprite.Velocity.X != 0)
            {
                Botsprite.Rotation = .0f + (Botsprite.Velocity.X / 100);
                Botsprite.Rotation = MathHelper.Clamp(Botsprite.Rotation, -1, 1);
                if (Botsprite.Velocity.X > 0 && Botsprite.Flip) Botsprite.Flip = false;
                if (Botsprite.Velocity.X < 0 && !Botsprite.Flip) Botsprite.Flip = true;

            }
            else Botsprite.Rotation = 0f;
        }
        public void getTarget1Block(Boolean mine)
        {
            target1 = Chunk.getBlockAt(Game1.Loadedchunks,(int)Game1.player.highlighted.X, (int)Game1.player.highlighted.Y);
            startx = target1.x;


        }
        public void getTarget2Block(Boolean mine)
        {
            target2 = Chunk.getBlockAt(Game1.Loadedchunks,(int)Game1.player.highlighted.X, (int)Game1.player.highlighted.Y);
            if (mine)
            {
                // target2.MineTime *= 2;
                if (target2.index != 0 && target2.canMine)
                {
                    botstate = BotState.mine;

                }
            }
            else botstate = BotState.place;

        }

        public void mineBlock()
        {
            target1 = Chunk.getBlockAt(Game1.Loadedchunks,target1.x, target1.y);
            int xcount = Math.Abs((int)(new Vector2(target1.x, target1.y) - new Vector2(target2.x + 1, target2.y + 1)).X);
            int ycount = Math.Abs((int)(new Vector2(target1.x, target1.y) - new Vector2(target2.x + 1, target2.y + 1)).Y);
            if (target1.y < target1.y + ycount)
            {
                if (target1.x < target1.x + xcount)
                {
                    if (target1.index != 0)
                    {
                        float minetime = target1.MineTime;
                        if (minetimer == -1)
                            minetimer = 0;
                        float dist = Vector2.Distance(Botsprite.Location, new Vector2(target1.x * 40, target1.y * 40));
                        if (minetimer > -1 && minetimer < minetime && dist <= 2)
                        {
                            minetimer += 5;
                            target1.damage += 5;
                        }
                        if (minetimer >= minetime)
                        {
                            minetimer = -1;
                            if (target1.index != 0)
                               addToInv(target1.Mine(target1.x, target1.y), 1);
                            Chunk.SetBlock(Game1.Loadedchunks,target1.x, target1.y, new Air(0, 0));
                            target1 = Chunk.getBlockAt(Game1.Loadedchunks,target1.x + 1, target1.y);

                        }
                    }
                    else target1 = Chunk.getBlockAt(Game1.Loadedchunks,target1.x + 1, target1.y);
                }
                else { target1 = Chunk.getBlockAt(Game1.Loadedchunks,startx, target1.y + 1); }
            }
            else
            {
                botstate = BotState.follow;
                target1 = new Block();
            }
        }
        public void PlaceBlock()
        {
            target1 = Chunk.getBlockAt(Game1.Loadedchunks,target1.x, target1.y);
            int xcount = Math.Abs((int)(new Vector2(target1.x, target1.y) - new Vector2(target2.x + 1, target2.y + 1)).X);
            int ycount = Math.Abs((int)(new Vector2(target1.x, target1.y) - new Vector2(target2.x + 1, target2.y + 1)).Y);
            if (target1.y < target1.y + ycount)
            {
                if (target1.x < target1.x + xcount)
                {
                    float dist = Vector2.Distance(Botsprite.Location, new Vector2(target1.x * 40, target1.y * 40));
                    if (target1.index == 0 && Game1.player.hotbar[Game1.player.selected].Count > 0)
                    {
                        if (dist <= 4)
                        {
                            if (Game1.player.hotbar[Game1.player.selected].Count > 0)
                                Chunk.SetBlock(Game1.Loadedchunks,target1.x, target1.y,  Game1.player.hotbar[Game1.player.selected].ReturnBlock().Place(target1.x, target1.y));
                            target1 = Chunk.getBlockAt(Game1.Loadedchunks,target1.x + 1, target1.y);
                            Game1.player.hotbar[Game1.player.selected].Count--;
                            if (Game1.player.hotbar[Game1.player.selected].Count == 0)
                                Game1.player.hotbar[Game1.player.selected] = new Air((Game1.player.selected * 40) + 16, 16).ItemBlock();
                        }
                    }
                    else target1 = Chunk.getBlockAt(Game1.Loadedchunks,target1.x + 1, target1.y);
                }
                else target1 = Chunk.getBlockAt(Game1.Loadedchunks,startx, target1.y + 1);
            }
            else { target1 = new Block(); botstate = BotState.follow; }
        }
        public void addToInv(Block newBlock, int BlockCount)
        {
            Boolean isInBar = false;
            for (int i = 0; i < 9; i++)
            {
                if (newBlock.index == Inv[i].index)
                {
                    count[i]++;
                    isInBar = true;
                    break;
                }
            }
            if (!isInBar)
                for (int i = 0; i < 9; i++)
                {
                    if (Inv[i].index == 0)
                    {
                        Inv[i] = newBlock.Reset((i * 40) + 16, 16);
                        count[i] += BlockCount;
                        break;
                    }
                }
        }
        public void draw(SpriteBatch batch)
        {
            if (target1.index >= 0 && botstate != BotState.follow)
                DrawLine(batch, new Vector2((target1.x * 40) + 20, (target1.y * 40) + 20), new Vector2(Botsprite.Location.X + 6, Botsprite.Location.Y + 13));
            Botsprite.Draw(batch);
            if (drawinv)
            {
             InvLocation = new Vector2(Botsprite.Location.X-85, Botsprite.Location.Y-30);
                batch.Draw(hotbarsheet, new Rectangle((int)InvLocation.X, (int)InvLocation.Y, 181, 21), Color.White);
                for (int i = 0; i < 9; i++)
                {
                    if (count[i] > 0)
                    {
                        float xPoss = (InvLocation.X + (i * 19.9f)) + 3;
                        float yPoss = InvLocation.Y + 3;
                        Inv[i].DrawInChest(batch, xPoss, yPoss);
                        batch.DrawString(pericles1, "" + count[i], new Vector2(xPoss, yPoss), Color.White);
                    }
                }
            }
        }
        public static void DrawLine(SpriteBatch sb, Vector2 start, Vector2 end)
        {
            Game1.DrawLine(sb, start, end);
        }
    }

}
