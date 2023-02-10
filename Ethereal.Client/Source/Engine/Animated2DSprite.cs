using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ethereal.Client.Source.Engine
{
    public class Animated2DSprite
    {
        public float Rotation;
        public Vector2 Position;
        public Vector2 Dimensions;
        public Texture2D Texture;
        public SpriteEffects Effects;
        float _animationTimer;
        int _animationAmount;
        int _animationIndex;
        int _animationSpeed;
        Rectangle[] _sourceRectangles;
        Rectangle _destinationRectangle;

        public Animated2DSprite(string path, Vector2 position, Vector2 dimensions, SpriteEffects effects)
        {
            Position = position;
            Dimensions = dimensions;
            Texture = Globals.Content.Load<Texture2D>(path);
            Effects = effects;
            _animationTimer = 0;
            _animationAmount = Texture.Width / 32;
            _animationIndex = 0;
            _animationSpeed = 150;
            _sourceRectangles = new Rectangle[_animationAmount];
            for (int i = 0; i < _sourceRectangles.Length; i++)
            {
                _sourceRectangles[i] = new Rectangle(i * (int)dimensions.X, 0, (int)dimensions.X, (int)dimensions.Y);
            }
        }
        public virtual void Update(Vector2 offset)
        {
            // Check if the timer has exceeded the threshold.
            if (_animationTimer > _animationSpeed)
            {
                if (_animationIndex < _animationAmount - 1)
                {
                    _animationIndex++;
                }
                else
                {
                    _animationIndex = 0;
                }
                _animationTimer = 0;
            }
            // If the timer has not reached the threshold, then add the milliseconds that have past since the last Update() to the timer.
            else
            {
                _animationTimer += (float)Globals.GameTime.ElapsedGameTime.TotalMilliseconds;
            }
            _destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)Dimensions.X, (int)Dimensions.Y);
        }
        public virtual void Draw(Vector2 offset)
        {
            Globals.SpriteBatch.Draw(Texture, _destinationRectangle, _sourceRectangles[_animationIndex], Color.White, Rotation, new Vector2(Dimensions.X / 2, Dimensions.Y / 2), Effects, 0);
        }
        public virtual void Draw(Vector2 offset, Vector2 origin)
        {
            Globals.SpriteBatch.Draw(Texture, _destinationRectangle, _sourceRectangles[_animationIndex], Color.White, Rotation, new Vector2(origin.X, origin.Y), Effects, 0);
        }
    }
}
