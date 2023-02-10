using Ethereal.Client.Source.Engine;
using Ethereal.Client.UI.UIPanels;
using Ethereal.Client.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ethereal.Client.UI.UIObjects
{
    public class InputTextBox : UIObject
    {
        public bool PasswordBox { get; set; }
        public string _objectName;
        public bool HasLabel { get; set; }
        private string _placeholder { get; set; }
        private Rectangle _interactionArea;
        private Vector2 _textPosition;
        private Vector2 _labelPosition;
        private SpriteFont _font = Globals.DefaultFont;
        private ButtonActions? _action;

        public InputTextBox(string objectName, int textBoxTexture) : base(objectName)
        {
            TextureIndex = textBoxTexture;
            _objectName = objectName;
            TextureColor = Color.LightGray;
        }
        public InputTextBox(string objectName, int textBoxTexture, string placeholder) : base(objectName)
        {
            TextureIndex = textBoxTexture;
            _objectName = objectName;
            TextureColor = Color.LightGray;
            HasLabel = true;
            _placeholder = placeholder;
        }
        public InputTextBox(string objectName, int textBoxTexture, string placeholder, ButtonActions action) : base(objectName)
        {
            TextureIndex = textBoxTexture;
            _objectName = objectName;
            TextureColor = Color.LightGray;
            HasLabel = true;
            _placeholder = placeholder;
            _action = action;
        }

        public override void LoadContent()
        {
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
            if (isInButtonArea && Globals.Mouse.LeftClick())
            {
                IsFocused = true;
                TextureColor = Color.White;
            }
            else if (!isInButtonArea && Globals.Mouse.LeftClickRelease())
            {
                IsFocused = false;
                TextureColor = Color.LightGray;
            }
            if (IsFocused)
            {
                if (lengthCounter != Text.Length)
                {
                    lengthCounter = Text.Length;
                    SetInteractionArea(_interactionArea);
                }
            }
            base.Update();
        }
        public override void IsSelected()
        {
            IsFocused = true;
            TextureColor = Color.White;
            InteractionTimer = 0;
        }

        public override void Draw(TextureAtlas textureAtlas)
        {
            textureAtlas.Draw(_interactionArea, 1, TextureColor);
            if (PasswordBox)
            {
                textureAtlas.DrawString(_font, new string('*', Text.Length), _textPosition, TextureColor);
                if (HasLabel && Text.Length == 0)
                {
                    textureAtlas.DrawString(_font, _placeholder, _labelPosition, Color.DarkGray);
                }
            }
            else
            {
                if (HasLabel && Text.Length == 0)
                {
                    textureAtlas.DrawString(_font, _placeholder, _labelPosition, Color.DarkGray);
                }
                else
                {
                    textureAtlas.DrawString(_font, Text, _textPosition, TextureColor, new Rectangle(_interactionArea.X + 10, _interactionArea.Y, _interactionArea.Width - 10, _interactionArea.Height));
                }
            }
        }

        public override void OnClick()
        {
            base.OnClick();
        }

        public override void OnEnterPress()
        {
            if(_action != null)
            {
                this.Action = _action;
            }
            base.OnEnterPress();
        }

        public override Rectangle GetInteractionArea()
        {
            return _interactionArea;
        }
        public override void SetInteractionArea(Rectangle interactionArea)
        {
            _interactionArea = interactionArea;
            Vector2 cornerOfRect = new Vector2(_interactionArea.Left, _interactionArea.Top);
            Vector2 dimensionsOfRect = new Vector2(_interactionArea.Width, _interactionArea.Height);
            Vector2 stringDimensions = new Vector2();
            if (PasswordBox)
            {
                stringDimensions = Globals.DefaultFont.MeasureString(new string('*', Text.Length));
            }
            else
            {
                stringDimensions = Globals.DefaultFont.MeasureString(Text);
            }
            bool isBigger = false;
            if (stringDimensions.X > _interactionArea.Width - 20)
            {
                isBigger = true;
                cornerOfRect.X = cornerOfRect.X + _interactionArea.Width - stringDimensions.X - 20;
            }
            Vector2 offsetFromCornerOfRect = (dimensionsOfRect - stringDimensions) / 2;
            if (CenteredText)
            {
                _textPosition = cornerOfRect + offsetFromCornerOfRect;
                if(isBigger)
                {
                    _textPosition = new Vector2(cornerOfRect.X + 10, cornerOfRect.Y + (_interactionArea.Height - stringDimensions.Y) / 2);
                }
            }
            else
            {
                _textPosition = new Vector2(cornerOfRect.X + 10, cornerOfRect.Y +(_interactionArea.Height - stringDimensions.Y)/2);
            }
            if (HasLabel)
            {
                cornerOfRect = new Vector2(_interactionArea.Left, _interactionArea.Top);
                dimensionsOfRect = new Vector2(_interactionArea.Width, _interactionArea.Height);
                stringDimensions = Globals.DefaultFont.MeasureString(_placeholder);
                offsetFromCornerOfRect = (dimensionsOfRect - stringDimensions) / 2;
                _labelPosition = cornerOfRect + offsetFromCornerOfRect;
            }
        }
    }

}
