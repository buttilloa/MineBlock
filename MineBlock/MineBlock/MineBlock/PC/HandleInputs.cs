using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineBlock
{
    public class HandleInputs
    {

        public static Boolean[] canType = new Boolean[10];
        public static Boolean[] canTypeNum = new Boolean[10];
        public static Boolean isKeyDown(String key)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown((Keys)Enum.Parse(typeof(Keys), key))) return true;
             return false;

        }
        public static Boolean isKeyUp(String key)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyUp((Keys)Enum.Parse(typeof(Keys), key))) return true;
            return false;

        }
        public static Vector2 moveHighlighter(Vector2 high)
        {
            MouseState ms = Mouse.GetState();

            //if(Game1.menu.state == Managers.MenuRef.GameStates.Playing)
            //return Vector2.Transform(new Vector2(ms.X,ms.Y), Matrix.Invert(Game1.cameraTransform));
            if (Game1.menu.state == Managers.MenuRef.GameStates.Playing)
            {
                Mouse.SetPosition((int)MathHelper.Clamp(ms.X, 0, 800), (int)MathHelper.Clamp(ms.Y, -10, 500));
                return new Vector2(ms.X / 40, ms.Y / 40);
            }
            return new Vector2(ms.X, ms.Y);
        }
        public static Vector2 getMousepos()
        {
            MouseState ms = Mouse.GetState();
            return new Vector2(ms.X/40, ms.Y/40);
        }
        public static String SimNumPad(String chunk)
        {

            for (int i = -0; i <= 9; i++)
            {
                KeyboardState keys = Keyboard.GetState();
                //int temp = i + 1;
                string test = "NumPad" + i;// NumPad

                if (keys.IsKeyDown((Keys)Enum.Parse(typeof(Keys), test)) && canType[i])
                {
                    canType[i] = false;
                    return test.Substring(test.Length - 1);

                }
                if (keys.IsKeyUp((Keys)Enum.Parse(typeof(Keys), test)))
                    canType[i] = true;
            }
            return "";
        }
        public static int NumPadKeys()
        {
            for (int i = -0; i <= 8; i++)
            {
                KeyboardState keys = Keyboard.GetState();
                int temp = i + 1;
                string test = "NumPad" + temp;
                if (keys.IsKeyDown((Keys)Enum.Parse(typeof(Keys), test)))
                {

                    return i;

                }

            }
            return 10;
        }
        public static int NumKeys()
        {
            for (int i = -0; i <= 8; i++)
            {
                KeyboardState keys = Keyboard.GetState();
                int temp = i + 1;
                string test = "D" + temp;
                if (keys.IsKeyDown((Keys)Enum.Parse(typeof(Keys), test)))
                {

                    return i;

                }

            }
            return 10;
        }
        public static int HotBar(int current)
        {
            for (int i = -0; i <= 8; i++)
            {
                KeyboardState keys = Keyboard.GetState();
                int temp = i + 1;
                string test = "D" + temp;// NumPad
                if (keys.IsKeyDown((Keys)Enum.Parse(typeof(Keys), test)))
                {

                    return i;

                }

            }
            return current;
        }
        public static Boolean LeftTrigger()
        {
            MouseState ms = Mouse.GetState();
            if (ms.LeftButton == ButtonState.Pressed) return true;
            return false;
        }
        public static Boolean RightTrigger()
        {
            MouseState ms = Mouse.GetState();
            if (ms.RightButton == ButtonState.Pressed) return true;
            return false;
        }







    }
}
