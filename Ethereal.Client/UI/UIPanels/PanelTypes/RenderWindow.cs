using Ethereal.Client.Source.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Ethereal.Client.UI.UIPanels.PanelTypes
{
    public class RenderWindow
    {
        public Rectangle Rectangle;
        public Vector2 SpriteIncrements;
        private Vector2 StartPosition;
        private Vector2 EndPosition;
        private float Width;
        private float Height;
        //private TileMap _tileMap;
        public RenderWindow(Texture2D texture2D)
        {
            Height = Globals.ScreenHeight - (Globals.ScreenHeight / 4);
            if (Settings.SecondSidePanelExists)
            {
                Width = Globals.ScreenWidth - ((Globals.ScreenWidth / 5) * 2);
            }
            else
            {
                Width = Globals.ScreenWidth - ((Globals.ScreenWidth / 5));
            }
            if (Settings.FirstSidePanelLeft && Settings.SecondSidePanelLeft && Settings.SecondSidePanelExists)
            {
                StartPosition = new Vector2((Globals.ScreenWidth / 5) * 2, 0);
                EndPosition = new Vector2(Width, Height);
            }
            else if(Settings.FirstSidePanelLeft && !Settings.SecondSidePanelLeft && Settings.SecondSidePanelExists)
            {
                StartPosition = new Vector2(Globals.ScreenWidth / 5, 0);
                EndPosition = new Vector2(Width, Height);
            }
            else if (Settings.SecondSidePanelLeft && Settings.SecondSidePanelExists)
            {
                StartPosition = new Vector2(Globals.ScreenWidth / 5, 0);
                EndPosition = new Vector2(Width, Height);
            }
            else
            {
                StartPosition = new Vector2(0, 0);
                EndPosition = new Vector2(Width, Height);
            }
            Rectangle = new Rectangle((int)StartPosition.X, (int)StartPosition.Y, (int)EndPosition.X, (int)EndPosition.Y);
            SpriteIncrements = new Vector2(Width / 8, Height / 9);
            //_tileMap = new TileMap();
            //_tileMap.LoadMap(new TextureAtlas(texture2D, 63, 16));
        }
        /*public void Draw(SpriteBatch spriteBatch)
        {
            double startColumn = Math.Floor(this.StartPosition.X / _tileMap.TextureAtlas.Texture.Width);
            var endColumn = startColumn + (this.StartPosition.X / _tileMap.TextureAtlas.Texture.Width);
            var startRow = Math.Floor(this.EndPosition.Y / _tileMap.TextureAtlas.Texture.Height);
            var endRow = startRow + (this.EndPosition.Y / _tileMap.TextureAtlas.Texture.Height);
            var offsetX = -this.camera.x + startCol * map.tsize;
            var offsetY = -this.camera.y + startRow * map.tsize;
            spriteBatch.Draw(spriteBatch, new Vector2(uiManager.RenderWindow.Rectangle.Center.X - 32, uiManager.RenderWindow.Rectangle.Center.Y - 32), 2);
        }*/
    }
}
