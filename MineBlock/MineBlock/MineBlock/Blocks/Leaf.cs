using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace MineBlock.Blocks
{
    class Leaf : Block
    {
        public Leaf(int XPos, int yPos)
        {
            x = XPos;
            y = yPos;
            index = 53;
            MineTime = 40;
            preferedTool = new MineBlock.Items.Pick(0);
        }
      
        public override Block Reset(int X, int Y)
        {
            return new Leaf(X, Y);
        }
        public override Block Place(int x, int y)
        {

            return new Leaf(x, y);
        }

        public override Block Mine(int x, int y)
        {

            return new Leaf(x, y);
        }
    }
}
