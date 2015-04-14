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
        int xPos, yPos;
        Block[,] blocks = new Block[20, 20];
        bool Snow = false;
        public static Block getBlockAt(Chunk[,] chunks,int x, int y)
        {
            x = (int)MathHelper.Clamp(x, 0f, chunks.GetLength(0) * 20f);
            y = (int)MathHelper.Clamp(y, 0f, chunks.GetLength(1) * 20f);
            return chunks[x / 20,y / 20].getBlocks()[x%20, y%20];
        }
        public static Chunk getChunk(Chunk[,] chunks, int x, int y)
        {
            x = (int)MathHelper.Clamp(x, 0f, chunks.GetLength(0) * 20f);
            y = (int)MathHelper.Clamp(y, 0f, chunks.GetLength(1) * 20f);
            return chunks[x / 20, y / 20];
        }
        public static void UpdateBlock(Chunk[,] chunks, int x, int y)
        {
            x = (int)MathHelper.Clamp(x, 0f, chunks.GetLength(0) * 20f);
            y = (int)MathHelper.Clamp(y, 0f, chunks.GetLength(1) * 20f);
            chunks[x / 20,y / 20].getBlocks()[x%20, y%20].update(chunks);
        }
        public static void SetBlock(Chunk[,] chunks, int x, int y,Block block)
        {
            x = (int)MathHelper.Clamp(x, 0f, chunks.GetLength(0) * 20f);
            y = (int)MathHelper.Clamp(y, 0f, chunks.GetLength(1) * 20f); 
            chunks[x / 20, y / 20].getBlocks()[x % 20, y % 20] = block;
        }
        public Chunk(int chunk, Biome biome, Block[,] blocks)
        {
            xPos = chunk % 10;
            yPos = chunk / 10;
            this.biome = biome;
            this.blocks = blocks;
            if (biome == Biome.Snow) Snow = true;
            
        }
        public Block[,] getBlocks()
        {
            return blocks;
        }
        public void organiseBlocks()
        {
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                {
                    blocks[i, j].x = (xPos * 20) + i;
                    blocks[i, j].y = (yPos * 20) + j;
                
                }
        }
        public void updateBlocks(Chunk[,] chunks)
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
