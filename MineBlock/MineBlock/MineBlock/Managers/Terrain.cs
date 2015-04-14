using MineBlock.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineBlock
{
    public class Terrain
    {
        public static int genheight = 0;
        public static Chunk genStone(int chunk)
        {

            Block[,] blocks = new Block[20, 20];
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                {
                    blocks[i, j] = new Air(i, j);
                }
            int height = Game1.randy.Next(genheight - 1, genheight + 1);
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
                for (int j = height + 1; j < 20; j++)
                {
                    blocks[i, j] = new Stone(i, j);
                    if (Game1.randy.Next(0, 10) == 3)

                        blocks[i, j] = new Lava(i, j);
                }
            if (height != genheight)

                for (int i = 0; i < Math.Abs(genheight - height); i++)
                {
                    blocks[19 - (Math.Abs(genheight - height) + i), height - i] = new CobbleStone(Math.Abs(genheight - height) + i, height - i);
                    blocks[0 + (Math.Abs(genheight - height) - i), height + i] = new CobbleStone(Math.Abs(genheight - height) - i, height + i);
                }
            Console.WriteLine("Succesfully Generated a " + "Stone" + " Chunk at " + chunk);
            return new Chunk(chunk, Chunk.Biome.Stone, blocks);
        }
        public static Chunk genUnderGround(int chunk)
        {

            Block[,] blocks = new Block[20, 20];
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                {
                    blocks[i, j] = new Stone(i, j);
                }
            int height = Game1.randy.Next(3, 20);
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
                for (int j = height + 1; j < 20; j++)
                {
                    blocks[i, j] = new Stone(i, j);
                    if (Game1.randy.Next(0, 5) == 3)
                    {

                        blocks[i, j] = new Gravel(i, j);
                    }
                }
            ///*blocks[19, 12] = new _InformationBlock(19, 12);
            //_InformationBlock Info = (_InformationBlock)blocks[19, 12];
            //Info.Biome = "Stone/gravel";
            //Info.chunk = chunk; */
            Console.WriteLine("Succesfully Generated a " + "Stone with gravel" + " Chunk at " + chunk);
            return new Chunk(chunk, Chunk.Biome.StoneGravel, blocks);
        }
        public static Chunk genStoneWithGravel(int chunk)
        {

            Block[,] blocks = new Block[20, 20];
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                {
                    blocks[i, j] = new Air(i, j);
                }
            int height = Game1.randy.Next(genheight - 1, genheight + 1);
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
                for (int j = height + 1; j < 20; j++)
                {
                    blocks[i, j] = new Stone(i, j);
                    if (Game1.randy.Next(0, 5) == 3)
                    {

                        blocks[i, j] = new Gravel(i, j);
                    }
                }
            /*/*blocks[19, 12] = new _InformationBlock(19, 12);
            _InformationBlock Info = (_InformationBlock)blocks[19, 12];
            Info.Biome = "Stone/gravel";
            Info.chunk = chunk; */
            Console.WriteLine("Succesfully Generated a " + "Stone with gravel" + " Chunk at " + chunk);
            return new Chunk(chunk, Chunk.Biome.StoneGravel, blocks);
        }
        public static Chunk genDirt(int chunk)
        {
            Block[,] blocks = new Block[20, 20];
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                {
                    blocks[i, j] = new Air(i, j);
                }
            int height = Game1.randy.Next(genheight - 1, genheight + 1);
            for (int i = 0; i < 20; i++)
            {
                if (Game1.randy.Next(0, 5) == 3)
                    blocks[i, height] = new SnowyGrass(i, height);
                else if (Game1.randy.Next(0, 20) == 3)
                {
                    blocks[i, height - 1] = new Cake(i, height - 1);
                    blocks[i, height] = new Grass(i, height);
                }
                else
                    blocks[i, height] = new Grass(i, height);
            }
            for (int i = 0; i < 20; i++)
                for (int j = height + 1; j < 20; j++)
                {

                    blocks[i, j] = new Dirt(i, j);
                }

            /*blocks[19, 12] = new _InformationBlock(19, 12);
            _InformationBlock Info = (_InformationBlock)blocks[19, 12];
            Info.Biome = "Dirt";
            Info.chunk = chunk; */
            Game1.mobManager.AddMob(new Mobs.Pig(((chunk % 10) * 20) + 10, ((chunk / 10) * 20) + (height - 1), chunk));
            Game1.mobManager.AddMob(new Mobs.Cow(((chunk % 10) * 20) + 2, ((chunk / 10) * 20) + (height - 1), chunk));
            Game1.mobManager.AddMob(new Mobs.Chicken(((chunk % 10) * 20) + 17, ((chunk / 10) * 20) + (height - 1), chunk));
            genTree(blocks);
            Console.WriteLine("Succesfully Generated a " + "Dirt" + " Chunk at " + chunk);
            return new Chunk(chunk, Chunk.Biome.StoneGravel, blocks);
        }
        public static Chunk genSnow(int chunk)
        {
            Block[,] blocks = new Block[20, 20];
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                {
                    blocks[i, j] = new Air(i, j);
                }
            int height = Game1.randy.Next(genheight - 1, genheight + 1);
            for (int i = 0; i < 20; i++)
            {
                blocks[i, height] = new SnowyGrass(i, height);
            }
            for (int i = 0; i < 20; i++)
                for (int j = height + 1; j < 20; j++)
                {

                    blocks[i, j] = new Dirt(i, j);
                }
            /*blocks[19, 12] = new _InformationBlock(19, 12);
            _InformationBlock Info = (_InformationBlock)blocks[19, 12];
            Info.Biome = "Snow";
            Info.chunk = chunk; 
            Info.ShouldSnow = true;*/
            Console.WriteLine("Succesfully Generated a " + "Snow" + " Chunk at " + chunk);
            return new Chunk(chunk, Chunk.Biome.Snow, blocks);
        }
        public static Chunk genMycelium(int chunk)
        {
            Block[,] blocks = new Block[20, 20];
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                {
                    blocks[i, j] = new Air(i, j);
                }
            int height = Game1.randy.Next(genheight - 1, genheight + 1);
            for (int i = 0; i < 20; i++)
            {
                if (Game1.randy.Next(0, 20) != 3)
                    blocks[i, height] = new Mycelium(i, height);
                else
                {
                    blocks[i, height - 1] = new Pumpkin(i, height - 1);
                    blocks[i, height] = new Mycelium(i, height);
                }
            }
            for (int i = 0; i < 20; i++)
                for (int j = height + 1; j < 20; j++)
                {

                    blocks[i, j] = new Dirt(i, j);
                }
            /*blocks[19, 12] = new _InformationBlock(19, 12);
            _InformationBlock Info = (_InformationBlock)blocks[19, 12];
            Info.Biome = "Mycelium";
            Info.chunk = chunk; */
            Console.WriteLine("Succesfully Generated a " + "Mycelium" + " Chunk at " + chunk);
            return new Chunk(chunk, Chunk.Biome.Mycelium, blocks);
        }
        public static Chunk genBeach(int chunk)
        {
            Block[,] blocks = new Block[20, 20];
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                {
                    blocks[i, j] = new Air(i, j);
                }
            int height = Game1.randy.Next(genheight - 1, genheight + 1);
            for (int i = 0; i < 20; i++)
            {
                blocks[i, height] = new Sand(i, height);
            }
            for (int i = 0; i < 20; i++)
                for (int j = height + 1; j < 20; j++)
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
                for (int j = 0; j < 20; j++)
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
            /*blocks[19, 12] = new _InformationBlock(19, 12);
            _InformationBlock Info = (_InformationBlock)blocks[19, 12];
            Info.Biome = "Beach";
            Info.chunk = chunk; */
            Console.WriteLine("Succesfully Generated a " + "Beach" + " Chunk at " + chunk);
            return new Chunk(chunk, Chunk.Biome.Beach, blocks);
        }
        public static Chunk GenerateSpawnTerrain(MobManager mobManager, PlayerManager player)
        {
            int height = Game1.randy.Next(6, 10);
            int maxheight = 20;
            int maxwidth = 20;
            genheight = height;
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
            /*blocks[19, 12] = new _InformationBlock(19, 12);
            _InformationBlock Info = (_InformationBlock)blocks[19, 12];
            Info.isSpawnChunk = true;
            Info.Biome = "Spawn";
            Info.chunk = 0; */
            mobManager.AddMob(new Mobs.Pig(2, height - 1, 0));
            mobManager.AddMob(new Mobs.Cow(17, height - 1, 0));
            mobManager.AddMob(new Mobs.Chicken(10, height - 1, 0));
            genTree(blocks);
            //chunks.Add(blocks);-
            //currentChunk = blocks;

            return new Chunk(0, Chunk.Biome.Spawn, blocks);
        }
        public static void genTree(Block[,] chunks)
        {
            int treeheight = Game1.randy.Next(genheight - 5, genheight - 1);
            int xcoord = Game1.randy.Next(2, 18);
            for (int i = treeheight; i < genheight; i++) chunks[xcoord, i] = new Wood(xcoord, i);
            chunks[xcoord, treeheight - 1] = new Leaf(xcoord, treeheight - 1);
            chunks[xcoord + 1, treeheight - 1] = new Leaf(xcoord + 1, treeheight - 1);
            chunks[xcoord - 1, treeheight - 1] = new Leaf(xcoord - 1, treeheight - 1);
            chunks[xcoord + 1, treeheight] = new Leaf(xcoord + 1, treeheight);
            chunks[xcoord - 1, treeheight] = new Leaf(xcoord - 1, treeheight);
            chunks[xcoord, treeheight] = new Leaf(xcoord, treeheight);
            chunks[xcoord + 2, treeheight] = new Leaf(xcoord + 2, treeheight);
            chunks[xcoord - 2, treeheight] = new Leaf(xcoord - 2, treeheight);
        }
        public static Chunk[,] genTerrain(int chunkcount)
        {

            genheight = Game1.randy.Next(6, 15);
            Chunk[,] chunks = new Chunk[10, 10];
            for (int p = 0; p < chunkcount; p++)
            {

                if (p < 10)
                {
                    Chunk genned;
                    switch (Game1.randy.Next(0, 5))
                    {
                        case 1:
                            {
                                genned = genStone(p);
                                break;
                            }
                        case 2:
                            {
                                genned = genSnow(p);
                                break;
                            }
                        case 3:
                            {
                                genned = genBeach(p);
                                break;
                            }
                        case 4:
                            {
                                genned = genMycelium(p);
                                break;
                            }
                        default: genned = genDirt(p); break;
                    }
                    int chunkx = p % 10;
                    int chunky = p / 10;

                    chunks[chunkx, chunky] = genned;
                }
                else
                {
                    Chunk genned = genUnderGround(p);
                    int chunkx = p % 10;
                    int chunky = p / 10;

                    chunks[chunkx, chunky] = genned;
                }

            }
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    chunks[i, j].organiseBlocks();

            return chunks;
        }
    }
}
