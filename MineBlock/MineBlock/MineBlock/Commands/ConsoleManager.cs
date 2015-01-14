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
        public bool isShown = true;
        String Command = "";
        String output = "";
        public List<String> history = new List<String>();
        int currentcmd = 0;
        public List<Command> cmds = new List<Command>();
        public Boolean display = false;
        public Boolean outlined = false;
        public Boolean mobCooords = false;
        KeyboardState oldstate;
        public ConsoleManager()
        {
            cmds.Add(new Setblock());
            cmds.Add(new DisplayCoords());
            cmds.Add(new ShowOutline());
            cmds.Add(new ShowMob());
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
        }
        public void ParseCmd()
        {

            currentcmd++;
            string[] parsed = Command.Split(' ');
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

            if (newState.GetPressedKeys().Length > 0)
            {
                if (!oldstate.IsKeyDown(newState.GetPressedKeys()[0]))
                {
                    Keys key = newState.GetPressedKeys()[0];

                    if (key.ToString() == "Escape")
                        isShown = false;
                    else if (key.ToString() == "Space")
                        Command += " ";
                    else if (key.ToString() == "Up")
                    {
                        if (currentcmd != 0)
                        {
                            currentcmd--;
                            Command = history[currentcmd];
                        }
                    }
                    else if (key.ToString() == "Down")
                    {
                        if (currentcmd != history.Count - 1)
                        {
                            currentcmd++;
                            Command = history[currentcmd];
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
                for (int i = 0; i < 20; i++)
                    for (int j = 0; j < 13; j++)
                        batch.DrawString(Game1.pericles1, "" + i + "," + j, new Vector2((i * 40) + 2, (j * 40) + 2), Color.White);
            if (outlined)
                for (int i = 0; i < 20; i++)
                    for (int j = 0; j < 13; j++)
                        batch.Draw(Game1.SaveSelectHighlight, new Rectangle(i * 40, j * 40, 40, 40), Color.Wheat);
            if (mobCooords)
                foreach (Mob mob in Game1.mobManager.mobs)
                {
                    if (mob.CurrentChunk == Game1.currentChunkNumber) batch.DrawString(Game1.pericles14, "" + ((mob.getX() * 40) + mob.subPixel.X) + "," + ((mob.getY() * 40) + mob.subPixel.Y), new Vector2(mob.getX() * 40, (mob.getY() * 40) - 10), Color.Gray);
                    else batch.DrawString(Game1.pericles1, "" + mob.ToString().Substring(15) + " " + mob.CurrentChunk + ((mob.getX() * 40) + mob.subPixel.X) + "," + ((mob.getY() * 40) + mob.subPixel.Y), new Vector2(mob.getX() * 40, (mob.getY() * 40) - 10), Color.Gray);
                }
        }
        public void Drawstatic(SpriteBatch batch)
        {

            if (isShown)
            {
                batch.Draw(Game1.Blur, new Rectangle(0, 425, 800, 25), Color.White);
                batch.DrawString(Game1.pericles14, Command + "_", new Vector2(2, 427), Color.Gray);
                String[] temp = history.ToArray();

                Array.Reverse(temp);

                for (int i = 0; i < temp.Length; i++)
                    batch.DrawString(Game1.pericles1, temp[i], new Vector2(3, 420 - (i * 13)), Color.DarkGray);
            }
        }
    }
}
