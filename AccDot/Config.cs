using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;
using UnityEngine;

namespace AccDot
{
    public class Config
    {
        [UseConverter(typeof(HexColorConverter))]
        public virtual Color Color { get; set; } = Color.white;
        public virtual float Scale { get; set; } = 0.1f;
        public virtual float Distance { get; set; } = 1f;
    }
}