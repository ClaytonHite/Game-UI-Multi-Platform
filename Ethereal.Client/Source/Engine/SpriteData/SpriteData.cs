using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethereal.Client.Source.Engine.SpriteData
{
    internal class SpriteData
    {
        public Rectangle SpriteLocation { get; set; }
        public Vector2 SpriteSize { get; set; }
        public int ItemId { get; set; }
        public int ActionId { get; set; }
        public int UniqueId { get; set; }
        public string Article { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public bool IsAnimated { get; set; }
        public int AnimationCount { get; set; }
        public string WrittenBy { get; set; }
        public DateTime WrittenDate { get; set; }
        public List<Properties> Flags { get; set; }
        public enum Properties
        {
            IsWalkable,
            IsDoor,
            IsContainer,
            IsUsable,
            IsAnimated,
            IsDecayable,
            IsWriteable,
            IsMoveable,
            IsPickupable,
            IsRotatable,
            IsStackable
        }
    }
}
