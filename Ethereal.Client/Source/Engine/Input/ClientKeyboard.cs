using Ethereal.Client.Source.Engine.Input.Keyboard;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethereal.Client.Source.Engine.Input
{
    public class ClientKeyboard
    {

        public KeyboardState newKeyboard;
        public KeyboardState oldKeyboard;
        public List<ClientKeys> pressedKeys = new List<ClientKeys>();
        public List<ClientKeys> previousPressedKeys = new List<ClientKeys>();

        public virtual void Update()
        {
            newKeyboard = Microsoft.Xna.Framework.Input.Keyboard.GetState();
            GetPressedKeys();
        }

        public void UpdateOld()
        {
            oldKeyboard = newKeyboard;

            previousPressedKeys = new List<ClientKeys>();
            for (int i = 0; i < pressedKeys.Count; i++)
            {
                previousPressedKeys.Add(pressedKeys[i]);
            }
        }


        public bool GetPress(string KEY)
        {

            for (int i = 0; i < pressedKeys.Count; i++)
            {
                if (pressedKeys[i].key == KEY)
                {
                    return true;
                }
            }
            return false;
        }

        public virtual void GetPressedKeys()
        {
            pressedKeys.Clear();
            for (int i = 0; i < newKeyboard.GetPressedKeys().Length; i++)
            {
                pressedKeys.Add(new ClientKeys(newKeyboard.GetPressedKeys()[i].ToString(), 1));
            }
        }
    }
}
