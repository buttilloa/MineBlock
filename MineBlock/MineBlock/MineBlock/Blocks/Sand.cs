using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace MineBlock.Blocks
{
    class
        Sand : IGravityBlock
    {
        public Sand(int XPos, int yPos)
            : base()
        {
            x = XPos;
            y = yPos;
            index = 18;
            MineTime = 60;
            preferedTool = new MineBlock.Items.Shovel(0);
        }


        public override Block Reset(int X, int Y)
        {
            return new Sand(X, Y);
        }
        public override Block Place(int x, int y)
        {

            return new Sand(x, y);
        }

        public override Block Mine(int x, int y)
        {

            return new Sand(x, y);
        }

    }
}
