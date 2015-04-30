using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineBlock.Items
{
    public class Item
    {
        public int Blockindex = -1;
        public int index = -1;
        public int x = 0, y = 0;
        public int Count = 0;
        public bool hasCount = true;
        Block ItemBlock;
        protected Texture2D terrainsheet;
        public static Item ItemFromIndex(int index)
        {
            switch (index)
            {
                case 1: { return new Pick(1); }
                case 2: { return new Shovel(1); }
            }
            return new Item();
        }
      
        public Item() {
            terrainsheet = Tm.getTexture(Tm.Textures.terrainsheet);  
        }
        public Item(Block itemBlock)
        {
            ItemBlock = itemBlock;
            Blockindex = ItemBlock.index;
            int x = ItemBlock.x;
            int y = ItemBlock.y;
            terrainsheet = Tm.getTexture(Tm.Textures.terrainsheet);  
        }

        public Block ReturnBlock()
        {
            return ItemBlock;
        }
        public virtual void DrawMini(SpriteBatch batch, int Xpos, int Ypos)
        {
            if (Blockindex > 15)
            {
                int indexY = Blockindex / 16;
                int indexX = Blockindex % 16;
                batch.Draw(terrainsheet, new Vector2(Xpos, Ypos), new Rectangle(indexX * 40, indexY * 40, 40, 40), Color.White, 0f, Vector2.Zero, 0.77f, SpriteEffects.None, 0f);
            }
            else if (Blockindex > 0)
                batch.Draw(terrainsheet, new Vector2(Xpos, Ypos), new Rectangle(Blockindex * 40, 0, 40, 40), Color.White, 0f, Vector2.Zero, 0.77f, SpriteEffects.None, 0f);
            //else batch.Draw(Game1.Tools, new Vector2(Xpos, Ypos), new Rectangle(upgrade * 40, index *40, 40, 40), Color.White, 0f, Vector2.Zero, 0.77f, SpriteEffects.None, 0f);
        }

        float rotation = 0;
        public virtual void DrawInHand(SpriteBatch batch, int x, int y, Boolean Flip)
        {
            rotation += .05f;
            if (!Flip)
            {
                int X = x + 75; int Y = y + 70;
                if (Blockindex > 15)
                {
                    int indexY = Blockindex / 16;
                    int indexX = Blockindex % 16;
                    if(terrainsheet != null)batch.Draw(terrainsheet, new Vector2(X, Y), new Rectangle(indexX * 40, indexY * 40, 40, 40), Color.White, rotation, new Vector2(20, 20), 0.3f, SpriteEffects.None, 0f);
                }
                else if (Blockindex > 0)
                    batch.Draw(terrainsheet, new Vector2(X, Y), new Rectangle(Blockindex * 40, 0, 40, 40), Color.White, rotation, new Vector2(20,20), 0.3f, SpriteEffects.None, 0f);
                //batch.Draw(Game1.terrainsheet, new Vector2(X, Y), new Rectangle(Blockindex * 40, 0, 40, 40), Color.White, 0f, Vector2.Zero, 0.3f, SpriteEffects.None, 0f);
            }
            else
            {
                int X = x + 30; int Y = y + 70;
                if (Blockindex > 15)
                {
                    int indexY = Blockindex / 16;
                    int indexX = Blockindex % 16;
                    batch.Draw(terrainsheet, new Vector2(X, Y), new Rectangle(indexX * 40, indexY * 40, 40, 40), Color.White, rotation, new Vector2(20, 20), 0.3f, SpriteEffects.None, 0f);
                }
                else if (Blockindex > 0)
                    batch.Draw(terrainsheet, new Vector2(X, Y), new Rectangle(Blockindex * 40, 40, 40, 40), Color.White, rotation, new Vector2(20, 20), 0.3f, SpriteEffects.None, 0f);
            }
        }



    }
}

