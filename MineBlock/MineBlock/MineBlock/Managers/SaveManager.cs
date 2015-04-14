using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework;
using MineBlock.Blocks;


namespace MineBlock
{
    public class SaveManager
    {
        StorageDevice device;
        StorageContainer container;
        public byte TimesOpened = 1;
        public SaveManager()
        {

        }
        public bool hasSaved()
        {
            if (container.FileExists("saveData"))
                return true;
            return false;
        }
        public bool hasSaved(int saveState)
        {
            GetContainer("MineBlock" + saveState);
            if (container.FileExists("saveData"))
            {
                container.Dispose();
                return true;
            }
            container.Dispose();
            return false;
        }
        public void setDevice(StorageDevice newdevice)
        {
            device = newdevice;
        }
        public void GetDevice()
        {
            //Starts the selection processes.
            IAsyncResult result = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);

            // Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            //Sets the global variable.
            device = StorageDevice.EndShowSelector(result);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();
        }
        public void GetContainer(string DisplayName)
        {
            //Starts the selection processes.
            IAsyncResult result = device.BeginOpenContainer(DisplayName, null, null);

            // Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            //Sets the global variable.
            container = device.EndOpenContainer(result);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();
        } 
        public void LoadFile()
        {
            Stream SaveData = null;
            if (container.FileExists("saveData"))
            {
                //Load number here.
                SaveData = container.OpenFile("saveData", FileMode.Open);

                SaveData.Position = 0;
                TimesOpened = (byte)SaveData.ReadByte();
            }
            else
            {
                SaveData = container.CreateFile("saveData");
            }



            SaveData.Close();
            SaveData.Dispose();
        }
        
        public void SaveChunk(int chunk)
        {
            Stream SaveData = null;
            if (container.FileExists("saveData"+chunk))
            {
                //Load number here.
                SaveData = container.OpenFile("saveData"+chunk, FileMode.Open);

            }
            else
            {
                SaveData = container.CreateFile("saveData"+chunk);
            }
            Block[,] block = new Block[20, 20];
            int chunkx = chunk % 10;
            int chunky = chunk / 10;
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                    block[i, j] = Chunk.getBlockAt(Game1.chunks, i, j);
            SaveData.Position = 0;
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                {

                    SaveData.WriteByte((byte)block[i, j].index);
                    /*if (block[i, j].index == 255)
                    {
                        _InformationBlock Info = (_InformationBlock)block[i, j];
                        SaveData.WriteByte((byte)Info.getindexfromBiome(Info.Biome));
                        if (Info.ShouldSnow) SaveData.WriteByte(1);
                        else SaveData.WriteByte(0);
                    }
                    Console.WriteLine("Wrote: " + (byte)blocks[i, j].index + " in position " + SaveData.Position+" for chunk "+ chunk);
                    */
                }
            Console.WriteLine("Chunk " + chunk + " Succesfully saved");
            SaveData.Close();
            SaveData.Dispose();
        }
        public Block[,] LoadChunk(int chunk)
        {
            Stream SaveData = null;
            Block[,] blocks = new Block[20, 20];
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                {
                    blocks[i, j] = new Air(i, j);
                }
            if (container.FileExists("saveData" + chunk))
            {
                //Load number here.
                SaveData = container.OpenFile("saveData" + chunk, FileMode.Open);

            }
            else
            {
                SaveData = container.CreateFile("saveData" + chunk);
            }
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                {
                    blocks[i, j] = blocks[i, j].returnBlock(SaveData.ReadByte(), i, j);
                    //Console.WriteLine("Read: " + (byte)blocks[i, j].index + " in position " + SaveData.Position + " for chunk " + chunk);
                }
            /*if (blocks[19, 12].index != 255)
            {
                blocks[19, 12] = new _InformationBlock(19, 12);
            }
            _InformationBlock Info = (_InformationBlock)blocks[19, 12];
            Info.getBiomefromIndex((int)SaveData.ReadByte());
            if (SaveData.ReadByte() == 1) Info.ShouldSnow = true;
            else Info.ShouldSnow = false;
            blocks[19, 12] = Info;
            */

            Console.WriteLine("Chunk " + chunk + " Succesfully loaded");
            SaveData.Close();
            SaveData.Dispose();
            return blocks;

        }
        
