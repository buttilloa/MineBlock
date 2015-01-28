using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace MineBlock.Blocks
{
    class SnowyGrass : Block
    {
        public SnowyGrass(int XPos, int yPos)
        {
            x = XPos;
            y = yPos;
            index = 68;
            MineTime = 60;
            preferedTool = new MineBlock.Items.Shovel(0);
        }
     
        public override Block Reset(int X, int Y)
        {
            return new SnowyGrass(X, Y);
        }
        public override Block Place(int x, int y)
        {

            return new SnowyGrass(x, y);
        }

        public override Block Mine(int x, int y)
        {

            return new Snow(x, y);
        }
    }
}
