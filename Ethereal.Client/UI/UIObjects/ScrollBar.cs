using Ethereal.Client.Source.Engine;
using Ethereal.Client.UI.UIPanels;
using Microsoft.Xna.Framework;

namespace Ethereal.Client.UI.UIObjects
{
    public class ScrollBar : UIObject
    {
        private Button _scrollUp;
        private Button _scrollDown;
        private Button _scrollBarSlider;
        private Rectangle _interactionArea;
        private Rectangle _scrollArea;
        private int _scrollBarPosition = 0;
        private int _scrollBarHeight = 0;
        public ScrollBar(string objectName , Rectangle interactionArea) : base(objectName)
        {
            _scrollUp = new Button("ScrollUp", ButtonActions.None, 1, false);
            _scrollDown = new Button("_scrollDown", ButtonActions.None, 1, false);
            _scrollBarSlider = new Button("_scrollBarSlider", ButtonActions.None, 1, false);
            _interactionArea = new Rectangle();
            AlignScrollbar(interactionArea);
        }
        public void AlignScrollbar(Rectangle interactionArea)
        {
            _interactionArea = interactionArea;
            Rectangle _scrollUpRec = new Rectangle(interactionArea.X, interactionArea.Y, interactionArea.Width, interactionArea.Height / 10);
            _scrollUp.SetInteractionArea(_scrollUpRec);
            Rectangle _scrollDownRec = new Rectangle(interactionArea.X, (interactionArea.Y) + interactionArea.Height - (interactionArea.Height / 10), interactionArea.Width, interactionArea.Height / 10);
            _scrollDown.SetInteractionArea(_scrollDownRec);
            _scrollArea = new Rectangle(interactionArea.X, interactionArea.Y + _scrollUpRec.Height, interactionArea.Width, interactionArea.Height - _scrollUpRec.Height - _scrollDownRec.Height);
            _scrollBarSlider.SetInteractionArea(_scrollArea);
        }
        public override void SetInteractionArea(Rectangle interactionArea)
        {
            _interactionArea = interactionArea;
            Rectangle _scrollUpRec = new Rectangle(interactionArea.X, interactionArea.Y, interactionArea.Width, interactionArea.Height / 10);
            _scrollUp.SetInteractionArea(_scrollUpRec);
            Rectangle _scrollDownRec = new Rectangle(interactionArea.X, (interactionArea.Y) + interactionArea.Height - (interactionArea.Height / 10), interactionArea.Width, interactionArea.Height / 10);
            _scrollDown.SetInteractionArea(_scrollDownRec);
            _scrollArea = new Rectangle(interactionArea.X, interactionArea.Y + _scrollUpRec.Height, interactionArea.Width, interactionArea.Height - _scrollUpRec.Height - _scrollDownRec.Height);
            _scrollBarSlider.SetInteractionArea(_scrollArea);
        }

        public void MoveScrollBar(int direction)
        {
            _scrollBarPosition += direction;
            SetScrollBarPosition(_scrollBarPosition);
        }

        public void ScrollBarSize(int direction)
        {

        }

        public void SetScrollBarLength(int scrollBarLengthPercentage)
        {
            if(scrollBarLengthPercentage > 100)
            {
                scrollBarLengthPercentage = 100;
            }
            else if(scrollBarLengthPercentage < 0)
            {
                scrollBarLengthPercentage = 0;
            }
            Rectangle currentRectangle = _scrollBarSlider.GetInteractionArea();
            int percentage = (int)((float)_scrollArea.Height * (float)scrollBarLengthPercentage / 100);
            _scrollBarSlider.SetInteractionArea(new Rectangle(currentRectangle.X, currentRectangle.Y, currentRectangle.Width, percentage));
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
            Rectangle currentRectangle = _scrollBarSlider.GetInteractionArea();
            int amountOfArea = _scrollArea.Height - currentRectangle.Height;
            int percentage = _scrollArea.Y + (int)((float)amountOfArea * (float)scrollBarPositionPercentage / 100);
            _scrollBarSlider.SetInteractionArea(new Rectangle(currentRectangle.X, percentage, currentRectangle.Width, currentRectangle.Height));
        }

        public override void Draw(TextureAtlas textureAtlas)
        {
            base.Draw(textureAtlas);
            //textureAtlas.Draw(_interactionArea, 1, TextureColor);
            _scrollUp.Draw(textureAtlas);
            _scrollBarSlider.Draw(textureAtlas);
            _scrollDown.Draw(textureAtlas);
        }
    }
}
