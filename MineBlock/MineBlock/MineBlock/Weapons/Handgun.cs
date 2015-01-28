using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineBlock.Weapons
{
    class Handgun : Weapon
    {
        int X,Y;
        public Handgun()
        {
            index = 1;
        }
        public override void Shoot(PlayerManager player, Boolean Flip)
        {
            //player.shots.Add(new Bullet(X, Y, Flip));
           
        }
        
    
        public override void DrawInHand(SpriteBatch batch, int x, int y, Boolean Flip)
        {

            if (!Flip)
            {
                X = x + 70; Y = y + 60;
                batch.Draw(Game1.HandGun, new Rectangle(X, Y, 22, 15), Color.White);
            }
            else
            {
                X = x + 10; Y = y + 60;
                batch.Draw(Game1.HandGun, new Vector2(X, Y), new Rectangle(0, 0, 44, 30), Color.White, 0f, Vector2.Zero, 0.5f, Flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            }
        }
        public override void DrawInInv(SpriteBatch batch, int x, int y)
        {
            batch.Draw(Game1.HandGun, new Rectangle(x+4, y+10 + 00, 33, 20), Color.White);
        }
    }
}
