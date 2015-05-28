using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace MineBlock.Blocks
{
    class Gravel : IGravityBlock
    {


        public Gravel(int XPos, int yPos)
            : base()
        {
            x = XPos;
            y = yPos;
            index = 19;
            MineTime = 90;
            preferedTool = new MineBlock.Items.Shovel(0);
        }


        public override Block Reset(int X, int Y)
        {
            return new Gravel(X, Y);
        }
        public override Block Place(int x, int y)
        {

            return new Gravel(x, y);
        }

        public override Block Mine(int x, int y)
        {

            return new Gravel(x, y);
        }

    }
}
