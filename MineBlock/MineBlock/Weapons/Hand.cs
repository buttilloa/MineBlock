using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineBlock.Weapons
{
    class Hand : Weapon
    {
        public Hand()
        {
            index = 0;
        }
        public override void DrawInHand(SpriteBatch batch, int x, int y,Boolean flip)
        {


        }
        public override void DrawInInv(SpriteBatch batch, int x, int y)
        {


        }
    }
}
