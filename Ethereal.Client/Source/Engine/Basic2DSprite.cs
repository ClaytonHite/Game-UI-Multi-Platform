using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ethereal.Client.Source.Engine
{
    public class Basic2DSprite
    {
        public float Rotation;
        public Vector2 Position;
        public Vector2 Dimensions;
        public Texture2D Texture;
        public SpriteEffects Effects;

        public Basic2DSprite(string path, Vector2 position, Vector2 dimensions, SpriteEffects effects)
        {
            Position = position;
            Dimensions = dimensions;
            Texture = Globals.Content.Load<Texture2D>(path);
            Effects = effects;
        }
        public virtual void Update(Vector2 offset)
        {

        }
        public virtual void Draw(Vector2 offset)
        {
            Globals.SpriteBatch.Draw(Texture, new Rectangle((int)(Position.X + offset.X), (int)(Position.Y + offset.Y), (int)Dimensions.X, (int)Dimensions.Y), null, Color.White, Rotation, new Vector2(Texture.Bounds.Width / 2, Texture.Bounds.Height / 2), Effects, 0);
        }
        public virtual void Draw(Vector2 offset, Vector2 origin)
        {
            Globals.SpriteBatch.Draw(Texture, new Rectangle((int)(Position.X + offset.X), (int)(Position.Y + offset.Y), (int)Dimensions.X, (int)Dimensions.Y), null, Color.White, Rotation, new Vector2(origin.X, origin.Y), Effects, 0);
        }
    }
}
