using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace MineBlock.Blocks
{
    public class Chest : Block
    {
        public Block[] items = new Block[9];
        public int[] count = new int[9];
        public Chest(int XPos, int yPos) :base()
        {
            x = XPos;
            y = yPos;
            index = 26;
            //canMine = false;
            
            for (int i = 0; i < 9; i++)
            {
                items[i] = new Dirt((x + (i * 20)) + 3, y + 3);
                count[i] = 10;
            }
        }
       
        public override Block Place(int x, int y)
        {

            return new Chest(x, y);
        }

        public override Block Mine(int x, int y)
        {

            return new Chest(x, y);
        }
        public void addToInv(Block newBlock, int BlockCount)
        {
            Boolean isInBar = false;
            for (int i = 0; i < 9; i++)
            {
                if (newBlock.index == items[i].index)
                {
                    count[i]++;
                    isInBar = true;
                    break;
                }
            }
            if (!isInBar)
                for (int i = 0; i < 9; i++)
                {
                    if (items[i].index == 0)
                    {
                        items[i] = newBlock.Reset((i * 40) + 16, 16);
                        count[i] += BlockCount;
                        break;
                    }
                }
        }
        public override Block Reset(int X, int Y)
        {
            return new Chest(X, Y);
        }
    }
}
