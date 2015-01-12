using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace MineBlock.Blocks
{
    class Snow : Block
    {
        public Snow(int XPos, int yPos)
        {
            x = XPos;
            y = yPos;
            index = 66;
          
        }
     
        public override Block Reset(int X, int Y)
        {
            return new Snow(X, Y);
        }
        public override Block Place(int x, int y)
        {

            return new Snow(x, y);
        }

        public override Block Mine(int x, int y)
        {

            return new Snow(x, y);
        }
    }
}
