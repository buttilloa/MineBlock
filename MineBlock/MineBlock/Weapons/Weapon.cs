using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineBlock
{
    class Weapon
    {

        public int index;
     public Weapon()
        {

        }

     public virtual void Shoot(PlayerManager player, Boolean Flip)
     {
     
     }
     public virtual void DrawInHand(SpriteBatch batch, int x, int y, Boolean Flip)
     {


        }
        public virtual void DrawInInv(SpriteBatch batch, int x, int y)
        {


        }
    }
}
