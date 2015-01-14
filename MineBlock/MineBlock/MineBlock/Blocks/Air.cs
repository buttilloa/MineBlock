using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace MineBlock.Blocks
{
    class Air : Block
    {

        public Air(int XPos , int yPos)
        {
            x = XPos;
            y = yPos;
             index = 0;
        }

        public override Block Place(int x, int y)
        {

            return new Air(x,y);
        }

        public override Block Mine(int x, int y)
        {

            return new Air(x,y);
        }
         
    }
}
