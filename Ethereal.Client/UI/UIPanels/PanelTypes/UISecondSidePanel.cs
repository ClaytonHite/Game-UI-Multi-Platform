using Ethereal.Client.Source.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics.Metrics;

namespace Ethereal.Client.UI.UIPanels.PanelTypes
{
    public class UISecondSidePanel : UIObject
    {
        public Rectangle _interactionRectangle;

        public UISecondSidePanel(string objectName, Rectangle interactionArea, int textureIndex) : base(objectName)
        {
            int Width = Globals.ScreenWidth / 5;
            if (Settings.FirstSidePanelLeft && Settings.SecondSidePanelLeft)
            {
                Vector2 StartPosition = new Vector2(Width, 0);
                Vector2 EndPosition = new Vector2(Width, Globals.ScreenHeight);
                _interactionRectangle = new Rectangle((int)StartPosition.X, (int)StartPosition.Y, (int)EndPosition.X + 1, (int)EndPosition.Y);
            }
            else if (Settings.FirstSidePanelLeft && !Settings.SecondSidePanelLeft)
            {
                Vector2 StartPosition = new Vector2(Globals.ScreenWidth - Width, 0);
                Vector2 EndPosition = new Vector2(Width, Globals.ScreenHeight);
                _interactionRectangle = new Rectangle((int)StartPosition.X, (int)StartPosition.Y, (int)EndPosition.X + 1, (int)EndPosition.Y);
            }
            else if (Settings.SecondSidePanelLeft)
            {
                Vector2 StartPosition = new Vector2(0, 0);
                Vector2 EndPosition = new Vector2(Width, Globals.ScreenHeight);
                _interactionRectangle = new Rectangle((int)StartPosition.X, (int)StartPosition.Y, (int)EndPosition.X + 1, (int)EndPosition.Y);
            }
            else
            {
                Vector2 StartPosition = new Vector2(Globals.ScreenWidth - (Width * 2), 0);
                Vector2 EndPosition = new Vector2(Width, Globals.ScreenHeight);
                _interactionRectangle = new Rectangle((int)StartPosition.X, (int)StartPosition.Y, (int)EndPosition.X + 1, (int)EndPosition.Y);
            }
        }
    }
}
