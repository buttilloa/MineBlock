using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MineBlock.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineBlock.Managers
{
    public class Inventory
    {
        public bool isdisplayed = false;
        Block[,] slots = new Block[9,3];
        int[,] count = new int[9,3];
        Block holding = new Block();
        int holdcount = 0;
        Texture2D hotboarSheet;
        Boolean canclick = true;
        public Inventory(Texture2D hotbarsheet)
        {
            hotboarSheet = hotbarsheet;
            for (int j = 0; j < 3; j++)
                for (int i = 0; i < 9; i++)
                {
                    slots[i, j] = new Blocks.Ladder((i * 40) + 16, ((j+1) * 42) + 16);
                    count[i, j] = Game1.randy.Next(1,100);
                }
        }

        public void displayinv()
        {
            if (!isdisplayed) isdisplayed = true;

        }
        public void addToInv(Block newBlock, int BlockCount)
        {
            Boolean isInBar = false;
            for (int j = 0; j < 3; j++)
                for (int i = 0; i < 9; i++)
                    if (newBlock.index == slots[i, j].index)
                    {
                        count[i, j]++;
                        isInBar = true;
                        break;
                    }

            if (!isInBar)
                for (int j = 0; j < 3; j++)
                    for (int i = 0; i < 9; i++)
                        if (slots[i, j].index == 0)
                        {
                            slots[i, j] = newBlock.Reset((i * 40) + 16, ((j+1) * 42)+16);
                            count[i, j] += BlockCount;
                            break;
                        }
        }
        public void handlemovement()
        {
            MouseState ms = Mouse.GetState();
            if (isdisplayed)
                for (int j = 0; j < 3; j++)
                    for (int i = 0; i < 9; i++)
                        if (Vector2.Distance(new Vector2(ms.X,ms.Y), new Vector2(slots[i, j].x + 15, slots[i, j].y + 15)) <= 8)
                        {
                            if (slots[i, j].index > 0 && HandleInputs.LeftTrigger() && canclick)
                            {
                                canclick = false;
                                holding = slots[i, j];
                                holdcount = count[i, j];
                                slots[i, j] = new Air((i * 40) + 16, ((j + 1) * 42) + 16);
                                count[i, j] = 0;
                            }
                        }
            if (ms.LeftButton == ButtonState.Released&&!canclick)canclick = true;
            if (holding.index > 0)
            {
                Vector2 pos = HandleInputs.getMousepos();
                holding.x = (int)pos.X; holding.y = (int)pos.Y;
                if (HandleInputs.LeftTrigger()&&canclick )
                {
                    canclick = false;
                    Vector2 closest = getClosestSpot(ms);
                    Mouse.SetPosition((int)closest.X, (int)closest.Y);
                    /* closest = new Vector2(closest.X, closest.Y + 1);
                    Block temp = slots[(int)closest.X, (int)closest.Y].Reset((int)closest.X, (int)closest.Y);
                    int tempcount = count[(int)closest.X, (int)closest.Y];
                    slots[(int)closest.X, (int)closest.Y] = holding.Reset(((int)closest.X * 40) + 16, (((int)closest.Y * +1) * 42) + 16);
                    count[(int)closest.X, (int)closest.Y] = holdcount;
                    if (slots[(int)closest.X, (int)closest.Y].index > 0)
                    {
                        holding = temp;
                        holdcount = tempcount;
                    }
                    else { holding = new Block(); holdcount = 0; }
               */
                  }
            }
           
        
        }
        public Vector2 getClosestSpot(MouseState ms)
        {
            float distance = 100;// Vector2.Distance(new Vector2(ms.X, ms.Y), new Vector2(slots[0, 0].x + 15, slots[0, 0].y + 15)); ;
            Vector2 slot = new Vector2(0,0);
            for (int j = 0; j < 3; j++)
                for (int i = 0; i < 9; i++)
            if (Vector2.Distance(new Vector2(ms.X, ms.Y), new Vector2(slots[i, j].x + 15, slots[i, j].y + 15)) < distance)
            {
                distance = Vector2.Distance(new Vector2(ms.X, ms.Y), new Vector2(slots[i, j].x + 15, slots[i, j].y + 15));
                slot = new Vector2(i,j);
            }
            return slot;
        }
        public void draw(SpriteBatch batch)
        {
            if (isdisplayed)
            {
                for (int j = 0; j < 3; j++)
                {
                    batch.Draw(hotboarSheet, new Rectangle(10, ((j + 1) * 42) + 10, 362, 42), Color.White);
                    for (int i = 0; i < 9; i++)
                    {
                        //if (count[i] == 0) hotbar[i] = new Air((i * 40) + 16, 16);
                        if (count[i, j] > 0)
                        {
                            slots[i, j].DrawMini(batch);
                            batch.DrawString(Game1.pericles14, "" + count[i, j], new Vector2(slots[i, j].x + 5, slots[i, j].y + 3), Color.White);
                        }
                    }
                }
                if (holdcount > 0)
                {
                    holding.DrawMini(batch);
                    batch.DrawString(Game1.pericles14, "" + holdcount, new Vector2(holding.x + 5, holding.y + 3), Color.White);

                }
            }
        }
    }
}
