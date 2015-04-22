using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MineBlock.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineBlock.Items
{
    class Tool : Item
    {
        public int upgrade = -1;
        public int damage = 0;
        public int StartDamage = 0;
        private Texture2D ToolSheet, Blank;
        public Tool()
        {
            Blank = Tm.getTexture(Tm.Texture.Blank);
            ToolSheet = Tm.getTexture(Tm.Texture.Tools);
        }
        public override void DrawMini(SpriteBatch batch, int Xpos, int Ypos)
        {
            batch.Draw(ToolSheet, new Vector2(Xpos, Ypos), new Rectangle(upgrade * 40, (index-1) * 40, 40, 40), Color.White, 0f, Vector2.Zero, 0.77f, SpriteEffects.None, 0f);
            batch.Draw(Blank, new Rectangle(Xpos, Ypos + 25, handleDamage(), 3), damage > StartDamage / 2 ? Color.Green : damage > StartDamage / 4 ? Color.Orange : Color.Red);

        }
        public int handleDamage() // max ==30
        {
            int drawdamage = 0;
             for (float i = 0; i < 10; i += .35f)
            {
                if (damage >= (.1f * i) * StartDamage) drawdamage++;
            }
            return drawdamage;
        }
        public override void DrawInHand(SpriteBatch batch, int x, int y, Boolean Flip)
        {

            if (!Flip)
            {
                int X = x + 67; int Y = y + 55;
                batch.Draw(ToolSheet, new Vector2(X, Y), new Rectangle(upgrade * 40, (index-1)*40, 40, 40), Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);

            }
            else
            {
                int X = x + 15; int Y = y + 55;
                batch.Draw(ToolSheet, new Vector2(X, Y), new Rectangle(upgrade * 40, (index-1)*40, 40, 40), Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.FlipHorizontally, 0f);
            }
        }
    }


    class Pick : Tool
    {

        public Pick(int level)
        {
            index = 1;
            upgrade = level - 1;
            hasCount = false;
            Count = 1;
            StartDamage = 1000 + (upgrade * 100);
            damage = StartDamage;
        }


    }
    class Shovel : Tool
    {

        public Shovel(int level)
        {
            index = 2;
            upgrade = level - 1;
            hasCount = false;
            Count = 1;
            StartDamage = 1000 + (upgrade * 100);
            damage = StartDamage;
        }

    }
}
