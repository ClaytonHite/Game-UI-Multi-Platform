using Ethereal.Client.Source.Engine;
using Ethereal.Client.UI;
using Ethereal.Client.UI.UIObjects;
using Ethereal.Client.UI.UIPanels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Ethereal.Client.Views
{
    public class IntroWindow : BaseWindow
    {
        private SpriteBatch _spriteBatch;
        private ContentManager Content;
        private List<UIObject> _objects;
        private TextureAtlas _UIAtlas;
        private UIWindow UIWindow;
        public IntroWindow(SpriteBatch _spriteBatch, ContentManager content) : base(_spriteBatch, content)
        {
            this._spriteBatch = _spriteBatch;
            this.Content = content;
            _objects = new List<UIObject>();
            _UIAtlas = new TextureAtlas(Content.Load<Texture2D>("2D/UI/UISprites"), 1, 3, _spriteBatch);
            LoadIntroScreen();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent()
        {
            for (int i = 0; i < _objects.Count; i++)
            {
                _objects[i].LoadContent();
            }
        }

        public override void ScreenResize()
        {
            for (int i = 0; i < _objects.Count; i++)
            {
                _objects[i].ScreenResize();
            }
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < _objects.Count; i++)
            {
                List<UIObject> children = _objects[i].Children;
                if(CheckForButtonInput(children)) return;
                _objects[i].Update();
            }
            base.Update(gameTime);
        }

        public bool CheckForButtonInput(List<UIObject> objs)
        {
            for (int i = 0; i < objs.Count; i++)
            {
                if (objs[i].Action != null)
                {
                    ButtonActions? newAction = objs[i].Action;
                    objs[i].Action = null;
                    ChangeWindow(newAction);
                    return true;
                }
                if (objs[i].Tabbed)
                {
                    if (i == objs.Count - 1)
                    {
                        for (int j = 0; j < objs.Count; j++)
                        {
                            objs[j].Tabbed = false;
                            objs[j].IsFocused = false;
                        }
                        objs[0].IsFocused = true;
                        return false;
                    }
                    objs[i].IsFocused = false;
                    objs[i + 1].IsFocused = true;
                }
                if (objs[i].EnterPressed)
                {
                    objs[i].OnClick();
                    objs[i].EnterPressed = false;
                    return true;
                }
            }
            return false;
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            for (int i = 0; i < _objects.Count; i++)
            {
                _objects[i].Draw(_UIAtlas);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void ChangeWindow(ButtonActions? action)
        {
            switch(action)
            {
                case ButtonActions.LogIn:
                    _objects.Clear();
                    LoadAccountLoginPanel();
                    break;
                case ButtonActions.GoBack:
                    _objects.Clear();
                    LoadIntroScreen();
                    break;
                case ButtonActions.AccountLogIn:
                    for (int i = 0; i < _objects.Count; i++)
                    {
                        for (int j = 0; j < _objects[i].Children.Count; j++)
                        {
                            string AccountName;
                            string AccountPassword;
                            if (_objects[i].Children[j].ObjectName == "AccountName")
                            {
                                AccountName = _objects[i].Children[j].Text;
                            }
                            if (_objects[i].Children[j].ObjectName == "Password")
                            {
                                AccountPassword = _objects[i].Children[j].Text;
                            }
                        }
                    }
                    //CEH TODO Send off for login
                    _objects.Clear();
                    LoadAccountLoginPanel();
                    break;
                case ButtonActions.Exit:
                    _objects.Clear();
                    Content.Unload();
                    Ethereal.WindowEvent("Exit");
                    break;
                case ButtonActions.MapEditor:
                    _objects.Clear();
                    Content.Unload();
                    Ethereal.WindowEvent("MapEditor");
                    break;
                case ButtonActions.SpriteEditor:
                    _objects.Clear();
                    Content.Unload();
                    Ethereal.WindowEvent("SpriteEditor");
                    break;
            }
        }

        private void RegisterObjectInGrid(Panel panel, List<UIObject> objs)
        {
            for (int i = 0; i < objs.Count; i++)
            {
                panel.AddToPanelGrid(objs[i]);
                _objects.Add(objs[i]);
            }
        }

        public void LoadIntroScreen()
        {
            _objects.Clear();
            Panel loginMenu = new Panel("LoginMenu", 1, 1, 6);
            loginMenu.CenterObjectOnScreen();
            _objects.Add(loginMenu);
            List<UIObject> objs = new List<UIObject>();
            objs.Add(new Button("Log In", ButtonActions.LogIn, 1, true));
            objs.Add(new Button("Create Account", ButtonActions.CreateAccount, 1, true));
            objs.Add(new Button("Options", ButtonActions.Options, 1, true));
            objs.Add(new Button("Exit", ButtonActions.Exit, 1, true));
            objs.Add(new Button("MapEditor", ButtonActions.MapEditor, 1, true));
            objs.Add(new Button("SpriteEditor", ButtonActions.SpriteEditor, 1, true));
            RegisterObjectInGrid(loginMenu, objs);
            UIWindow = new UIWindow("Inventory1", 25, new Rectangle(100, 100, 300, 300));
            _objects.Add(UIWindow);
            LoadContent();
        }

        public void LoadAccountLoginPanel()
        {
            _objects.Clear();
            Panel accountLogInMenu = new Panel("AccountLoginMenu", 1, 1, 4);
            accountLogInMenu.CenterObjectOnScreen();
            _objects.Add(accountLogInMenu);
            List<UIObject> objs = new List<UIObject>();
            InputTextBox accountTextBox = new InputTextBox("AccountName", 1, "Account Name");
            accountTextBox.CenteredText = true;
            InputTextBox passwordTextBox = new InputTextBox("Password", 1, "Password", ButtonActions.AccountLogIn);
            passwordTextBox.CenteredText = true;
            passwordTextBox.PasswordBox = true;
            objs.Add(accountTextBox);
            objs.Add(passwordTextBox);
            objs.Add(new Button("Log In", ButtonActions.AccountLogIn, 1, true));
            objs.Add(new Button("Go Back", ButtonActions.GoBack, 1, true));
            RegisterObjectInGrid(accountLogInMenu, objs);
            LoadContent();
        }

        public void MapEditor()
        {

        }
    }
}
