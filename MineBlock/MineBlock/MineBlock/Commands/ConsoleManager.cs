using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineBlock.Commands
{
    public class ConsoleManager
    {
        public bool isShown = false;
        String Command = "";
        String output = "";
        public List<String> history = new List<String>();
        int currentcmd = 0;
        public List<Command> cmds = new List<Command>();
        public Boolean display = false;
        public Boolean outlined = false;
        public Boolean mobCooords = false;
        KeyboardState oldstate;
        private SpriteFont pericles14, pericles1;
        private Texture2D Blur, saveSelectHighlight;
        public ConsoleManager()
        {
            cmds.Add(new Setblock());
            cmds.Add(new DisplayCoords());
            cmds.Add(new BlockOutline());
            cmds.Add(new MobCoords());
            cmds.Add(new Save());
            cmds.Add(new Load());
            cmds.Add(new Exit());
            cmds.Add(new Give());
            cmds.Add(new Goto());
            cmds.Add(new Tp());
            cmds.Add(new GetBlock());
            cmds.Add(new WriteLine());
            cmds.Add(new Write());
            cmds.Add(new Clear());
            cmds.Add(new ToggleDownfall());
            cmds.Add(new AddtoCB());
            cmds.Add(new DeleteSave());
            cmds.Add(new Fuckoff());
            cmds.Add(new TmCalls());
            cmds.Add(new RenderDistance());
            pericles1 = Tm.getFont(Tm.Font.f1);
            pericles14 = Tm.getFont(Tm.Font.f14);
            Blur = Tm.getTexture(Tm.Textures.Blur);
            saveSelectHighlight = Tm.getTexture(Tm.Textures.SaveSelectHighlight);
            
        }
        public void ParseCmd()
        {

            currentcmd++;
            string[] parsed = Command.Split(' ');
            for (int i = 1; i < parsed.Length; i++)
            {
                if (parsed[i][0] == '~')
                {
                    parsed[i] = parsed[i].PadRight(2, '0');
                    parsed[i] = "" + ((Game1.player.Player.Location.X / Constants.BlockSize) + Convert.ToInt32(parsed[i].Substring(1)));
                    i++;
                    if (parsed[i][0] == '~')
                    {
                        parsed[i] = parsed[i].PadRight(2, '0');
                        parsed[i] = "" + ((Game1.player.Player.Location.Y / Constants.BlockSize) + Convert.ToInt32(parsed[i].Substring(1)));
                        break;
                    }
                }
            }
            foreach (Command cmd in cmds)
                if (parsed[0] == cmd.usage.ToUpper())
                {
                    try
                    {
                        output = cmd.Execute(parsed).ToLower();
                    }
                    catch (System.IndexOutOfRangeException)
                    {
                        output = "invalid arguments: " + cmd.Desc;
                    }
                    break;
                }
                else output = "Command not found";
            if (parsed[0] == "HELP")
            {
                foreach (Command cmd in cmds)
                    history.Add("" + cmd.ToString().Substring(19) + " : " + cmd.Desc);
                output = "Displays all commands";
            }
            history.Add(Command + " : " + output);
        }
        public void getKeyStrokes()
        {
            KeyboardState newState = Keyboard.GetState();

            if (newState.GetPressedKeys().Length > 0 && oldstate != null)
            {
                if (!oldstate.IsKeyDown(newState.GetPressedKeys()[0]))
                {
                    Keys key = newState.GetPressedKeys()[0];
                    if (key.ToString() == "OemTilde")
                        Command += "~";
                    else if (key.ToString() == "OemMinus")
                        Command += "-";
                    else if (key.ToString() == "Delete")
                        Command = "";
                    else if (key.ToString() == "Space")
                        Command += " ";
                    else if (key.ToString() == "Up")
                    {
                        if (currentcmd != 0)
                        {
                            currentcmd--;
                            Command = history[currentcmd].Split(' ')[0].ToUpper();
                        }
                    }
                    else if (key.ToString() == "Down")
                    {
                        if (currentcmd != history.Count - 1)
                        {
                            currentcmd++;
                            Command = history[currentcmd].Split(' ')[0].ToUpper();
                        }
                        else Command = "";
                    }
                    else if (key.ToString() == "Back")
                    {
                        if (Command.Length > 0)
                            Command = Command.Substring(0, Command.Length - 1);
                    }
                    else if (key.ToString().Substring(0, 1) == "D" && key.ToString().Length > 1)
                        Command += key.ToString().Substring(1, 1);
                    else if (key.ToString() == "Enter") ParseCmd();
                    else
                        if (IsKeyAChar(key))
                            Command += key;

                }

            }
            oldstate = newState;

        }
        bool IsKeyAChar(Keys key)
        {
            return key >= Keys.A && key <= Keys.Z;
        }
        public void Draw(SpriteBatch batch)
        {
            if (display)
                for (int i = Game1.renderXStart; i <= Game1.renderXEnd; i++)
                    for (int j = Game1.renderYStart; j <= Game1.renderYEnd; j++)
                        batch.DrawString(pericles1, "" + i + "," + j, new Vector2((i * Constants.BlockSize) + 2, (j * Constants.BlockSize) + 2), Color.White);
            if (outlined)
                for (int i = Game1.renderXStart; i <= Game1.renderXEnd; i++)
                    for (int j = Game1.renderYStart; j <= Game1.renderYEnd; j++)
                        batch.Draw(saveSelectHighlight, new Rectangle(i * Constants.BlockSize, j * Constants.BlockSize, 40, 40), Color.Wheat);
            if (mobCooords)
                foreach (Mob mob in Game1.mobManager.mobs)
                {
                    if (mob.CurrentChunk == Game1.currentChunkNumber) batch.DrawString(pericles14, "" + ((mob.getX() * Constants.BlockSize) + mob.subPixel.X) + "," + ((mob.getY() * Constants.BlockSize) + mob.subPixel.Y), new Vector2(mob.getX() * Constants.BlockSize, (mob.getY() * Constants.BlockSize) - 10), Color.Gray);
                    else batch.DrawString(pericles1, "" + mob.ToString().Substring(15) + " " + mob.CurrentChunk + ((mob.getX() * Constants.BlockSize) + mob.subPixel.X) + "," + ((mob.getY() * Constants.BlockSize) + mob.subPixel.Y), new Vector2(mob.getX() * Constants.BlockSize, (mob.getY() * Constants.BlockSize) - 10), Color.Gray);
                }
        }
        public void Drawstatic(SpriteBatch batch)
        {

            if (isShown)
            {
                batch.Draw(Blur, new Rectangle(0, 0, 800, Game1.Instance.Window.ClientBounds.Height), Color.White);
                batch.Draw(Blur, new Rectangle(0, 458, 800, 25), Color.DarkGray);
                batch.DrawString(pericles14, Command + "_", new Vector2(2, 458), Color.Gray);
                String[] temp = history.ToArray();
                Array.Reverse(temp);

                for (int i = 0; i < temp.Length; i++)
                    batch.DrawString(pericles1, temp[i], new Vector2(3, 444 - (i * 13)), Color.DarkGray);
            }
        }
    }
}
