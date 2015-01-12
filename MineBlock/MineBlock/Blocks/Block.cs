using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MineBlock.Blocks;
namespace MineBlock
{
    public class Block
    {
        public int index;
        public int x = 0, y = 0;
        public int special;
        // Boolean isFlamible = false;
        public Boolean canMine = true;

        public Block()
        {

        }
        public virtual Block Reset(int x, int y)
        {
            return new Block();
        }
        public virtual Block Place(int x, int y)
        {

            return new Block();
        }
        public virtual Block Mine(int x, int y)
        {

            return new Block();
        }
        public Block returnBlock(int index, int X, int Y)
        {
            switch (index)
            {

                case 0: return new Air(X, Y);
                case 1: return new Stone(X, Y);
                case 2: return new Dirt(X, Y);
                case 3: return new Grass(X, Y);
                case 6: return new BedRock(X, Y);
                case 11: return new Lava(X, Y);
                case 14: return new Water(X, Y);
                case 16: return new CobbleStone(X, Y);
                case 18: return new Sand(X, Y);
                case 19: return new Gravel(X, Y);
                case 26: return new Chest(X, Y);
                case 37: return new Obsidian(X, Y);
                case 46: return new Teleporter(X, Y);
                case 47: return new PigBlock(X, Y);
                case 66: return new Snow(X, Y);
                case 68: return new SnowyGrass(X, Y);
                case 77: return new Mycelium(X, Y);
                case 83: return new Ladder(X, Y);
                case 119: return new Pumpkin(X, Y);
                case 122: return new Cake(X, Y);
                case 158: return new Commandblock(X, Y);
                case 255: return new _InformationBlock(X, Y);


            }
            return new Block();
        }
        public virtual void update(Block[,] blocks)
        {


        }
        public virtual void switchTeleporter(Boolean IsLava)
        {
        }
        
        public virtual Block ConvertGrass(int x, int y, int orientation)
        {
            return new Block();
        }
        public virtual void Draw(SpriteBatch batch)
        {
            if (index > 15)
            {
                int indexY = index / 16;
                int indexX = index % 16;
                batch.Draw(Game1.terrainsheet, new Vector2(x * 40, y * 40), new Rectangle(indexX * 40, indexY * 40, 40, 40), Color.White);
            }
            else
                batch.Draw(Game1.terrainsheet, new Vector2(x * 40, y * 40), new Rectangle(index * 40, 0, 40, 40), Color.White);
        }
        public void DrawMini(SpriteBatch batch)
        {
            if (index > 15)
            {
                int indexY = index / 16;
                int indexX = index % 16;
                batch.Draw(Game1.terrainsheet, new Vector2(x, y), new Rectangle(indexX * 40, indexY * 40, 40, 40), Color.White, 0f, Vector2.Zero, 0.77f, SpriteEffects.None, 0f);
            }
            else
                batch.Draw(Game1.terrainsheet, new Vector2(x, y), new Rectangle(index * 40, 0, 40, 40), Color.White, 0f, Vector2.Zero, 0.77f, SpriteEffects.None, 0f);

        }
        public void DrawInChest(SpriteBatch batch, float xPos, float yPos)
        {
            if (index > 15)
            {
                int indexY = index / 16;
                int indexX = index % 16;
                batch.Draw(Game1.terrainsheet, new Vector2(xPos, yPos), new Rectangle(indexX * 40, indexY * 40, 40, 40), Color.White, 0f, Vector2.Zero, 0.389f, SpriteEffects.None, 0f);
            }
            else
                batch.Draw(Game1.terrainsheet, new Vector2(xPos, yPos), new Rectangle(index * 40, 0, 40, 40), Color.White, 0f, Vector2.Zero, 0.389f, SpriteEffects.None, 0f);

        }

    }
}
