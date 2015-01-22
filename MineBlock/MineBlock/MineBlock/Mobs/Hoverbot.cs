using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MineBlock.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineBlock.Mobs
{
    public class Hoverbot
    {
        enum BotState {mine,place,follow};
        BotState botstate = BotState.follow;
        public Sprite Botsprite;
        //bool isMineing = false;
        //bool isPlacing = false
            
        int speed = 150;
        Block target1 = new Block();
        Block target2 = new Block();
        int startx;
        int minetimer;
        public Hoverbot()
        {
            Botsprite = new Sprite(new Vector2(10, 10), Game1.hoverbot, new Rectangle(0, 0, 15, 21), Vector2.Zero);
            Botsprite.scale = 1;
            Botsprite.AddFrame(new Rectangle(32, 0, 15, 21));
            Botsprite.AddFrame(new Rectangle(61, 0, 15, 21));
            Botsprite.AddFrame(new Rectangle(88, 0, 15, 21));
        }
        public void update(GameTime time)
        {
           
            if(target1.index != -1)
                flytoLocation(new Vector2(target1.x*40,target1.y*40));
            else flytoLocation(Game1.player.Player.Location);
            if (HandleInputs.isKeyDown("N") && botstate == BotState.follow)
                getTarget1Block(true);
            if (HandleInputs.isKeyDown("M") && botstate == BotState.follow)
                getTarget2Block(true);
            if (HandleInputs.isKeyDown("J") && botstate == BotState.follow)
                getTarget1Block(false);
            if (HandleInputs.isKeyDown("K") && botstate == BotState.follow)
                getTarget2Block(false);
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

            if (Botsprite.Velocity.X != 0)
            {
                Botsprite.Rotation = .0f + (Botsprite.Velocity.X / 100);
                Botsprite.Rotation = MathHelper.Clamp(Botsprite.Rotation, -1, 1);
                if (Botsprite.Velocity.X > 0 && Botsprite.Flip) Botsprite.Flip = false;
                if (Botsprite.Velocity.X < 0 && !Botsprite.Flip) Botsprite.Flip = true;

            }
            else Botsprite.Rotation = 0f;
            if (Botsprite.Velocity == new Vector2(0, 0)) Botsprite.Location = new Vector2(0, 0);
            Botsprite.Velocity = new Vector2(MathHelper.Clamp(Botsprite.Velocity.X, -speed, speed), MathHelper.Clamp(Botsprite.Velocity.Y, -speed, speed));
        }
        public void getTarget1Block(Boolean mine)
        {
            target1 = Game1.chunk[(int)Game1.player.highlighted.X, (int)Game1.player.highlighted.Y];
            startx = target1.x;
         
            
        }
        public void getTarget2Block(Boolean mine)
        {
            target2 = Game1.chunk[(int)Game1.player.highlighted.X, (int)Game1.player.highlighted.Y];
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
            target1 = Game1.chunk[target1.x, target1.y];
            int xcount = Math.Abs((int)(new Vector2(target1.x, target1.y) - new Vector2(target2.x+1, target2.y+1)).X);
            int ycount = Math.Abs((int)(new Vector2(target1.x, target1.y) - new Vector2(target2.x+1, target2.y+1)).Y);
            if (target1.y < target1.y + ycount)
            {
                if (target1.x < target1.x + xcount)
                {
                    if (target1.index != 0)
                    {
                        int minetime = target1.MineTime;
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
                                Game1.player.addToInv(target1.Mine(target1.x, target1.y), 1);
                            Game1.chunk[target1.x, target1.y] = new Air(0, 0);
                            target1 = Game1.chunk[target1.x + 1, target1.y];

                        }
                    }
                    else target1 = Game1.chunk[target1.x + 1, target1.y];
                }
                else { target1 = Game1.chunk[startx, target1.y + 1]; }
            }
            else
            {
                botstate = BotState.follow;
                target1 = new Block();
            }
        }
        public void PlaceBlock()
        {
            target1 = Game1.chunk[target1.x, target1.y];
             int xcount = Math.Abs((int)(new Vector2(target1.x, target1.y) - new Vector2(target2.x+1, target2.y+1)).X);
            int ycount = Math.Abs((int)(new Vector2(target1.x, target1.y) - new Vector2(target2.x+1, target2.y+1)).Y);
            if (target1.y < target1.y + ycount)
            {
                if (target1.x < target1.x + xcount)
                {
                    float dist = Vector2.Distance(Botsprite.Location, new Vector2(target1.x * 40, target1.y * 40));
                    if (target1.index == 0 && Game1.player.count[Game1.player.selected] > 0)
                    {
                        if (dist <= 4)
                        {
                            if (Game1.player.count[Game1.player.selected] > 0)
                                Game1.chunk[target1.x, target1.y] = Game1.player.hotbar[Game1.player.selected].Place(target1.x, target1.y);
                            target1 = Game1.chunk[target1.x + 1, target1.y];
                            Game1.player.count[Game1.player.selected]--;
                            if (Game1.player.count[Game1.player.selected] == 0)
                                Game1.player.hotbar[Game1.player.selected] = new Air((Game1.player.selected * 40) + 16, 16);
                        }
                    }
                    else target1 = Game1.chunk[target1.x + 1, target1.y];
                }
                else target1 = Game1.chunk[startx, target1.y + 1];
            }
            else { target1 = new Block(); botstate = BotState.follow; }
        }
        public void draw(SpriteBatch batch)
        {
            Botsprite.Draw(batch);
        }
    }

}