        public void SaveInoneChunk()
        {
            Stream SaveData = null;
            if (container.FileExists("saveData"))
            {
                //Load number here.
                SaveData = container.OpenFile("saveData", FileMode.Open);

            }
            else
            {
                SaveData = container.CreateFile("saveData");
            }
             SaveData.Position = 0;
            for (int i = 0; i < 200; i++)
                for (int j = 0; j < 200; j++)
                { SaveData.WriteByte((byte)Chunk.getBlockAt(Game1.chunks,i,j).index);
                    /*if (block[i, j].index == 255)
                    {
                        _InformationBlock Info = (_InformationBlock)block[i, j];
                        SaveData.WriteByte((byte)Info.getindexfromBiome(Info.Biome));
                        if (Info.ShouldSnow) SaveData.WriteByte(1);
                        else SaveData.WriteByte(0);
                    }
                    Console.WriteLine("Wrote: " + (byte)blocks[i, j].index + " in position " + SaveData.Position+" for chunk "+ chunk);
                    */
                }
            Console.WriteLine("Chunks Succesfully saved");
            SaveData.Close();
            SaveData.Dispose();
        }
        public Chunk[,] LoadInoneChunk()
        {
            Stream SaveData = null;
            var chunks = Game1.chunks;
            if (container.FileExists("saveData" ))
            {
                //Load number here.
                SaveData = container.OpenFile("saveData" , FileMode.Open);

            }
            else
            {
                SaveData = container.CreateFile("saveData" );
            }
            for (int i = 0; i < 200; i++)
                for (int j = 0; j < 200; j++)
                {
                    Chunk.SetBlock(chunks,i,j, new Block().returnBlock(SaveData.ReadByte(), i, j));
                   //Console.WriteLine("Read: " + (byte)chunk[i, j].index + " in position " + SaveData.Position + " for chunk " + chunk);
                }
            /*if (blocks[19, 12].index != 255)
            {
                blocks[19, 12] = new _InformationBlock(19, 12);
            }
            _InformationBlock Info = (_InformationBlock)blocks[19, 12];
            Info.getBiomefromIndex((int)SaveData.ReadByte());
            if (SaveData.ReadByte() == 1) Info.ShouldSnow = true;
            else Info.ShouldSnow = false;
            blocks[19, 12] = Info;
            */

            Console.WriteLine("Chunks Succesfully loaded");
            SaveData.Close();
            SaveData.Dispose();
            return chunks;

        }
        
        public void SavePlayer(PlayerManager player)
        {
            Stream SaveData = null;
            if (container.FileExists("saveData" + "Player"))
            {
                //Load number here.
                SaveData = container.OpenFile("saveData" + "Player", FileMode.Open);

            }
            else
            {
                SaveData = container.CreateFile("saveData" + "Player");
            }



            SaveData.Position = 0;
            //SaveData.WriteByte((byte)Game1.currentChunkNumber);
            SaveData.WriteByte((byte)player.Player.Location.X);
            SaveData.WriteByte((byte)player.Player.Location.Y);

            for (int i = 0; i < 9; i++)
                SaveData.WriteByte((byte)player.hotbar[i].Blockindex);
            for (int i = 0; i < 9; i++)
                SaveData.WriteByte((byte)player.hotbar[i].Count);

            SaveData.WriteByte((byte)player.Health);

            Console.WriteLine("Player Succesfully saved");
            SaveData.Close();
            SaveData.Dispose();
        }
        public void LoadPlayer(PlayerManager player)
        {
            Stream SaveData = null;
            if (container.FileExists("saveData" + "Player"))
            {
                //Load number here.
                SaveData = container.OpenFile("saveData" + "Player", FileMode.Open);

            }
            else
            {
                SaveData = container.CreateFile("saveData" + "Player");
            }



            SaveData.Position = 0;
            //Game1.switchChunk(player,SaveData.ReadByte());
            player.Player.Location = new Vector2(SaveData.ReadByte(), SaveData.ReadByte());
            //player.updateBlocks(Game1.chunks);

            for (int i = 0; i < 9; i++)
                player.hotbar[i] = new Block().returnBlock((int)SaveData.ReadByte(), (i * 40) + 16, 16).ItemBlock();
            for (int i = 0; i < 9; i++)
                player.hotbar[i].Count = SaveData.ReadByte();
            player.Health = SaveData.ReadByte();
            //SaveData.WriteByte((byte)Game1.currentChunkNumber);


            Console.WriteLine("Player Succesfully Loaded");
            SaveData.Close();
            SaveData.Dispose();
        }
    
