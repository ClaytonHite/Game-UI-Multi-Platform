using Ethereal.Client.Source.Engine;
using Ethereal.Client.UI.UIPanels;
using Ethereal.Client.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ethereal.Client.UI.UIObjects
{
    public class Button : Panel
    {
        string _objectName;
        private Rectangle _interactionArea;
        private Vector2 _labelPosition;
        private bool IsClicked;
        private SpriteFont _font = Globals.DefaultFont;
        private ButtonActions _action;
        private bool _hasLabel;
        public Button(string objectName, ButtonActions buttonAction, int textureIndex, bool hasLabel) : base(objectName, textureIndex)
        {
            TextureIndex = textureIndex;
            TextureColor = Color.DarkGray;
            _objectName = objectName;
            _action = buttonAction;
            _hasLabel = hasLabel;
        }
        public override void LoadContent()
        {
            if (_hasLabel)
            {
                Vector2 cornerOfRect = new Vector2(_interactionArea.Left, _interactionArea.Top);
                Vector2 dimensionsOfRect = new Vector2(_interactionArea.Width, _interactionArea.Height);
                Vector2 stringDimensions = Globals.DefaultFont.MeasureString(_objectName);
                Vector2 offsetFromCornerOfRect = (dimensionsOfRect - stringDimensions) / 2;
                _labelPosition = cornerOfRect + offsetFromCornerOfRect;
            }
            base.LoadContent();
        }
        public override void Update()
        {
            bool isInButtonArea = _interactionArea.Contains(Globals.Mouse.newMousePos);
            if (isInButtonArea)
            {
                TextureColor = Color.White;
            }
            else if (!isInButtonArea)
            {
                TextureColor = Color.DarkGray;
            }
            if (IsFocused && !Tabbed)
            {
                TextureColor = Color.White;
            }
            if (isInButtonArea && Globals.Mouse.LeftClick() && !IsClicked)
            {
                IsClicked = true;
                _interactionArea = new Rectangle(_interactionArea.X + 5, _interactionArea.Y + 5, _interactionArea.Width, _interactionArea.Height);
                SetInteractionArea(_interactionArea);
            }
            else if (Globals.Mouse.LeftClickRelease() && IsClicked)
            {
                IsClicked = false;
                _interactionArea = new Rectangle(_interactionArea.X - 5, _interactionArea.Y - 5, _interactionArea.Width, _interactionArea.Height);
                SetInteractionArea(_interactionArea);
            }
            base.Update();
        }

        public override void Draw(TextureAtlas textureAtlas)
        {
            base.Draw(textureAtlas);
            textureAtlas.Draw(_interactionArea, 1, TextureColor);
            if (_hasLabel)
            {
                textureAtlas.DrawString(_font, _objectName, _labelPosition, TextureColor);
            }
        }

        public override void OnEnterPress()
        {
            this.Action = _action;
            base.OnEnterPress();
        }

        public override void OnClick()
        {
            this.Action = _action;
            base.OnClick();
        }
        public override void IsSelected()
        {
            TextureColor = Color.White;
            InteractionTimer = 0;
        }
        public override void SetInteractionArea(Rectangle interactionArea)
        {
            _interactionArea = interactionArea;
            if (_hasLabel)
            {
                Vector2 cornerOfRect = new Vector2(_interactionArea.Left, _interactionArea.Top);
                Vector2 dimensionsOfRect = new Vector2(_interactionArea.Width, _interactionArea.Height);
                Vector2 stringDimensions = Globals.DefaultFont.MeasureString(_objectName);
                Vector2 offsetFromCornerOfRect = (dimensionsOfRect - stringDimensions) / 2;
                _labelPosition = cornerOfRect + offsetFromCornerOfRect;
            }
        }
        public override Rectangle GetInteractionArea()
        {
            return _interactionArea;
        }
    }
}
