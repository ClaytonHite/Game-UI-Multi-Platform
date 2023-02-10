using Ethereal.Client.Source.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Ethereal.Client.UI.UIPanels.PanelTypes
{
    public class BookPanel
    {
        SpriteFont _font;
        string _text;
        Rectangle _borderRectangle;
        Rectangle _textboxRectangle;
        private Vector2 _textboxPosition = Vector2.Zero;
        public void LoadContent()
        {
            _borderRectangle = new Rectangle(100, 100, 500, 500);
            _textboxRectangle = new Rectangle(105, 105, 490, 490);
            _font = Globals.DefaultFont;
            _text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Vulputate mi sit amet mauris. Nibh cras pulvinar mattis nunc sed. Semper viverra nam libero justo laoreet. Magna etiam tempor orci eu lobortis elementum nibh tellus molestie. Ipsum dolor sit amet consectetur adipiscing elit ut. Tincidunt tortor aliquam nulla facilisi cras fermentum. Dictum non consectetur a erat nam at. Non tellus orci ac auctor augue mauris augue. Magna eget est lorem ipsum dolor sit. Non arcu risus quis varius quam quisque id diam. Nulla pharetra diam sit amet nisl suscipit adipiscing bibendum est. Vitae aliquet nec ullamcorper sit. Sagittis nisl rhoncus mattis rhoncus urna. Mauris pharetra et ultrices neque.\r\n\r\nMassa ultricies mi quis hendrerit dolor magna eget est. Scelerisque viverra mauris in aliquam sem. Ipsum faucibus vitae aliquet nec ullamcorper sit amet risus nullam. Gravida arcu ac tortor dignissim convallis. Risus pretium quam vulputate dignissim suspendisse in est ante in. Adipiscing commodo elit at imperdiet dui accumsan sit amet. Vitae suscipit tellus mauris a diam maecenas sed enim ut. Ornare lectus sit amet est placerat in. Tincidunt augue interdum velit euismod in. Vitae suscipit tellus mauris a diam maecenas sed enim ut. Lacus sed turpis tincidunt id. Vitae turpis massa sed elementum tempus egestas sed sed. Vehicula ipsum a arcu cursus vitae. Vulputate sapien nec sagittis aliquam.\r\n\r\nSollicitudin ac orci phasellus egestas tellus rutrum tellus pellentesque. Blandit aliquam etiam erat velit. Nulla aliquet enim tortor at auctor urna nunc. Sapien faucibus et molestie ac feugiat. Arcu non odio euismod lacinia. Auctor eu augue ut lectus arcu bibendum at varius. Facilisis volutpat est velit egestas. Sit amet venenatis urna cursus eget nunc scelerisque viverra mauris. Cursus sit amet dictum sit amet justo donec. Egestas erat imperdiet sed euismod nisi. Ut consequat semper viverra nam libero justo laoreet. Ac tortor vitae purus faucibus ornare suspendisse.";
        }
        public void Update()
        {

        }
        private string parseText(string text)
        {
            string line = string.Empty;
            string returnString = string.Empty;
            string[] wordArray = text.Split(' ');

            for (int i = 0; i < wordArray.Length; i++)
            {
                if (_font.MeasureString(line + wordArray[i]).Length() > _textboxRectangle.Width)
                {
                    returnString = returnString + line + '\n';
                    line = string.Empty;
                }

                line = line + wordArray[i] + ' ';
            }
            return returnString + line;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawString(Globals.DefaultFont, line, textPostion, Color.White);
        }
    }
}
