using Ethereal.Client.Source.Engine;
using Ethereal.Client.UI.UIPanels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace Ethereal.Client.Views
{
    public class BaseWindow
    {
        public Type Type { get; set; }
        private SpriteBatch _spriteBatch;
        private ContentManager _contentManager;
        private FrameCounter _frameCounter = new FrameCounter();
        private long counter = 0;
        public int FrameTicking = 0;
        public string GameDuration = string.Empty;
        public string FPS = "";
        public BaseWindow(SpriteBatch _spriteBatch, ContentManager content)
        {
            this._spriteBatch = _spriteBatch;
            this._contentManager = content;
            Type type = this.GetType();
        }
        public virtual void Initialize()
        {

        }

        public virtual void LoadContent()
        {

        }

        public virtual void ScreenResize()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _frameCounter.Update(deltaTime);
            counter++;
            FrameTicking = (int)(counter / 60);
            int hours = FrameTicking / 3600;
            int mins = (FrameTicking % 3600) / 60;
            int secs = FrameTicking % 60;
            GameDuration = string.Format("{0:D2}:{1:D2}:{2:D2}", hours, mins, secs);
            FPS = $"{_frameCounter.AverageFramesPerSecond.ToString("0.00")}";
        }

        public virtual void Draw(GameTime gameTime)
        {

        }

        public virtual void ChangeWindow(ButtonActions? action)
        {

        }
    }
}
