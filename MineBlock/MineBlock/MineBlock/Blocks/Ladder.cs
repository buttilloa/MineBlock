using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace MineBlock.Blocks
{
    class Ladder : IGravityBlock
    {

        public Ladder(int XPos, int yPos)
            : base()
        {
            x = XPos;
            y = yPos;
            index = 83;
            MineTime = 30;
            isSolid = false;
        }

        public override Block Reset(int X, int Y)
        {
            return new Ladder(X, Y);
        }
        public override Block Place(int x, int y)
        {

            return new Ladder(x, y);
        }

        public override Block Mine(int x, int y)
        {

            return new Ladder(x, y);
        }

    }
}
