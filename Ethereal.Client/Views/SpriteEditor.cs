using Ethereal.Client.Source.Engine;
using Ethereal.Client.UI;
using Ethereal.Client.UI.UIObjects;
using Ethereal.Client.UI.UIPanels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Ethereal.Client.Views
{
    internal class SpriteEditor : BaseWindow
    {
        private SpriteBatch _spriteBatch;
        private ContentManager Content;
        private List<UIObject> _objects;
        TextureAtlas _UIAtlas;
        private UIObject DisplaySprite = new UIObject("DisplaySprite");
        private Rectangle _sourceRectangle = new Rectangle();
        public SpriteEditor(SpriteBatch _spriteBatch, ContentManager content) : base(_spriteBatch, content)
        {
            this._spriteBatch = _spriteBatch;
            this.Content = content;
            _objects = new List<UIObject>();
            _UIAtlas = new TextureAtlas(Content.Load<Texture2D>("2D/UI/UISprites"), 1, 3, _spriteBatch);
            LoadSpriteEditor();
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
                if (CheckForButtonInput(children)) return;
                _objects[i].Update();
                if (_objects[i].ObjectName == "CreatorPanel")
                {
                    for (int y = 0; y < children.Count; y++)
                    {
                        if (children[y].ObjectName == "RectangleX" && children[y].Text != string.Empty)
                        {
                            _sourceRectangle.X = Convert.ToInt32(children[y].Text);
                        }
                        if (children[y].ObjectName == "RectangleY" && children[y].Text != string.Empty)
                        {
                            _sourceRectangle.Y = Convert.ToInt32(children[y].Text);
                        }
                        if (children[y].ObjectName == "RectangleWidth" && children[y].Text != string.Empty)
                        {
                            _sourceRectangle.Width = Convert.ToInt32(children[y].Text);
                        }
                        if (children[y].ObjectName == "RectangleHeight" && children[y].Text != string.Empty)
                        {
                            _sourceRectangle.Height = Convert.ToInt32(children[y].Text);
                        }
                    }
                }
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

        public void ChangeWindow(ButtonActions action)
        {
            switch (action)
            {
                case ButtonActions.LogIn:
                    _objects.Clear();
                    //LoadAccountLoginPanel();
                    break;
                case ButtonActions.CreateSprites:
                    //LoadSpriteCreator();
                    //LoadAccountLoginPanel();
                    break;
                case ButtonActions.GoBack:
                    _objects.Clear();
                    //LoadIntroScreen();
                    break;
            }
        }
        public void LoadSpriteEditor()
        {
            Panel Menu = new Panel("SideBar", 1, 1, 12);
            Menu.SetInteractionArea(new Rectangle(Globals.ScreenWidth - 400, 0, 400, Globals.ScreenHeight));
            _objects.Add(Menu);
            List<UIObject> objs = new List<UIObject>();
            objs.Add(new Button("Create", ButtonActions.CreateSprites, 1, true));
            objs.Add(new Button("Load", ButtonActions.LoadSprites, 1, true));
            objs.Add(new Button("Save", ButtonActions.SaveSprites, 1, true));
            objs.Add(new Button("Delete", ButtonActions.DeleteSprites, 1, true));
            RegisterObjectInGrid(Menu, objs);
            Menu.LoadContent();
        }
        private void RegisterObjectInGrid(Panel panel, List<UIObject> objs)
        {
            for (int i = 0; i < objs.Count; i++)
            {
                panel.AddToPanelGrid(objs[i]);
                _objects.Add(objs[i]);
            }
        }
        public void LoadSpriteCreator()
        {
            /*CreateSprite = new Panel("CreatorPanel", 1, false);
            CreateSprite.SetInteractionArea(new Rectangle(Globals.ScreenWidth - 800, 0, 400, Globals.ScreenHeight));
            CreateSprite.AddInputTextBox("RectangleX", 1, 1, true, false, 3, "Rectangle X");
            CreateSprite.AddInputTextBox("RectangleY", 1, 1, true, false, 3, "Rectangle Y");
            CreateSprite.AddInputTextBox("RectangleWidth", 1, 1, true, false, 3, "RectangleWidth");
            CreateSprite.AddInputTextBox("RectangleHeight", 1, 1, true, false, 3, "RectangleHeight");
            CreateSprite.AddInputTextBox("Sprite Size X", 1, 1, true, false, 3, "Sprite Size X");
            CreateSprite.AddInputTextBox("Sprite Size Y", 1, 1, true, false, 3, "Sprite Size Y");
            CreateSprite.AddInputTextBox("Item Id", 1, 1, true, false, 3, "Item Id");
            CreateSprite.AddInputTextBox("Action Id", 1, 1, true, false, 3, "Action Id");
            CreateSprite.AddInputTextBox("Unique Id", 1, 1, true, false, 3, "Unique Id");
            CreateSprite.AddInputTextBox("Article", 1, 1, true, false, 3, "Article");
            CreateSprite.AddInputTextBox("Name", 1, 1, true, false, 3, "Name");
            CreateSprite.AddInputTextBox("Description", 1, 1, true, false, 3, "Description");
            CreateSprite.AddInputTextBox("Count", 1, 1, true, false, 3, "Count");
            CreateSprite.LoadContent();
            _objects.Add(CreateSprite);
            DisplaySprite.SetInteractionArea(new Rectangle(Globals.ScreenWidth - 928, 0, 128, 128));
            _objects.Add(DisplaySprite);*/
        }
    }
}
