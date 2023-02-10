using Ethereal.Client.Source.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Ethereal.Client.UI.UIPanels.PanelTypes
{
    public class UISidePanel : UIObject
    {
        private Rectangle _interactionRectangle;
        private List<Rectangle> openWindows;

        public enum SidePanelLocation
        {
            Left, 
            TopMiddle,
            Right,
            BottomMiddle
        }
        public UISidePanel(string objectName, int textureIndex) : base(objectName)
        {
            if (Settings.FirstSidePanelLeft)
            {
                int Width = Globals.ScreenWidth / 5;
                Vector2 StartPosition = new Vector2(0, 0);
                Vector2 EndPosition = new Vector2(Width, Globals.ScreenHeight);
                _interactionRectangle = new Rectangle((int)StartPosition.X, (int)StartPosition.Y, (int)EndPosition.X + 1, (int)EndPosition.Y);
                TextureIndex = textureIndex;
            }
            else
            {
                int Width = Globals.ScreenWidth / 5;
                Vector2 StartPosition = new Vector2(Globals.ScreenWidth - Width, 0);
                Vector2 EndPosition = new Vector2(Width, Globals.ScreenHeight);
                _interactionRectangle = new Rectangle((int)StartPosition.X, (int)StartPosition.Y, (int)EndPosition.X + 1, (int)EndPosition.Y);
                TextureIndex = textureIndex;
            }
        }

        public void AddUIWindow()
        {

        }





    }
}
