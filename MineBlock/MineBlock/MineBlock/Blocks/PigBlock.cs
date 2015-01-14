using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace MineBlock.Blocks
{
    class PigBlock : Block
    {
        public PigBlock(int XPos, int yPos)
        {
            x = XPos;
            y = yPos;
            index = 47;
        }
        
        public override Block Reset(int X, int Y)
        {
          
            return new Air(X, Y);
        }
        public override Block Place(int x, int y)
        {
            Game1.mobManager.AddMob(new Mobs.Pig(x, y, Game1.currentChunkNumber));
            return new Air(x, y);
        }

        public override Block Mine(int x, int y)
        {

            return new PigBlock(x, y);
        }
    }
}
