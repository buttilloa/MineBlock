using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineBlock
{
    class Texture
    {
        Texture2D texture;
        string name;
        public Texture()
        {

        }
        public Texture(String name, Texture2D text)
        {
            texture = text;
            this.name = name;
        }
        public void assignTexture(Texture2D text, String name)
        {
            texture = text;
            this.name = name;
        }
        public Texture2D getTexture()
        {
            return texture;
        }
        public string getName()
        {
            return name;
        }
    }
}
