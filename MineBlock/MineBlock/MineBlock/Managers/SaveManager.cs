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
            if (container.FileExists("saveData" + "0"))
                return true;
            return false;
        }
        public bool hasSaved(int saveState)
        {
            GetContainer("MineBlock" + saveState);
            if (container.FileExists("saveData" + "0"))
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
            if (container.FileExists("saveData" + chunk))
            {
                //Load number here.
                SaveData = container.OpenFile("saveData" + chunk, FileMode.Open);

            }
            else
            {
                SaveData = container.CreateFile("saveData" + chunk);
            }


            Block[,] blocks = Game1.chunks[chunk];
            SaveData.Position = 0;
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 13; j++)
                {

                    SaveData.WriteByte((byte)blocks[i, j].index);
                    if (blocks[i, j].index == 255)
                    {
                        _InformationBlock Info = (_InformationBlock)blocks[i, j];
                        SaveData.WriteByte((byte)Info.getindexfromBiome(Info.Biome));
                        if (Info.ShouldSnow) SaveData.WriteByte(1);
                        else SaveData.WriteByte(0);
                    }
                    //Console.WriteLine("Wrote: " + (byte)blocks[i, j].index + " in position " + SaveData.Position+" for chunk "+ chunk);

                }
            Console.WriteLine("Chunk " + chunk + " Succesfully saved");
            SaveData.Close();
            SaveData.Dispose();
        }
        public Block[,] LoadChunk(int chunk)
        {
            Stream SaveData = null;
            Block[,] blocks = new Block[20, 13];
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 13; j++)
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
                for (int j = 0; j < 13; j++)
                {
                    blocks[i, j] = blocks[i, j].returnBlock(SaveData.ReadByte(), i, j);
                    //Console.WriteLine("Read: " + (byte)blocks[i, j].index + " in position " + SaveData.Position + " for chunk " + chunk);
                }
            if (blocks[19, 12].index != 255)
            {
                blocks[19, 12] = new _InformationBlock(19, 12);
            }
            _InformationBlock Info = (_InformationBlock)blocks[19, 12];
            Info.getBiomefromIndex((int)SaveData.ReadByte());
            if (SaveData.ReadByte() == 1) Info.ShouldSnow = true;
            else Info.ShouldSnow = false;
            blocks[19, 12] = Info;


            Console.WriteLine("Chunk " + chunk + " Succesfully loaded");
            SaveData.Close();
            SaveData.Dispose();
            return blocks;

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
            SaveData.WriteByte((byte)Game1.currentChunkNumber);
            SaveData.WriteByte((byte)player.Player.Location.X);
            SaveData.WriteByte((byte)player.Player.Location.Y);

            for (int i = 0; i < 9; i++)
                SaveData.WriteByte((byte)player.hotbar[i].index);
            for (int i = 0; i < 9; i++)
                SaveData.WriteByte((byte)player.count[i]);

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
            Game1.switchChunk(player,SaveData.ReadByte());
            player.Player.Location = new Vector2(SaveData.ReadByte(), SaveData.ReadByte());
            player.updateBlocks(Game1.chunks[Game1.currentChunkNumber]);

            for (int i = 0; i < 9; i++)
                player.hotbar[i] = new Block().returnBlock((int)SaveData.ReadByte(), (i * 40) + 16, 16);
            for (int i = 0; i < 9; i++)
                player.count[i] = SaveData.ReadByte();
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

        public List<Block[,]> loadSave(int selectedSave,int currentChunkNumber, List<Block[,]>chunks,PlayerManager player, MobManager mobManager )
        {
#if WINDOWS
            chunks.Clear();
            Console.WriteLine("READING SAVE " + selectedSave);
            GetDevice();
            GetContainer("MineBlock" + selectedSave);
            if (hasSaved())
            {
                //chunks[currentChunkNumber] = LoadChunk(currentChunkNumber);
                //chunks.Add(currentChunk);
                loadTerrainCollum(chunks);
                LoadPlayer(player);
                mobManager.RemoveMobs();
                LoadMobs(mobManager);
            }
            else
            {
                chunks.Add(Terrain.GenerateSpawnTerrain(mobManager, player));
                //currentChunk = chunks[0];
                chunks = Terrain.genTerrainCollum(chunks, 100);
            }
            return chunks;
#endif

        }
        public List<Block[,]>loadTerrainCollum(List<Block[,]> chunks)
        {
            for (int p = 1; p < 101; p++)
            {
                chunks.Add(LoadChunk(p));
            }
            return chunks;
        }
        public void SaveAll(int selectedSave, PlayerManager player, MobManager mobManager)
        {
#if WINDOWS
            GetDevice();
            GetContainer("MineBlock" + selectedSave);
#endif
            for (int i = 0; i < Game1.chunks.Count; i++)
            {
                SaveChunk(i);
            }
            SaveMobs(mobManager);
            SavePlayer(player);
        }
    }

}
