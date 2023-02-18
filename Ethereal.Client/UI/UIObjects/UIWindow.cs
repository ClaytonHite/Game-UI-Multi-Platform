using Ethereal.Client.Source.Engine;
using Ethereal.Client.Source.Engine.Input;
using Microsoft.Xna.Framework;
using System.Linq;

namespace Ethereal.Client.UI.UIObjects
{
    public class UIWindow : UIObject
    {
        private ScrollBar _scrollBar;
        private Button _closeButton;
        private UIObject DragHandle;
        private Rectangle _resizeHandle;

        //inner window
        private Rectangle displayArea; //The inner window with item display
        private Rectangle[] items; // the items inside the inner window
        private Color[] itemsHighlighted;
        private int _columns;
        private int _rows;
        private int _itemSize;
        private int _padding;
        private int _windowBorder;
        private Rectangle _scissorRectangle;
        private int _minimumWindowHeight = 0;
        private int _maximumWindowHeight = 0;
        private bool _dragging;
        public UIWindow(string objectName, int amountOfItems, Rectangle interactionArea) : base(objectName)
        {
            items = new Rectangle[amountOfItems];
            itemsHighlighted = new Color[amountOfItems];
            _padding = 5;
            _windowBorder = interactionArea.Height / 20;
            displayArea = new Rectangle(interactionArea.X + (interactionArea.Width / 10) + _padding, interactionArea.Y + _windowBorder, interactionArea.Width - (interactionArea.Width / 10) - (_padding * 2), (interactionArea.Height - _windowBorder * 2));
            _itemSize = ((displayArea.Width - _padding) / 5);
            _columns = 5;
            _rows = items.Length / 5;
            _minimumWindowHeight = _itemSize + (interactionArea.Width / 10);
            for (var i = 0; i < _rows; i++)
            {
                for (var j = 0; j < _columns; j++)
                {
                    items[i * _columns + j] = new Rectangle(displayArea.X + (i * _itemSize) + _padding, displayArea.Y + (j * _itemSize) + _padding, _itemSize - _padding, _itemSize - _padding);
                    itemsHighlighted[i * _columns + j] = Color.DarkGray;
                }
            }
            Rectangle lastItem = items.Last();
            _maximumWindowHeight = lastItem.Y + lastItem.Height + _padding * 4 - interactionArea.Y;
            interactionArea.Height = _maximumWindowHeight;
            InteractionArea = interactionArea;
            AlignContainerItems();
        }

        private void AlignContainerItems()
        {
            //set up inner window
            displayArea = new Rectangle(InteractionArea.X + (InteractionArea.Width / 10) + _padding, InteractionArea.Y + _windowBorder, InteractionArea.Width - (InteractionArea.Width / 10) - (_padding * 2), (InteractionArea.Height - _windowBorder * 2));
            _scissorRectangle = new Rectangle(displayArea.X, displayArea.Y, displayArea.Width, displayArea.Height - (_padding / 2));

            //set up scrollbar
            _scrollBar = new ScrollBar($"{this.ObjectName}ScrollBar", new Rectangle(InteractionArea.X + _padding, displayArea.Y, (InteractionArea.Width / 10), displayArea.Height));

            //SetHandleInteractionArea
            _resizeHandle = new Rectangle(InteractionArea.X, InteractionArea.Y + InteractionArea.Height - (_padding * 2), InteractionArea.Width, _padding * 2);
        }        

        public override void Update()
        {
            bool isInButtonArea = InteractionArea.Contains(Globals.Mouse.newMousePos);
            ClientMouse mouse = Globals.Mouse;
            if (isInButtonArea)
            {
                if(_resizeHandle.Contains(mouse.newMousePos) && mouse.LeftClickHold())
                {
                    _dragging = true;
                }

                isInButtonArea = displayArea.Contains(Globals.Mouse.newMousePos);
                if (isInButtonArea)
                {
                    for (int i = 0; i < items.Length; i++)
                    {
                        isInButtonArea = items[i].Contains(Globals.Mouse.newMousePos);
                        if (isInButtonArea)
                        {
                            itemsHighlighted[i] = Color.White;
                        }
                        else if (!isInButtonArea)
                        {
                            itemsHighlighted[i] = Color.DarkGray;
                        }
                    }
                }
            }
            if(mouse.LeftClickRelease())
            {
                _dragging = false;
            }
            if(_dragging)
            {
                int windowYPos = (int)(InteractionArea.Height + mouse.newMousePos.Y - mouse.oldMousePos.Y);
                if (InteractionArea.Height > _minimumWindowHeight && InteractionArea.Height < _maximumWindowHeight)
                {
                    if (windowYPos < _minimumWindowHeight)
                    {
                        windowYPos = _minimumWindowHeight;
                    }
                    InteractionArea = new Rectangle(InteractionArea.X, InteractionArea.Y, InteractionArea.Width, windowYPos);
                    AlignContainerItems();
                }
                else
                {
                    if (windowYPos < _minimumWindowHeight)
                    {
                        windowYPos = _minimumWindowHeight;
                        InteractionArea = new Rectangle(InteractionArea.X, InteractionArea.Y, InteractionArea.Width, windowYPos);
                        AlignContainerItems();
                    }
                    else if (windowYPos > _maximumWindowHeight)
                    {
                        windowYPos = _maximumWindowHeight;
                        InteractionArea = new Rectangle(InteractionArea.X, InteractionArea.Y, InteractionArea.Width, windowYPos);
                        AlignContainerItems();
                    }
                    else
                    {
                        InteractionArea = new Rectangle(InteractionArea.X, InteractionArea.Y, InteractionArea.Width, windowYPos);
                        AlignContainerItems();
                    }
                }
            }
            base.Update();
        }

        public override void Draw(TextureAtlas textureAtlas)
        {
            textureAtlas.Draw(InteractionArea, 1);
            textureAtlas.Draw(displayArea, 1);
            _scrollBar.Draw(textureAtlas);
            for (int i = 0; i < items.Length; i++)
            {
                textureAtlas.Draw(items[i], 1, itemsHighlighted[i], _scissorRectangle);
            }
        }
    }
}
