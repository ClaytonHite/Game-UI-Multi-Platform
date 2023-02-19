using Ethereal.Client.Source.Engine;
using Ethereal.Client.Source.Engine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace Ethereal.Client.UI.UIObjects
{
    public class UIWindow : UIObject
    {
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
        private int _windowOffset = 0;
        private int _minimumWindowHeight = 0;
        private int _maximumWindowHeight = 0;
        private bool _dragging = false;
        private string _displayText = string.Empty;

        //scrollbar
        private Rectangle _scrollBarInteractionArea;
        private Rectangle _scrollUpRectangle;
        private Rectangle _scrollDownRectangle;
        private Rectangle _scrollBarSliderRectangle;
        private Rectangle _scrollArea;
        private bool _scrollBarDragging = false;
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
            _scissorRectangle = new Rectangle(displayArea.X, displayArea.Y + (_padding), displayArea.Width, displayArea.Height - (_padding * 2));

            //set up scrollbar
            AlignScrollbar(new Rectangle(InteractionArea.X + (_padding * 3), displayArea.Y, (InteractionArea.Width / 10) - _padding * 3, displayArea.Height));

            //SetHandleInteractionArea
            _resizeHandle = new Rectangle(InteractionArea.X, InteractionArea.Y + InteractionArea.Height - (_padding * 2), InteractionArea.Width, _padding * 2);
        }        

        public override void Update()
        {
            Vector2 mousePos = Globals.Mouse.newMousePos;
            bool isInButtonArea = InteractionArea.Contains(mousePos);
            ClientMouse mouse = Globals.Mouse;
            if (isInButtonArea)
            {
                if(_resizeHandle.Contains(mouse.newMousePos) && mouse.LeftClickHold())
                {
                    _dragging = true;
                }
                isInButtonArea = displayArea.Contains(mousePos);
                if (isInButtonArea)
                {
                    int mouseWheelDirection = mouse.GetMouseWheelChange();
                    if(mouseWheelDirection != 0)
                    {
                        ScrollInnerWindow(mouseWheelDirection);
                    }
                    for (int i = 0; i < items.Length; i++)
                    {
                        Rectangle rec = new Rectangle(items[i].X, items[i].Y + _windowOffset, items[i].Width, items[i].Height);
                        isInButtonArea = rec.Contains(mousePos);
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
                else
                {
                    for (int i = 0; i < items.Length; i++)
                    {
                        itemsHighlighted[i] = Color.DarkGray;
                    }
                }
                isInButtonArea = _scrollBarInteractionArea.Contains(mousePos);
                if (isInButtonArea)
                {
                    if(_scrollUpRectangle.Contains(mousePos) && Globals.Mouse.LeftClick())
                    {
                        ScrollInnerWindow(1);
                    }
                    else if (_scrollBarSliderRectangle.Contains(mousePos) && Globals.Mouse.LeftClickHold())
                    {
                        _scrollBarDragging = true;
                    }
                    else if (_scrollDownRectangle.Contains(mousePos) && Globals.Mouse.LeftClick())
                    {
                        ScrollInnerWindow(-1);
                    }
                }
            }
            if(mouse.LeftClickRelease())
            {
                _dragging = false;
                _scrollBarDragging = false;
            }
            if(_dragging && !_scrollBarDragging)
            {
                DragResizeBar();
            }
            if(_scrollBarDragging && !_dragging)
            {
                DragScrollBar();
            }
            base.Update();
        }

        private void ScrollInnerWindow(int direction)
        {
            int windowSizeWithItems = ((_itemSize) * _rows);
            int visibleWindowWithItems = windowSizeWithItems -_scissorRectangle.Height - _padding;
            if (direction > 0 && _windowOffset < 0)
            {
                _windowOffset += 10;
            }
            if (direction < 0)
            {
                if (_windowOffset > -1 * visibleWindowWithItems)
                {
                    _windowOffset -= 10;
                }
            }
            if(visibleWindowWithItems == 0)
            {
                visibleWindowWithItems = 1;
            }
            int scrollbarlength = (int)(((float)(windowSizeWithItems - visibleWindowWithItems) / (float)windowSizeWithItems) * (float)100);
            float position = (float)_windowOffset / (float)visibleWindowWithItems;
            SetScrollBarLength(scrollbarlength);
            SetScrollBarPosition((int)(position*-100));
        }

        private void DragResizeBar()
        {
            ClientMouse mouse = Globals.Mouse;
            int windowYPos = (int)(InteractionArea.Height + mouse.newMousePos.Y - mouse.oldMousePos.Y);
            if (windowYPos < _minimumWindowHeight + _padding)
            {
                windowYPos = _minimumWindowHeight + _padding;
            }
            if (windowYPos > _maximumWindowHeight)
            {
                windowYPos = _maximumWindowHeight;
            }
            _windowOffset = 0;
            InteractionArea = new Rectangle(InteractionArea.X, InteractionArea.Y, InteractionArea.Width, windowYPos);
            AlignContainerItems();
            int windowSizeWithItems = ((_itemSize) * _rows);
            int visibleWindowWithItems = windowSizeWithItems - _scissorRectangle.Height - _padding;
            int scrollbarlength = (int)(((float)(windowSizeWithItems - visibleWindowWithItems) / (float)windowSizeWithItems) * (float)100);
            SetScrollBarLength(scrollbarlength);
        }

        public void AlignScrollbar(Rectangle interactionArea)
        {
            _scrollBarInteractionArea = interactionArea;
            _scrollUpRectangle = new Rectangle(interactionArea.X, interactionArea.Y, interactionArea.Width, interactionArea.Width);
            _scrollDownRectangle = new Rectangle(interactionArea.X, (interactionArea.Y) + interactionArea.Height - interactionArea.Width, interactionArea.Width, interactionArea.Width);
            _scrollArea = new Rectangle(interactionArea.X, interactionArea.Y + _scrollUpRectangle.Height, interactionArea.Width, interactionArea.Height - _scrollUpRectangle.Height - _scrollDownRectangle.Height);
            _scrollBarSliderRectangle = _scrollArea;
        }

        public void DragScrollBar()
        {
            Vector2 mouseMovement = Globals.Mouse.newMousePos - Globals.Mouse.oldMousePos;
            int scrollBarPosition = _scrollBarSliderRectangle.Y;
            scrollBarPosition = scrollBarPosition + (int)(mouseMovement.Y);
            int minPos = _scrollArea.Y;
            int maxPos = _scrollArea.Y + _scrollArea.Height - _scrollBarSliderRectangle.Height;
            if(scrollBarPosition < minPos)
            {
                scrollBarPosition = minPos;
            }
            if (scrollBarPosition > maxPos)
            {
                scrollBarPosition = maxPos;
            }
            int totalAmount = (maxPos - minPos);
            int amount = (maxPos - minPos) - (scrollBarPosition - minPos);
            float percentage = ((float)totalAmount - (float)amount) / (float)totalAmount;
            int windowSizeWithItems = ((_itemSize) * _rows);
            int visibleWindowWithItems = windowSizeWithItems - _scissorRectangle.Height - _padding;
            _windowOffset = -1 * (int)(visibleWindowWithItems * percentage);
            _scrollBarSliderRectangle = new Rectangle(_scrollBarSliderRectangle.X, scrollBarPosition, _scrollBarSliderRectangle.Width, _scrollBarSliderRectangle.Height);

        }

        public void SetScrollBarLength(int scrollBarLengthPercentage)
        {
            if (scrollBarLengthPercentage > 100)
            {
                scrollBarLengthPercentage = 100;
            }
            else if (scrollBarLengthPercentage < 0)
            {
                scrollBarLengthPercentage = 0;
            }
            Rectangle currentRectangle = _scrollBarSliderRectangle;
            int percentage = (int)((float)_scrollArea.Height * (float)scrollBarLengthPercentage / 100);
            _scrollBarSliderRectangle = new Rectangle(currentRectangle.X, currentRectangle.Y, currentRectangle.Width, percentage);
        }
        public void SetScrollBarPosition(int scrollBarPositionPercentage)
        {
            if (scrollBarPositionPercentage > 100)
            {
                scrollBarPositionPercentage = 100;
            }
            else if (scrollBarPositionPercentage < 0)
            {
                scrollBarPositionPercentage = 0;
            }
            Rectangle currentRectangle = _scrollBarSliderRectangle;
            int amountOfArea = _scrollArea.Height - currentRectangle.Height;
            int percentage = _scrollArea.Y + (int)((float)amountOfArea * (float)scrollBarPositionPercentage / 100);
            _scrollBarSliderRectangle = new Rectangle(currentRectangle.X, percentage, currentRectangle.Width, currentRectangle.Height);
        }

        public override void Draw(TextureAtlas textureAtlas)
        {
            textureAtlas.Draw(InteractionArea, 1);
            textureAtlas.Draw(displayArea, 1);
            textureAtlas.Draw(_scrollUpRectangle, 1);
            textureAtlas.Draw(_scrollDownRectangle, 1);
            textureAtlas.Draw(_scrollBarSliderRectangle, 1);
            for (int i = 0; i < items.Length; i++)
            {
                Rectangle Orig = items[i];
                Rectangle recToDraw = new Rectangle(Orig.X, Orig.Y + _windowOffset, Orig.Width, Orig.Height);
                textureAtlas.Draw(recToDraw, 1, itemsHighlighted[i], _scissorRectangle);
            }
            textureAtlas.DrawString(Globals.DefaultFont, _displayText, new Vector2(0,0), Color.White);
        }
    }
}
