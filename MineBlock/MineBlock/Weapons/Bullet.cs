using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineBlock.Weapons
{
    public class Bullet
    {
        public Sprite shot;
        public Bullet(int x , int y , Boolean direction)
    {
        shot = new Sprite(new Vector2(x, y), Game1.Weather, new Rectangle(0, 0,4, 4), direction ? new Vector2(-150,0):new Vector2(150,0));
       
    }
        public void update(GameTime time)
        {
            shot.Update(time);
        }
      public void Draw(SpriteBatch batch)
        {
            shot.Draw(batch);
        }
    }
}
