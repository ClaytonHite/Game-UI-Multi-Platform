using Ethereal.GameLayer.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Ethereal.Client.Source.Engine
{
    public class Projectile2DSprite : Animated2DSprite
    {
        public bool Done;
        public float Speed;
        public BaseCreatureModel Owner;
        public Vector2 Direction;
        public Timer Timer;

        public Projectile2DSprite(string path, Vector2 position, Vector2 dimensions, SpriteEffects effects) : base(path, position, dimensions, effects)
        {
        }

        public Projectile2DSprite(string path, Vector2 position, Vector2 dimensions, BaseCreatureModel owner, Vector2 Target, SpriteEffects effects)
            : base(path, position, dimensions, effects)
        {
            Done = false;
            Speed = 12.0f;
            //Owner = owner;
            //Direction = Target - Owner.Position;
            Direction.Normalize();
            Timer = new Timer(1200);// 1.2 seconds lasting
            Rotation = Globals.RotateTowards(position, new Vector2(Target.X, Target.Y));
        }

        public virtual void Update(Vector2 offset, List<BaseCreatureModel> units)
        {
            Position += Direction * Speed;
            Timer.UpdateTimer();
            if (Timer.Test())
            {
                Done = true;// stops projectile 
            }
            if (IsColliding(units))
            {
                Done = true;// stops projectile if hit something
            }
            base.Update(offset);
        }

        public virtual bool IsColliding(List<BaseCreatureModel> units)
        {
            for (int i = 0; i < units.Count; i++)
            {
                if (Globals.GetDistance(Position, units[i].Sprite.Position) < units[i].HitDistance)
                {
                    units[i].GetHit();
                    return true;
                }
            }
            return false;
        }

        public override void Draw(Vector2 offset)
        {
            base.Draw(offset);
        }
    }
}
