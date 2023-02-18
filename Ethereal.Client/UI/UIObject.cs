using Ethereal.Client.Source.Engine;
using Ethereal.Client.UI.UIPanels;
using Ethereal.Client.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using static Ethereal.Client.UI.UIObjects.Panel;

namespace Ethereal.Client.UI
{
    public class UIObject
    {
        public int TextureIndex { get; set; }
        public ButtonActions? Action;
        public bool CenteredOnScreen;
        public List<UIObject> Children = new List<UIObject>();
        public string ObjectName;

        //Handle keyboard
        public string Text = "";
        public bool Tabbed = false;
        public bool IsFocused = false;
        public bool EnterPressed = false;
        public float InteractionTimer;
        public bool CenteredText;
        public int lengthCounter;
        public Color TextureColor;
        private KeyboardState _newKeyboard;
        private KeyboardState _oldKeyboard;
        private Keys[] _newKeys;
        private Keys[] _oldKeys;
        private float _timerSpeed;
        private bool _capitalize = false;

        //Positioning
        public Rectangle InteractionArea { get; set; }
        public UIObject(string objectName)
        {
            ObjectName = objectName;
            TextureColor = Color.LightGray;
            InteractionTimer = 0;
            _timerSpeed = 100;
        }

        public virtual void LoadContent()
        {

        }
        public virtual void Update()
        {
            if (GetInteractionArea().Contains(Globals.Mouse.newMousePos) && Globals.Mouse.LeftClickRelease())
            {
                OnClick();
            }
            if (!GetInteractionArea().Contains(Globals.Mouse.newMousePos) && Globals.Mouse.LeftClickRelease())
            {
                IsFocused = false;
            }
            if (IsFocused)
            {
                _newKeyboard = Globals.Keyboard.newKeyboard;
                _newKeys = Globals.Keyboard.newKeyboard.GetPressedKeys();
                _oldKeyboard = Globals.Keyboard.oldKeyboard;
                _oldKeys = Globals.Keyboard.oldKeyboard.GetPressedKeys();
                for (int i = 0; i < _newKeys.Length; i++)
                {
                    if (_newKeys[i] != Keys.None)
                    {
                        if (!_oldKeys.Contains(_newKeys[i]))
                        {
                            if (_newKeys[i] == Keys.CapsLock)
                            {
                                _capitalize = _capitalize == true ? false : true;
                            }
                            else if (_newKeys[i] == Keys.Back && InteractionTimer > _timerSpeed)
                            {
                                InteractionTimer = 0;
                                HandleKey(_newKeys[i]);
                            }
                            else if (_newKeys[i] != Keys.LeftShift && _newKeys[i] != Keys.RightShift)
                            {
                                HandleKey(_newKeys[i]);
                            }
                        }
                        else if (_newKeys[i] == Keys.Back && InteractionTimer > _timerSpeed)
                        {
                            InteractionTimer = 0;
                            HandleKey(_newKeys[i]);
                        }
                    }
                }
                if (InteractionTimer < _timerSpeed)
                {
                    InteractionTimer += (float)Globals.GameTime.ElapsedGameTime.TotalMilliseconds;
                }
            }
        }

