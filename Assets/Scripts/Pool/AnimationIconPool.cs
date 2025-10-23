using Views;
using Zenject;

namespace Pool
{
    public class AnimationIconPool : MonoMemoryPool<AnimationIconView>
    {
        protected override void OnSpawned(AnimationIconView item)
        {
            item.gameObject.SetActive(true);
        }

        protected override void OnDespawned(AnimationIconView item)
        {
            item.gameObject.SetActive(false);
        }
    }
}