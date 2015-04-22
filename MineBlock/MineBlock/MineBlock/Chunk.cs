using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineBlock
{
    public class Chunk
    {
        public enum Biome { Stone, StoneGravel, Dirt, Snow, Mycelium, Beach, Spawn };
        Biome biome;
        public int xPos, yPos;
        Block[,] blocks = new Block[20, 20];
        bool Snow = false;

        public static Block getBlockAt(List<Chunk> chunks, int x, int y)
        {
            return getChunk(chunks, x, y).getBlocks()[Math.Abs(x % 20), y % 20];

        }
        public static Chunk getChunk(List<Chunk> chunks, int x, int y)
        {
            foreach (Chunk chunk in chunks)
            {
                if (chunk.xPos == x / 20 && chunk.yPos == y / 20)
                    return chunk;

            }
            Chunk newChunk = Terrain.GenChunk(x / 20, y / 20);
            newChunk.organiseBlocks();
            Game1.save.SaveChunk(newChunk);
            chunks.Add(newChunk);
            if (chunks.Count > 10) { chunks[0].unloadChunk(Game1.save); chunks.RemoveAt(0); }
            return newChunk;
        }
        public static void UpdateBlock(List<Chunk> chunks, int x, int y)
        {
            getChunk(chunks, x, y).getBlocks()[Math.Abs(x % 20), y % 20].update(chunks);
        }
        public static void SetBlock(List<Chunk> chunks, int x, int y, Block block)
        {
           
            getChunk(chunks, x, y).getBlocks()[Math.Abs(x % 20), y % 20] = block;
 
        }
        public Chunk(int chunk, Biome biome, Block[,] blocks)
        {
            xPos = chunk % 10;
            yPos = chunk / 10;
            this.biome = biome;
            this.blocks = blocks;
            if (biome == Biome.Snow) Snow = true;
           

        }
        public Chunk(int x, int y, Biome biome, Block[,] blocks, bool snow)
        {
            xPos = x;
            yPos = y;
            this.biome = biome;
            this.blocks = blocks;
            this.Snow = snow;
            //Game1.save.SaveChunk(this);

        }
        public void unloadChunk(MineBlock.Managers.SaveHandler saves)
        {
            saves.SaveChunk(this);
        }
        public Block[,] getBlocks()
        {
            return blocks;
        }
        public void organiseBlocks()
        {
           
            if(xPos >=0)
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                {
                    blocks[i, j].x = (xPos * 20) + i;
                    blocks[i, j].y = (yPos * 20) + j;

                }
            else
                for (int i = 0; i < 20; i++)
                    for (int j = 0; j < 20; j++)
                    {
                        blocks[i, j].x = (xPos * 20) - i;
                        blocks[i, j].y = (yPos * 20) + j;

                    }
            
            }
        public void updateBlocks(List<Chunk> chunks)
        {
            foreach (Block block in this.blocks)
                block.update(chunks);
        }
        public bool ShouldSnow
        {
            get { return Snow; }
        }
        public Biome getBiome
        {
            get { return biome; }
        }
    }
}