        private void HandleKey(Keys currentKey)
        {
            string keyString = currentKey.ToString();
            if (currentKey == Keys.Space)
            {
                Text += " ";
            }
            else if ((currentKey == Keys.Back || currentKey == Keys.Delete) && Text.Length >= 0)
            {
                if (Text.Length > 0)
                {
                    Text = Text.Remove(Text.Length - 1);
                }
            }
            else if (currentKey == Keys.Tab && InteractionTimer > _timerSpeed)
            {
                InteractionTimer = 0;
                IsFocused = false;
                TextureColor = Color.LightGray;
                if (_newKeyboard.IsKeyDown(Keys.LeftShift) || _newKeyboard.IsKeyDown(Keys.RightShift))
                {
                    OnTabPress(-1);
                }
                else
                {
                    OnTabPress(1);
                }
                return;
            }
            else if (currentKey == Keys.Enter)
            {
                OnEnterPress();
                return;
            }
            else if (currentKey == Keys.OemOpenBrackets || currentKey == Keys.OemCloseBrackets || currentKey == Keys.OemMinus || currentKey == Keys.OemPeriod || currentKey == Keys.Decimal || currentKey == Keys.OemComma ||
                     currentKey == Keys.OemBackslash || currentKey == Keys.OemQuestion || currentKey == Keys.OemTilde || currentKey == Keys.OemPlus || currentKey == Keys.OemQuotes || currentKey == Keys.OemSemicolon)
            {
                bool switchCase = _newKeyboard.IsKeyDown(Keys.LeftShift) || _newKeyboard.IsKeyDown(Keys.RightShift) ? true : false;
                switch (currentKey)
                {
                    case Keys.OemOpenBrackets:
                        Text += switchCase ? "{" : "[";
                        break;
                    case Keys.OemCloseBrackets:
                        Text += switchCase ? "}" : "]";
                        break;
                    case Keys.OemTilde:
                        Text += switchCase ? "~" : "`";
                        break;
                    case Keys.OemQuotes:
                        Text += switchCase ? "\"" : @"'";
                        break;
                    case Keys.OemPlus:
                        Text += switchCase ? "+" : "=";
                        break;
                    case Keys.OemMinus:
                        Text += switchCase ? "_" : "-";
                        break;
                    case Keys.OemPeriod:
                    case Keys.Decimal:
                        Text += switchCase ? ">" : ".";
                        break;
                    case Keys.OemComma:
                        Text += switchCase ? "<" : ",";
                        break;
                    case Keys.OemSemicolon:
                        Text += switchCase ? ":" : ";";
                        break;
                    case Keys.OemPipe:
                        Text += switchCase ? "|" : @"\";
                        break;
                    case Keys.OemQuestion:
                        Text += switchCase ? "?" : "/";
                        break;
                }
            }
            else if (currentKey == Keys.D1 || currentKey == Keys.D2 || currentKey == Keys.D3 || currentKey == Keys.D4 || currentKey == Keys.D5 || currentKey == Keys.D6 || currentKey == Keys.D7 || currentKey == Keys.D8 || currentKey == Keys.D9 || currentKey == Keys.D0)
            {
                if (_newKeyboard.IsKeyDown(Keys.LeftShift) || _newKeyboard.IsKeyDown(Keys.RightShift))
                {
                    switch (keyString.Substring(1))
                    {
                        case "1":
                            Text += "!";
                            break;
                        case "2":
                            Text += "@";
                            break;
                        case "3":
                            Text += "#";
                            break;
                        case "4":
                            Text += "$";
                            break;
                        case "5":
                            Text += "%";
                            break;
                        case "6":
                            Text += "^";
                            break;
                        case "7":
                            Text += "&";
                            break;
                        case "8":
                            Text += "*";
                            break;
                        case "9":
                            Text += "(";
                            break;
                        case "0":
                            Text += ")";
                            break;
                    }
                }
                else
                {
                    Text += keyString.Substring(1);
                }
            }
            else
            {
                if (keyString.ToLower() == "tab")
                {
                    return;
                }
                if (_newKeyboard.IsKeyDown(Keys.LeftShift) || _newKeyboard.IsKeyDown(Keys.RightShift) || _capitalize)
                {
                    Text += keyString;
                }
                else
                {
                    Text += keyString.ToLower();
                }
            }
            InteractionTimer += (float)Globals.GameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public virtual Rectangle GetInteractionArea()
        {
            if (InteractionArea != Rectangle.Empty)
            {
                return InteractionArea;
            }
            return new Rectangle();
        }
        public virtual void Draw(TextureAtlas textureAtlas)
        {
        }
        public virtual void CenterObjectOnScreen()
        {
            CenteredOnScreen = true;
            int Width = Globals.ScreenWidth / 7;
            if (Width <= 300)
            {
                Width = 320;
            }
            int Height = Globals.ScreenHeight / 3;
            SetInteractionArea(new Rectangle(Globals.ScreenWidth / 2 - Width / 2, Globals.ScreenHeight / 2 - Height / 2, Width, Height));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="size"></param>
        public virtual void PositionObjectInSpecificArea(float screenWidthPercentage, float screenHeightPercentage)
        {
            //TODO!!!
            //SetInteractionArea(new Rectangle(Globals.ScreenWidth * location.X, Globals.ScreenHeight - destinationRectangle.Y, Globals.ScreenWidth - 400, Globals.ScreenHeight));
        }
        public virtual void IsSelected()
        {

        }
        public virtual void OnClick()
        {

        }
        public virtual void OnTabPress(int direction)
        {
            IsFocused = false;
            if (direction > 0)
            {
                Tabbed = true;
            }
            if (direction < 0)
            {
                Tabbed = false;
            }
        }
        public virtual void OnEnterPress()
        {

        }

        public virtual void SetInteractionArea(Rectangle interactionArea)
        {
            InteractionArea = interactionArea;
            /*float locationX = (float)interactionArea.X / (float)Globals.ScreenWidth;
            float locationY = (float)interactionArea.Y / (float)Globals.ScreenHeight;
            float SizeX = (float)interactionArea.Width / (float)Globals.ScreenWidth;
            float SizeY = (float)interactionArea.Height / (float)Globals.ScreenHeight;
            location = new Vector2(locationX, locationY);
            size = new Vector2(SizeX, SizeY);*/
        }

        public virtual void ScreenResize()
        {

        }
    }
}
