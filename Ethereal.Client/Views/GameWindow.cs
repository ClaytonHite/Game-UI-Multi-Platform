using Ethereal.Client.Source.Engine;
using Ethereal.Client.Source.GamePlay;
using Ethereal.GameLayer.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Ethereal.Client.Views
{
    public class GameWindow : BaseWindow
    {
        public List<Projectile2DSprite> projectiles = new List<Projectile2DSprite>();
        public List<BaseCreatureModel> Monsters = new List<BaseCreatureModel>();
        public GameWindow(SpriteBatch _spriteBatch, ContentManager _content) : base(_spriteBatch, _content)
        {
            GameGlobals.PassProjectile = AddProjectile;
            GameGlobals.PassMonster = AddMonster;
        }

        public override void Initialize()
        {

        }

        public override void LoadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime)
        {

        }
        public virtual void AddMonster(object obj)
        {
            Monsters.Add((BaseCreatureModel)obj);
        }
        public virtual void AddProjectile(object obj)
        {
            projectiles.Add((Projectile2DSprite)obj);
        }
    }
}
