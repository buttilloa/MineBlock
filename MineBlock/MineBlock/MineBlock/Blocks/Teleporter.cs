using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace MineBlock.Blocks
{
    class Teleporter : Block
    {
        public Teleporter(int XPos, int yPos)
        {
            x = XPos;
            y = yPos;
            index = 46;
            MineTime = 240;
            preferedTool = new MineBlock.Items.Pick(0);
        }
        public override void update(List<Chunk> chunks)
        {
        
            if(index!=46)
            if(Game1.randy.Next(0,100)==4)
                index = 46;
            base.update(chunks);
        }
        public override void switchTeleporter(Boolean isLava)
        {
            if (isLava) 
                index = 46;
            else
            index = 45;
        }
        public override Block Reset(int X, int Y)
        {
            return new Teleporter(X, Y);
        }
        public override Block Place(int x, int y)
        {

            return new Teleporter(x, y);
        }

        public override Block Mine(int x, int y)
        {

            return new Teleporter(x, y);
        }
    }
}
