using MineBlock.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineBlock
{
    public class Terrain
    {

        public static Block[,] genStone(Block[,] blocks, int chunk)
        {

            blocks = new Block[20, 13];
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 13; j++)
                {
                    blocks[i, j] = new Air(i, j);
                }
            int height = Game1.randy.Next(5, 13);
            for (int i = 0; i < 20; i++)
            {
                blocks[i, height] = new Stone(i, height);
                switch (Game1.randy.Next(0, 5))
                {
                    case 1:
                        blocks[i, height - 1] = new Stone(i, height - 1);
                        break;
                    case 2:
                        {
                            blocks[i, height - 2] = new Stone(i, height - 2);
                            blocks[i, height - 1] = new Stone(i, height - 1);
                            break;
                        }
                    case 3:
                        {
                            blocks[i, height - 2] = new Stone(i, height - 2);
                            blocks[i, height - 1] = new Stone(i, height - 1);
                            try
                            {
                                if (Game1.randy.Next(0, 2) == 1) blocks[i + 1, height - 1] = new Stone(i + 1, height - 1);
                                else blocks[i - 1, height - 1] = new Stone(i - 1, height - 1);
                            }
                            catch (System.IndexOutOfRangeException) { Console.WriteLine("Stone Gen had an error...Skipping..."); }
                            break;
                        }
                    case 4:
                        {
                            blocks[i, height - 2] = new Stone(i, height - 2);
                            blocks[i, height - 1] = new Stone(i, height - 1);
                            blocks[i, height - 3] = new Stone(i, height - 3);
                            break;
                        }


                }
                blocks[i, height - 1] = new Stone(i, height - 1);
            }
            for (int i = 0; i < 20; i++)
                for (int j = height + 1; j < 13; j++)
                {
                    blocks[i, j] = new Stone(i, j);
                    if (Game1.randy.Next(0, 10) == 3)

                        blocks[i, j] = new Lava(i, j);
                }
            blocks[19, 12] = new _InformationBlock(19, 12);
            _InformationBlock Info = (_InformationBlock)blocks[19, 12];
            Info.Biome = "Stone";
            Console.WriteLine("Succesfully Generated a " + "Stone" + " Chunk at " + chunk);
            return blocks;
        }
        public static Block[,] genUnderGround(Block[,] blocks, int chunk)
        {

            blocks = new Block[25, 13];
            for (int i = 0; i < 25; i++)
                for (int j = 0; j < 13; j++)
                {
                    blocks[i, j] = new Stone(i, j);
                }
            int height = Game1.randy.Next(3, 13);
            for (int i = 0; i < 20; i++)
            {
                blocks[i, height] = new Stone(i, height);
                switch (Game1.randy.Next(0, 5))
                {
                    case 1:
                        blocks[i, height - 1] = new Stone(i, height - 1);
                        break;
                    case 2:
                        {
                            blocks[i, height - 2] = new Stone(i, height - 2);
                            blocks[i, height - 1] = new Stone(i, height - 1);
                            break;
                        }
                    case 3:
                        {
                            blocks[i, height - 2] = new Stone(i, height - 2);
                            blocks[i, height - 1] = new Stone(i, height - 1);
                            try
                            {
                                if (Game1.randy.Next(0, 2) == 1) blocks[i + 1, height - 1] = new Stone(i + 1, height - 1);
                                else blocks[i - 1, height - 1] = new Stone(i - 1, height - 1);
                            }
                            catch (System.IndexOutOfRangeException) { Console.WriteLine("Stone Gen with Gravel Had an error...Skipping..."); }
                            break;
                        }
                    case 4:
                        {
                            blocks[i, height - 2] = new Stone(i, height - 2);
                            blocks[i, height - 1] = new Stone(i, height - 1);
                            blocks[i, height - 3] = new Stone(i, height - 3);
                            break;
                        }


                }
                blocks[i, height - 1] = new Stone(i, height - 1);
            }
            for (int i = 0; i < 20; i++)
                for (int j = height + 1; j < 13; j++)
                {
                    blocks[i, j] = new Stone(i, j);
                    if (Game1.randy.Next(0, 5) == 3)
                    {

                        blocks[i, j] = new Gravel(i, j);
                    }
                }
            blocks[19, 12] = new _InformationBlock(19, 12);
            blocks[9, 0] = new Air(9, 0);
            blocks[10, 0] = new Air(10, 0);
            blocks[11, 0] = new Air(11, 0);
            blocks[9, 1] = new Air(9, 1);
            blocks[10, 1] = new Air(10, 1);
            blocks[11, 1] = new Air(11, 1);
            _InformationBlock Info = (_InformationBlock)blocks[19, 12];
            Info.Biome = "Stone/gravel";
            Console.WriteLine("Succesfully Generated a " + "Stone with gravel" + " Chunk at " + chunk);
            return blocks;
        }
        public static Block[,] genStoneWithGravel(Block[,] blocks, int chunk)
        {

            blocks = new Block[20, 13];
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 13; j++)
                {
                    blocks[i, j] = new Air(i, j);
                }
            int height = Game1.randy.Next(3, 13);
            for (int i = 0; i < 20; i++)
            {
                blocks[i, height] = new Stone(i, height);
                switch (Game1.randy.Next(0, 5))
                {
                    case 1:
                        blocks[i, height - 1] = new Stone(i, height - 1);
                        break;
                    case 2:
                        {
                            blocks[i, height - 2] = new Stone(i, height - 2);
                            blocks[i, height - 1] = new Stone(i, height - 1);
                            break;
                        }
                    case 3:
                        {
                            blocks[i, height - 2] = new Stone(i, height - 2);
                            blocks[i, height - 1] = new Stone(i, height - 1);
                            try
                            {
                                if (Game1.randy.Next(0, 2) == 1) blocks[i + 1, height - 1] = new Stone(i + 1, height - 1);
                                else blocks[i - 1, height - 1] = new Stone(i - 1, height - 1);
                            }
                            catch (System.IndexOutOfRangeException) { Console.WriteLine("Stone Gen with Gravel Had an error...Skipping..."); }
                            break;
                        }
                    case 4:
                        {
                            blocks[i, height - 2] = new Stone(i, height - 2);
                            blocks[i, height - 1] = new Stone(i, height - 1);
                            blocks[i, height - 3] = new Stone(i, height - 3);
                            break;
                        }


                }
                blocks[i, height - 1] = new Stone(i, height - 1);
            }
            for (int i = 0; i < 20; i++)
                for (int j = height + 1; j < 13; j++)
                {
                    blocks[i, j] = new Stone(i, j);
                    if (Game1.randy.Next(0, 5) == 3)
                    {

                        blocks[i, j] = new Gravel(i, j);
                    }
                }
            blocks[19, 12] = new _InformationBlock(19, 12);
            _InformationBlock Info = (_InformationBlock)blocks[19, 12];
            Info.Biome = "Stone/gravel";
            Console.WriteLine("Succesfully Generated a " + "Stone with gravel" + " Chunk at " + chunk);
            return blocks;
        }
        public static Block[,] genDirt(Block[,] blocks, int chunk)
        {
            blocks = new Block[20, 13];
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 13; j++)
                {
                    blocks[i, j] = new Air(i, j);
                }
            int height = Game1.randy.Next(3, 13);
            for (int i = 0; i < 20; i++)
            {
                if (Game1.randy.Next(0, 5) == 3)
                    blocks[i, height] = new SnowyGrass(i, height);
                else if (Game1.randy.Next(0, 13) == 3)
                {
                    blocks[i, height - 1] = new Cake(i, height - 1);
                    blocks[i, height] = new Grass(i, height);
                }
                else
                    blocks[i, height] = new Grass(i, height);
            }
            for (int i = 0; i < 20; i++)
                for (int j = height + 1; j < 13; j++)
                {

                    blocks[i, j] = new Dirt(i, j);
                }
            blocks[19, 12] = new _InformationBlock(19, 12);
            _InformationBlock Info = (_InformationBlock)blocks[19, 12];
            Info.Biome = "Dirt";
            Game1.mobManager.AddMob(new Mobs.Pig(10, (height - 1), chunk));
            Game1.mobManager.AddMob(new Mobs.Cow(2, (height - 1), chunk));
            Game1.mobManager.AddMob(new Mobs.Chicken(17, (height - 1), chunk));
            Console.WriteLine("Succesfully Generated a " + "Dirt" + " Chunk at " + chunk);
            return blocks;
        }
        public static Block[,] genSnow(Block[,] blocks, int chunk)
        {
            blocks = new Block[20, 13];
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 13; j++)
                {
                    blocks[i, j] = new Air(i, j);
                }
            int height = Game1.randy.Next(3, 13);
            for (int i = 0; i < 20; i++)
            {
                blocks[i, height] = new SnowyGrass(i, height);
            }
            for (int i = 0; i < 20; i++)
                for (int j = height + 1; j < 13; j++)
                {

                    blocks[i, j] = new Dirt(i, j);
                }
            blocks[19, 12] = new _InformationBlock(19, 12);
            _InformationBlock Info = (_InformationBlock)blocks[19, 12];
            Info.Biome = "Snow";
            Info.ShouldSnow = true;
            Console.WriteLine("Succesfully Generated a " + "Snow" + " Chunk at " + chunk);
            return blocks;
        }
        public static Block[,] genMycelium(Block[,] blocks, int chunk)
        {
            blocks = new Block[20, 13];
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 13; j++)
                {
                    blocks[i, j] = new Air(i, j);
                }
            int height = Game1.randy.Next(3, 13);
            for (int i = 0; i < 20; i++)
            {
                if (Game1.randy.Next(0, 13) != 3)
                    blocks[i, height] = new Mycelium(i, height);
                else
                {
                    blocks[i, height - 1] = new Pumpkin(i, height - 1);
                    blocks[i, height] = new Mycelium(i, height);
                }
            }
            for (int i = 0; i < 20; i++)
                for (int j = height + 1; j < 13; j++)
                {

                    blocks[i, j] = new Dirt(i, j);
                }
            blocks[19, 12] = new _InformationBlock(19, 12);
            _InformationBlock Info = (_InformationBlock)blocks[19, 12];
            Info.Biome = "Mycelium";
            Console.WriteLine("Succesfully Generated a " + "Mycelium" + " Chunk at " + chunk);
            return blocks;
        }
        public static Block[,] genBeach(Block[,] blocks, int chunk)
        {
            blocks = new Block[20, 13];
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 13; j++)
                {
                    blocks[i, j] = new Air(i, j);
                }
            int height = Game1.randy.Next(3, 13);
            for (int i = 0; i < 20; i++)
            {
                blocks[i, height] = new Sand(i, height);
            }
            for (int i = 0; i < 20; i++)
                for (int j = height + 1; j < 13; j++)
                {

                    blocks[i, j] = new Sand(i, j);
                    switch (Game1.randy.Next(0, 5))
                    {
                        case 1:
                            blocks[i, height - 1] = new Sand(i, height - 1);
                            break;

                        case 2:
                            {
                                blocks[i, height - 1] = new Sand(i, height - 1);
                                try
                                {
                                    if (Game1.randy.Next(0, 2) == 1) blocks[i + 1, height - 1] = new Sand(i + 1, height - 1);
                                    else blocks[i - 1, height - 1] = new Sand(i - 1, height - 1);
                                }
                                catch (System.IndexOutOfRangeException) { }
                                break;
                            }
                        case 3:
                            {
                                blocks[i, height] = new Air(i, height);

                                break;
                            }


                    }
                }
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 13; j++)
                {
                    if (blocks[i, j].index == 18)
                    {
                        for (int p = i; p < 20; p++)
                            if (blocks[p, j].index != 18)
                            {

                                blocks[p, j] = new Water(p, j);
                            }
                        break;
                    }

                }
            blocks[19, 12] = new _InformationBlock(19, 12);
            _InformationBlock Info = (_InformationBlock)blocks[19, 12];
            Info.Biome = "Beach";
            Console.WriteLine("Succesfully Generated a " + "Beach" + " Chunk at " + chunk);
            return blocks;
        }
        public static Block[,] GenerateSpawnTerrain(MobManager mobManager, PlayerManager player)
        {
            int height = Game1.randy.Next(6, 10);
            int maxheight = 13;
            int maxwidth = 20;
            Block[,] blocks = new Block[maxwidth, maxheight];
            for (int i = 0; i < maxwidth; i++)
                for (int j = 0; j < maxheight; j++)
                {
                    blocks[i, j] = new Air(i, j);
                }
            for (int i = 0; i < maxwidth; i++)
                if (Game1.randy.Next(0, 5) == 3)
                    blocks[i, height] = new SnowyGrass(i, height);
                else
                    blocks[i, height] = new Grass(i, height);

            for (int i = 0; i < maxwidth; i++)
                for (int j = height + 1; j < maxheight; j++)
                {
                    blocks[i, j] = new Dirt(i, j);
                }

            blocks[10, 10] = new Teleporter(10, 10);
            blocks[19, 12] = new _InformationBlock(19, 12);
            _InformationBlock Info = (_InformationBlock)blocks[19, 12];
            Info.isSpawnChunk = true;
            Info.Biome = "Spawn";
            mobManager.AddMob(new Mobs.Pig(2, height - 1, 0));
            mobManager.AddMob(new Mobs.Cow(17, height - 1, 0));
            mobManager.AddMob(new Mobs.Chicken(10, height - 1, 0));
            //chunks.Add(blocks);
            //currentChunk = blocks;
            player.updateBlocks(blocks);
            return blocks;
        }
        public static List<Block[,]> genTerrainCollum(List<Block[,]> chunks,int chunkcount)
        {
          
            Block[,] blocks = new Block[20, 13];
            for (int p = 1; p < chunkcount; p++)
            {

                if (p < 10)
                {
                    switch (Game1.randy.Next(0, 5))
                    {
                        case 1:
                            {
                                chunks.Add(genStone(blocks, p));
                                break;
                            }
                        case 2:
                            {
                                chunks.Add(genSnow(blocks, p));
                                break;
                            }
                        case 3:
                            {
                                chunks.Add(genBeach(blocks, p));
                                break;
                            }
                        case 4:
                            {
                                chunks.Add(genMycelium(blocks, p));
                                break;
                            }
                        default: chunks.Add(genDirt(blocks, p)); break;
                    }
                }
                else
                    chunks.Add(Terrain.genUnderGround(blocks, p));

            }

            return chunks;
        }
    }
}
