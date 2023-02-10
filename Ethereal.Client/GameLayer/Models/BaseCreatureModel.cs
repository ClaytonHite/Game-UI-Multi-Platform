using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ethereal.Client.Source.Engine;
using Ethereal.GameLayer.Models.Interfaces;

namespace Ethereal.GameLayer.Models
{
    public class BaseCreatureModel : IEntityModel
    {
        public uint ID;
        public int Health = 0;
        public int MaxHealth = 100;
        public float HitDistance;
        public Animated2DSprite Sprite;
        public float HealthPercent
        {
            get
            {
                return 1f * Health / MaxHealth;
            }
        }
        public int Mana = 0;
        public int MaxMana = 100;
        public float ManaPercent
        {
            get
            {
                return 1f * Health / MaxHealth;
            }
        }
        public int Speed = 0;
        public enum Direction
        {
            North,
            East,
            West,
            South
        }
        public Direction FacingDirection = Direction.South;
        public String Name = "";
        /// <summary>
        /// If the other player is a player, is he online (for VIP?)
        /// </summary>
        public bool Online = false;
        public OutfitModel Outfit = new OutfitModel();
        public LightModel Light;
        public PartyShieldModel Shield;

        public BaseCreatureModel(uint CreatureID)
        {
            ID = CreatureID;
        }

        override public int GetHashCode()
        {
            return ID.GetHashCode();
        }

        /*public override int Order
        {
            get
            {
                return 4;
            }
        }*/

        public void Move(PositionModel FromPosition, PositionModel ToPosition)
        {
            if (ToPosition.X < FromPosition.X)
                FacingDirection = Direction.West;
            else if (ToPosition.X > FromPosition.X)
                FacingDirection = Direction.East;
            else if (ToPosition.Y < FromPosition.Y)
                FacingDirection = Direction.North;
            else if (ToPosition.Y > FromPosition.Y)
                FacingDirection = Direction.South;
        }

        public virtual void GetHit()
        {

        }
    }
}
