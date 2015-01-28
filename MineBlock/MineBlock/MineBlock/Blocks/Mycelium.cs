using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace MineBlock.Blocks
{
    class Mycelium : Block
    {
        public Mycelium(int XPos, int yPos)
        {
            x = XPos;
            y = yPos;
            index = 77;
            MineTime = 60;
            preferedTool = new MineBlock.Items.Shovel(0);
        }
     
        public override Block Reset(int X, int Y)
        {
            return new Mycelium(X, Y);
        }
        public override Block Place(int x, int y)
        {

            return new Mycelium(x, y);
        }

        public override Block Mine(int x, int y)
        {

            return new Dirt(x, y);
        }
    }
}
