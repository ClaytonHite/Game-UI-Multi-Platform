using Ethereal.Client.Source.Engine;
using Ethereal.Client.UI.UIPanels;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using static Ethereal.Client.UI.UIObjects.Button;
using Ethereal.Client.UI;
using Ethereal.Client.UI.UIObjects;

namespace Ethereal.Client.Views
{
    internal class MapEditor : BaseWindow
    {
        private SpriteBatch _spriteBatch;
        private ContentManager Content;
        private List<UIObject> _objects;
        TextureAtlas _UIAtlas;
        private Panel Menu;
        public MapEditor(SpriteBatch _spriteBatch, ContentManager content) : base(_spriteBatch, content)
        {
            this._spriteBatch = _spriteBatch;
            this.Content = content;
            _objects = new List<UIObject>();
            _UIAtlas = new TextureAtlas(Content.Load<Texture2D>("2D/UI/UISprites"), 1, 3, _spriteBatch);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent()
        {
            LoadMapEditor();
            for (int i = 0; i < _objects.Count; i++)
            {
                _objects[i].LoadContent();
            }
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < _objects.Count; i++)
            {
                _objects[i].Update();
            }
            base.Update(gameTime);
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
                case ButtonActions.GoBack:
                    _objects.Clear();
                    //LoadIntroScreen();
                    break;
            }
        }
        public void LoadMapEditor()
        {

        }
    }
}