        public void SaveMobs(MobManager mobs)
        {
            Stream SaveData = null;
            if (container.FileExists("saveData" + "Mobs"))
            {
                //Load number here.
                SaveData = container.OpenFile("saveData" + "Mobs", FileMode.Open);

            }
            else
            {
                SaveData = container.CreateFile("saveData" + "Mobs");
            }



            SaveData.Position = 0;
            SaveData.WriteByte((byte)mobs.mobs.Count);
            foreach (Mob mob in mobs.mobs)
            {
                SaveData.WriteByte((byte)mob.name);
                SaveData.WriteByte((byte)mob.Position.X);
                SaveData.WriteByte((byte)mob.Position.Y);
                SaveData.WriteByte((byte)mob.CurrentChunk);
                SaveData.WriteByte((byte)mob.subPixel.X);
                SaveData.WriteByte((byte)mob.subPixel.Y);
            }




            Console.WriteLine("Mobs Succesfully saved");
            SaveData.Close();
            SaveData.Dispose();
        }
        public void LoadMobs(MobManager mobs)
        {
            Stream SaveData = null;
            if (container.FileExists("saveData" + "Mobs"))
            {
                //Load number here.
                SaveData = container.OpenFile("saveData" + "Mobs", FileMode.Open);

            }
            else
            {
                SaveData = container.CreateFile("saveData" + "Mobs");
            }



            SaveData.Position = 0;
            mobs.RemoveMobs();
            int count = SaveData.ReadByte();
            for (int i = 0; i < count; i++)
            {
                Mob mob = new Mob();
                int index = SaveData.ReadByte();
                int x = SaveData.ReadByte();
                int y = SaveData.ReadByte();
                int chunk = SaveData.ReadByte();
                Vector2 subpixle = new Vector2(SaveData.ReadByte(), SaveData.ReadByte());
                mob = mob.returnMob(index, x, y, chunk);
                mob.subPixel = subpixle;
                Console.WriteLine("ADDED Mob" + index + " " + x + " " + y + " " + chunk);
                mobs.AddMob(mob);
            }


            //SaveData.WriteByte((byte)Game1.currentChunkNumber);


            Console.WriteLine("Mobs Succesfully Loaded");
            SaveData.Close();
            SaveData.Dispose();
        }

        public Chunk[,] loadSave(int selectedSave,int currentChunkNumber,PlayerManager player, MobManager mobManager )
        {
#if WINDOWS
            
            Console.WriteLine("READING SAVE " + selectedSave);
            GetDevice();
            GetContainer("MineBlock" + selectedSave);
            if (hasSaved())
            {
                //chunks[currentChunkNumber] = LoadChunk(currentChunkNumber);
                //chunks.Add(currentChunk);
                Game1.chunks = LoadInoneChunk();
                //chunks = loadTerrainCollum(chunks);
                LoadPlayer(player);
                mobManager.RemoveMobs();
                LoadMobs(mobManager);
            }
            else
            {
              //Chunk genned = Terrain.GenerateSpawnTerrain(mobManager, player);
                //int chunkx = p % 10;
                //int chunky = p / 10;
               // for (int i = 0; i < 20; i++)
               //     for (int j = 0; j < 20; j++)
               //         Chunk.PlaceBlock(chunks,i, j ,genned.getBlocks()[i, j]);
                //currentChunk = chunks[0];
                
            }
            return Terrain.genTerrain(100);
#endif

        }
        public Block[,] loadTerrainCollum(Block[,] chunks)
        {
            for (int p = 1; p < 100; p++)
            {
                Block[,] loaded = LoadChunk(p);
                int chunkx = p % 10;
                int chunky = p / 10; 
                for (int i = 0; i < 20; i++)
                    for (int j = 0; j < 20; j++)
                        chunks[i + (20 * chunkx), j + (20 * chunky)] = loaded[i, j];
                for (int i = 0; i < 200; i++)
                    for (int j = 0; j < 200; j++)
                        if (chunks[i, j] == null)
                            chunks[i, j] = new Air(i, j);
                //chunks.Add(LoadChunk(p));
            }
            return chunks;
        }
        public void SaveAll(int selectedSave, PlayerManager player, MobManager mobManager)
        {
#if WINDOWS
            GetDevice();
            GetContainer("MineBlock" + selectedSave);
#endif
           /*for (int i = 0; i < 100; i++)
            {
                SaveChunk(i);
           }
            */
            SaveInoneChunk();
            SaveMobs(mobManager);
            SavePlayer(player);
        }
    }

}
