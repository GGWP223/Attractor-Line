using System.Collections.Generic;
using DG.Tweening;
using Static.Enums;
using Views;

namespace Services.Currency.Context
{
    public class AnimationContext
    {
        public ECurrencyType Type { get; }
        public Tween Tween { get; }
        public List<AnimationIconView> Icons { get; }

        public AnimationContext(Tween tween, ECurrencyType type)
        {
            Tween = tween;
            Type = type;
            Icons = new List<AnimationIconView>();
        }
    }
}