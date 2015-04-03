using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace MineBlock.Blocks
{
    class Trampoline : Block
    {
        public Trampoline(int XPos, int yPos)
        {
            x = XPos;
            y = yPos;
            index = 65;
           preferedTool = new MineBlock.Items.Pick(0);
        }
        public override void EntityStandingEvent(object caller)
        {
            if(caller is PlayerManager)
            {
                PlayerManager player = (PlayerManager)caller;
                player.Jump();
            }
            if(caller is Mob)
            {
                Mob mob = (Mob)caller;
                mob.Jump();
            }
        }
        public override Block Place(int x, int y)
        {

            return new Trampoline(x, y);
        }

        public override Block Mine(int x, int y)
        {

            return new Trampoline(x, y);
        }
        public override Block Reset(int X, int Y)
        {
            return new Trampoline(X, Y);
        }
    }
}
