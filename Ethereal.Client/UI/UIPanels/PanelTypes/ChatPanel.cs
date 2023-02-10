using Ethereal.Client.Source.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethereal.Client.UI.UIPanels.PanelTypes
{
    public class ChatPanel
    {
        public Rectangle Rectangle;
        public ChatPanel()
        {
            Vector2 StartPosition = new Vector2();
            Vector2 EndPosition = new Vector2();
            int Height = Globals.ScreenHeight / 4;
            int Width = 0;
            if (Settings.SecondSidePanelExists)
            {
                Width = Globals.ScreenWidth - ((Globals.ScreenWidth / 5) * 2);
            }
            else
            {
                Width = Globals.ScreenWidth - ((Globals.ScreenWidth / 5));
            }
            if(Settings.FirstSidePanelLeft && Settings.SecondSidePanelLeft && Settings.SecondSidePanelExists)
            {
                StartPosition = new Vector2((Globals.ScreenWidth / 5) * 2, Globals.ScreenHeight - Height);
                EndPosition = new Vector2(Width + 2, Height + 1);
            }
            else if (Settings.FirstSidePanelLeft && !Settings.SecondSidePanelLeft && Settings.SecondSidePanelExists)
            {
                StartPosition = new Vector2(Globals.ScreenWidth / 5, Globals.ScreenHeight - Height);
                EndPosition = new Vector2(Width + 2, Height + 1);
            }
            else if(Settings.SecondSidePanelLeft && Settings.SecondSidePanelExists)
            {
                StartPosition = new Vector2(Globals.ScreenWidth / 5, Globals.ScreenHeight - Height);
                EndPosition = new Vector2(Width + 2, Height + 1);
            }
            else
            {
                StartPosition = new Vector2(0, Globals.ScreenHeight - Height);
                EndPosition = new Vector2(Width + 2, Height + 1);
            }
            Rectangle = new Rectangle((int)StartPosition.X, (int)StartPosition.Y, (int)EndPosition.X, (int)EndPosition.Y);
        }
    }
}
