using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineBlock.Blocks
{
    public class _InformationBlock : Block
    {
        public Boolean isSpawnChunk = false;
        public Boolean ShouldSnow = false;
        public String Biome = "";
        public _InformationBlock(int x, int y)
        {
            if (x != 19 && y != 12)
                Console.WriteLine("Information block placed at " + x + " " + y + " Is this Correct?");
            index = 255;

        }
        public void getBiomefromIndex(int index)
        {
            switch (index)
            {

                case 0: Biome = "Stone"; break;
                case 1: Biome = "Stone/gravel"; break;
                case 2: Biome = "Dirt"; break;
                case 3: Biome = "Snow"; break;
                case 4: Biome = "Mycelium"; break;
                case 5: Biome = "Beach"; break;
                case 6: { Biome = "Spawn"; isSpawnChunk = true; break; }

            }

        }
        public int getindexfromBiome(string index)
        {
            switch (index)
            {

                case "Stone": return 0;
                case "Stone/gravel": return 1;
                case "Dirt": return 2;
                case "Snow": return 3;
                case "Mycelium": return 4;
                case "Beach": return 5;
                case "Spawn": return 6;

            }
            return 0;
        }
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
        {

        }
    }
}
