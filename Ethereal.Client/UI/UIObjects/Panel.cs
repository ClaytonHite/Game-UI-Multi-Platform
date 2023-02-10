using Ethereal.Client.Source.Engine;
using Microsoft.Xna.Framework;

namespace Ethereal.Client.UI.UIObjects
{
    public class Panel : UIObject
    {
        public bool AutoResizeable;
        private Rectangle _PanelArea { get; set; }
        private Rectangle _PanelPadding { get; set; }
        public int PanelGridX;
        public int PanelGridY;
        public UIObject[,] PanelGrid;
        private int _gridWidth;
        private int _gridHeight;
        public Panel(string objectName, int textureIndex, int panelGridX, int panelGridY) : base(objectName)
        {
            TextureIndex = textureIndex;
            PanelGridX = panelGridX;
            PanelGridY = panelGridY;
            PanelGrid = new UIObject[PanelGridX, PanelGridY];
            _gridWidth = _PanelArea.Width / PanelGridX;
            _gridHeight = _PanelArea.Height / PanelGridY;
        }
        public Panel(string objectName, int textureIndex) : base(objectName)
        {
            TextureIndex = textureIndex;
            PanelGridX = 1;
            PanelGridY = 1;
            PanelGrid = new UIObject[PanelGridX, PanelGridY];
            _gridWidth = _PanelArea.Width / PanelGridX;
            _gridHeight = _PanelArea.Height / PanelGridY;
        }

        public void AddToPanelGrid(UIObject uIObject)
        {
            for (int y = 0; y < PanelGridY; y++)
            {
                for (int x = 0; x < PanelGridX; x++)
                {
                    if (PanelGrid[x, y] == null)
                    {
                        PanelGrid[x, y] = uIObject;
                        uIObject.SetInteractionArea(new Rectangle(_PanelPadding.X + (_gridWidth * x), _PanelPadding.Y + (_gridHeight * y), _gridWidth, _gridHeight - (_gridHeight / 10)));
                        Children.Add(uIObject);
                        return;
                    }
                }
            }
        }
        public override void ScreenResize()
        {
            if(CenteredOnScreen)
            {
                this.CenterObjectOnScreen();
            }
            for (int y = 0; y < PanelGridY; y++)
            {
                for (int x = 0; x < PanelGridX; x++)
                {
                    if (PanelGrid[x, y] != null)
                    {
                        PanelGrid[x, y].SetInteractionArea(new Rectangle(_PanelPadding.X + (_gridWidth * x), _PanelPadding.Y + (_gridHeight * y), _gridWidth, _gridHeight - (_gridHeight / 10)));
                    }
                }
            }
        }
        public override void LoadContent()
        {
            int count = Children.Count;
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].LoadContent();
            }
            if (AutoResizeable)
            {
                _PanelArea = new Rectangle(_PanelArea.X, _PanelArea.Y, _PanelArea.Width, 20 + 80 * count + 1);
            }
            base.LoadContent();
        }

        public override void Update()
        {
            base.Update();
        }
        public override void Draw(TextureAtlas textureAtlas)
        {
            textureAtlas.Draw(_PanelArea, 1);
            for (int y = 0; y < PanelGridY; y++)
            {
                for (int x = 0; x < PanelGridX; x++)
                {
                    if (PanelGrid[x, y] != null)
                    {
                        PanelGrid[x, y].Draw(textureAtlas);
                    }
                }
            }
        }

        public void TabPanel(object obj, int direction)
        {
            UIObject uiObj = (UIObject)obj;
            for (int i = 0; i < Children.Count; i++)
            {
                if (Children[i].ObjectName == uiObj.ObjectName)
                {
                    int index = i + direction;
                    if (index >= 0 && index < Children.Count)
                    {
                        Children[index].IsSelected();
                    }
                    else if (index < 0)
                    {
                        Children[Children.Count - 1].IsSelected();
                    }
                    else if (index == Children.Count)
                    {
                        Children[0].IsSelected();
                    }
                }
            }
        }
        public override void OnClick()
        {
            base.OnClick();
        }
        public override Rectangle GetInteractionArea()
        {
            return _PanelArea;
        }
        public override void SetInteractionArea(Rectangle interactionArea)
        {
            _PanelArea = interactionArea;
            base.SetInteractionArea(interactionArea);
            _PanelPadding = new Rectangle(_PanelArea.X + (_PanelArea.Width / 10), _PanelArea.Y + (_PanelArea.Height / 10), _PanelArea.Width - ((_PanelArea.Width / 10) * 2), _PanelArea.Height - ((_PanelArea.Height / 10) * 2));
            _gridWidth = _PanelPadding.Width / PanelGridX;
            _gridHeight = _PanelPadding.Height / PanelGridY;
        }
    }
}
