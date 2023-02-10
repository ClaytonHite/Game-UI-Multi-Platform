using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Ethereal.Client.Source.Engine
{
    public class TextureAtlas
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        private static Texture2D Texture { get; set; }
        private static Rectangle[] _sourceRectangles;
        private static SpriteBatch _spriteBatch;
        private RasterizerState _rasterizerState = new RasterizerState() { ScissorTestEnable = true };
        public TextureAtlas(Texture2D texture, int rows, int columns, SpriteBatch spriteBatch)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int counter = 0;
            _spriteBatch = spriteBatch;
            _sourceRectangles = new Rectangle[rows * columns];
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    //Populates the Source Rectangle then you can use the Texture associated and a destination rectangle when drawing
                    //Source Rectangle is the location of the sprite within the sprite sheet                    
                    _sourceRectangles[counter] = new Rectangle(width * column, height * row, width, height);
                    counter++;
                }
            }
        }
        public Rectangle GetSourceRectangle(int id)
        {
            return _sourceRectangles[id];
        }
        public void Draw(Rectangle destinationRectangle, int tileIndex)
        {
            _spriteBatch.Draw(Texture, destinationRectangle, _sourceRectangles[tileIndex], Color.White);
        }
        public void Draw(Rectangle destinationRectangle, int tileIndex, Color color)
        {
            _spriteBatch.Draw(Texture, destinationRectangle, _sourceRectangles[tileIndex], color);
        }
        public void Draw(Rectangle destinationRectangle, Rectangle sourceRectangle, Color color)
        {
            _spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }
        public void DrawString(SpriteFont font, string text, Vector2 position, Color color)
        {
            _spriteBatch.DrawString(font, text, position, color);
        }
        public void DrawString(SpriteFont font, string text, Vector2 position, Color color, Rectangle interactionRectangle)
        {
            _spriteBatch.End();
            //Set up the spritebatch to draw using scissoring (for text cropping)
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
                              null, null, _rasterizerState);
            //Copy the current scissor rect so we can restore it after
            Rectangle currentRect = _spriteBatch.GraphicsDevice.ScissorRectangle;
            //Set the current scissor rectangle
            _spriteBatch.GraphicsDevice.ScissorRectangle = interactionRectangle;
            //Draw the text at the top left of the scissor rectangle
            _spriteBatch.DrawString(font, text, position, color);
            //Reset scissor rectangle to the saved value
            _spriteBatch.GraphicsDevice.ScissorRectangle = currentRect;
            _spriteBatch.End();
            _spriteBatch.Begin();
        }
    }
}
