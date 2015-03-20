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

        public virtual void DrawBlank(SpriteBatch batch)
        {
                batch.Draw(Game1.terrainsheet, new Vector2((x ), (y )), new Rectangle(index * 40, 0, 40, 40), isfucked ? Color.Red : Color.White);
         handleBlankBlockDmg(batch);
        }
        public void handleBlankBlockDmg(SpriteBatch batch)
        {
            if (damage > 0)
            {
                if (damage <= .1f * MineTime) drawdamage = 1;
                else if (damage <= MineTime * .2f) drawdamage = 2;
                else if (damage <= MineTime * .3f) drawdamage = 3;
                else if (damage <= MineTime * .4f) drawdamage = 4;
                else if (damage <= MineTime * .5f) drawdamage = 5;
                else if (damage <= MineTime * .6f) drawdamage = 6;
                else if (damage <= MineTime * .7f) drawdamage = 7;
                else if (damage <= MineTime * .8f) drawdamage = 8;
                else if (damage <= MineTime * .9f) drawdamage = 9;
                else if (damage <= MineTime) drawdamage = 10;
                if (drawdamage > 0)
                {
                    batch.Draw(Game1.terrainsheet, new Vector2((x), (y )), new Rectangle(-40 + (drawdamage * 40), 600, 40, 40), Game1.breakanimcolor);

                }
            }
        }
    }
}
