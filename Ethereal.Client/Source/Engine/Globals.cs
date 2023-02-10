using Ethereal.Client.Source.Engine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Ethereal.Client.Source.Engine
{
    public delegate void PassObject(object obj);
    public delegate void PassObjectAndReturn(object obj);
    public class Globals
    {
        public static int ScreenHeight;
        public static int ScreenWidth;
        public static SpriteFont DefaultSmallFont;
        public static SpriteFont DefaultFont;
        public static SpriteFont DefaultLargeFont;
        public static ContentManager Content;
        public static SpriteBatch SpriteBatch;
        public static ClientKeyboard Keyboard;
        public static ClientMouse Mouse;
        public static GameTime GameTime;

        public static float GetDistance(Vector2 pos, Vector2 target)
        {
            return (float)Math.Sqrt(Math.Pow(pos.X - target.X, 2) + Math.Pow(pos.Y - target.Y, 2));
        }
        /// <summary>
        /// Gets a distance from initial pos and destination position, if destination is closer than speed it just travels that distance, else it continues it's speed.
        /// </summary>
        /// <param name="focus"></param>
        /// Target's Position
        /// <param name="pos"></param>
        /// Initial Position
        /// <param name="speed"></param>
        /// This objects speed.
        /// <returns></returns>
        public static Vector2 RadialMovement(Vector2 focus, Vector2 pos, float speed)
        {
            float dist = Globals.GetDistance(pos, focus);

            if (dist <= speed)
            {
                return focus - pos;
            }
            else
            {
                return (focus - pos) * speed / dist;
            }
        }

        public static float RotateTowards(Vector2 Pos, Vector2 focus)
        {

            float h, sineTheta, angle;
            if (Pos.Y - focus.Y != 0)
            {
                h = (float)Math.Sqrt(Math.Pow(Pos.X - focus.X, 2) + Math.Pow(Pos.Y - focus.Y, 2));
                sineTheta = (float)(Math.Abs(Pos.Y - focus.Y) / h); //* ((item.Pos.Y-focus.Y)/(Math.Abs(item.Pos.Y-focus.Y))));
            }
            else
            {
                h = Pos.X - focus.X;
                sineTheta = 0;
            }

            angle = (float)Math.Asin(sineTheta);

            // Drawing diagonial lines here.
            //Quadrant 2
            if (Pos.X - focus.X > 0 && Pos.Y - focus.Y > 0)
            {
                angle = (float)(Math.PI * 3 / 2 + angle);
            }
            //Quadrant 3
            else if (Pos.X - focus.X > 0 && Pos.Y - focus.Y < 0)
            {
                angle = (float)(Math.PI * 3 / 2 - angle);
            }
            //Quadrant 1
            else if (Pos.X - focus.X < 0 && Pos.Y - focus.Y > 0)
            {
                angle = (float)(Math.PI / 2 - angle);
            }
            else if (Pos.X - focus.X < 0 && Pos.Y - focus.Y < 0)
            {
                angle = (float)(Math.PI / 2 + angle);
            }
            else if (Pos.X - focus.X > 0 && Pos.Y - focus.Y == 0)
            {
                angle = (float)Math.PI * 3 / 2;
            }
            else if (Pos.X - focus.X < 0 && Pos.Y - focus.Y == 0)
            {
                angle = (float)Math.PI / 2;
            }
            else if (Pos.X - focus.X == 0 && Pos.Y - focus.Y > 0)
            {
                angle = (float)0;
            }
            else if (Pos.X - focus.X == 0 && Pos.Y - focus.Y < 0)
            {
                angle = (float)Math.PI;
            }

            return angle;
        }
    }
}
