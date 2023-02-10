using Ethereal.Client.Source.Engine;
using Ethereal.Client.UI.UIPanels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethereal.Client.UI.UIObjects
{
    internal class InventoryPanelWindow : Panel
    {
        string _objectName;
        private Rectangle _interactionArea;
        private Vector2 _labelPosition;
        private bool IsClicked;
        private ButtonActions _action;
        private SpriteFont _font = Globals.DefaultFont;
        public InventoryPanelWindow(string objectName, ButtonActions buttonAction, int textureIndex) : base(objectName, textureIndex)
        {
            TextureIndex = textureIndex;
            TextureColor = Color.DarkGray;
            _objectName = objectName;
            _action = buttonAction;
        }
        public override void LoadContent()
        {
            Vector2 cornerOfRect = new Vector2(_interactionArea.Left, _interactionArea.Top);
            Vector2 dimensionsOfRect = new Vector2(_interactionArea.Width, _interactionArea.Height);
            Vector2 stringDimensions = Globals.DefaultFont.MeasureString(_objectName);
            Vector2 offsetFromCornerOfRect = (dimensionsOfRect - stringDimensions) / 2;
            _labelPosition = cornerOfRect + offsetFromCornerOfRect;
            base.LoadContent();
        }
        public override void Update()
        {
            bool isInButtonArea = _interactionArea.Contains(Globals.Mouse.newMousePos);
            if (isInButtonArea && !IsFocused)
            {
                IsFocused = true;
                TextureColor = Color.White;
            }
            else if (!isInButtonArea && IsFocused)
            {
                IsFocused = false;
                TextureColor = Color.DarkGray;
            }
            if (isInButtonArea && Globals.Mouse.LeftClick() && !IsClicked)
            {
                IsClicked = true;
                _interactionArea = new Rectangle(_interactionArea.X + 5, _interactionArea.Y + 5, _interactionArea.Width, _interactionArea.Height);
                SetInteractionArea(_interactionArea);
            }
            else if (!Globals.Mouse.LeftClickHold() && IsClicked)
            {
                _interactionArea = new Rectangle(_interactionArea.X - 5, _interactionArea.Y - 5, _interactionArea.Width, _interactionArea.Height);
                SetInteractionArea(_interactionArea);
            }
            base.Update();
        }

        public override void Draw(TextureAtlas textureAtlas)
        {
            base.Draw(textureAtlas);
            textureAtlas.Draw(_interactionArea, 1, TextureColor);
            textureAtlas.DrawString(_font, _objectName, _labelPosition, TextureColor);
        }

        public override void OnClick()
        {
            base.OnClick();
        }
        public override void IsSelected()
        {
            IsFocused = true;
            TextureColor = Color.White;
            InteractionTimer = 0;
        }
        public override void SetInteractionArea(Rectangle interactionArea)
        {
            _interactionArea = interactionArea;
            Vector2 cornerOfRect = new Vector2(_interactionArea.Left, _interactionArea.Top);
            Vector2 dimensionsOfRect = new Vector2(_interactionArea.Width, _interactionArea.Height);
            Vector2 stringDimensions = Globals.DefaultFont.MeasureString(_objectName);
            Vector2 offsetFromCornerOfRect = (dimensionsOfRect - stringDimensions) / 2;
            _labelPosition = cornerOfRect + offsetFromCornerOfRect;
        }
        public override Rectangle GetInteractionArea()
        {
            return _interactionArea;
        }
    }
}

