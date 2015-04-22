using Microsoft.Xna.Framework;
using MineBlock.Items;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;

namespace MineBlock.Managers
{
    public class SaveHandler
    {
        const string dir = @"C:\Users\Anthony\Documents\SavedGames\MineBlock\Saves\";
        string userAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);  
        public SaveHandler()
        {
           
        }
        public String CalcFileName(int saveSlot,int x, int y)
        {
            String nDir = dir + saveSlot + "\\" + x + "-" + y + ".dat";
            if (!Directory.Exists(dir + saveSlot))
            Directory.CreateDirectory(dir+saveSlot);
               //DirectorySecurity Sec = di.GetAccessControl();
               //string User = System.Environment.UserName;// + "\\" +
                                                             
               //Sec.AddAccessRule(new FileSystemAccessRule(User,
                  //                               FileSystemRights.FullControl, AccessControlType.Deny));
               //di.SetAccessControl(Sec);
            
                return  nDir;

        }
        public bool hasSaved(int saveSlot)
        {
            return Directory.Exists(dir + saveSlot);
}
        public void SaveChunk(Chunk chunk)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(CalcFileName(Game1.selectedSave,chunk.xPos,chunk.yPos), FileMode.Create)))
            {
                writer.Write(chunk.xPos);
                writer.Write(chunk.yPos);
                writer.Write((int)chunk.getBiome);
                writer.Write(chunk.ShouldSnow);
                for (int i = 0; i < 20; i++)
                    for (int j = 0; j < 20; j++)
                    {
                        writer.Write(chunk.getBlocks()[i, j].index);
                        writer.Write(chunk.getBlocks()[i, j].x);
                        writer.Write(chunk.getBlocks()[i, j].y);
                    }
             
                writer.Dispose();
                writer.Close();
            }
        }
        public PlayerManager LoadPlayer()
        {
            PlayerManager player = new PlayerManager();
            if (File.Exists(dir + Game1.selectedSave + "\\Player.dat"))
            using (BinaryReader reader = new BinaryReader(File.Open(dir + Game1.selectedSave + "\\Player.dat", FileMode.Open)))
            {
                int x = reader.ReadInt32();
                int y = reader.ReadInt32();
                player.Player.Location = new Vector2(x, y);
                player.hotbar = new MineBlock.Items.Item[9];
                for (int i = 0; i < 9; i++)
                {
                    int index = reader.ReadInt32();
                    if (index > 0)
                        player.hotbar[i] = new Block().returnBlock(index, (i * 40) + 16, 16).ItemBlock();
                    else
                        player.hotbar[i] = Item.ItemFromIndex(0 - index);
                    player.hotbar[i].Count = reader.ReadInt32();
                }
               
            }
           return player;
        }
        public void SavePlayer()
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(dir+Game1.selectedSave+"\\Player.dat", FileMode.Create)))
            {
                writer.Write((int)Game1.player.Player.Location.X);
                writer.Write((int)Game1.player.Player.Location.Y);
                for(int i =0; i < 9;i++)
                {
                    if (Game1.player.hotbar[i].Blockindex == -1)
                        writer.Write(0 - Game1.player.hotbar[i].index);
                    else
                    writer.Write(Game1.player.hotbar[i].Blockindex);
                    writer.Write(Game1.player.hotbar[i].Count);
                }
                writer.Dispose();
                writer.Close();
            }
        }
        public void saveAll(List<Chunk> chunks)
        {
            foreach (Chunk chunk in chunks)
                SaveChunk(chunk);
            SavePlayer();

        }
        public Chunk LoadChunk(int x, int y)
        {
            if (File.Exists(CalcFileName(Game1.selectedSave,x,y)))
              using (BinaryReader reader = new BinaryReader(File.Open(CalcFileName(Game1.selectedSave,x,y), FileMode.Open)))
                {
                    int chunkX = reader.ReadInt32();
                    int chunkY = reader.ReadInt32();
                    int biome = reader.ReadInt32();
                    bool Snow = reader.ReadBoolean();
                    Block[,] blocks = new Block[20, 20];
                    for (int i = 0; i < 20; i++)
                        for (int j = 0; j < 20; j++)
                        {
                            int index = reader.ReadInt32();
                            int xpos = reader.ReadInt32();
                            int ypos = reader.ReadInt32();
                            blocks[i, j] = new Block().returnBlock(index, xpos, xpos);

                        }
                    reader.Dispose();
                    reader.Close();
                    return new Chunk(chunkX, chunkY, (Chunk.Biome)biome, blocks, Snow);

                }

            return null;
        }
    }
}
