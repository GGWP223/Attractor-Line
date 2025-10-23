using Pool;
using Services.Currency;
using Services.GameUI;
using UnityEngine;
using Views;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private AnimationIconView _prefab;
        [SerializeField]private Transform _parent;
        
        public override void InstallBindings()
        {
            Application.targetFrameRate = 120;
            
            Container.BindMemoryPool<AnimationIconView, AnimationIconPool>()
                .WithInitialSize(10)
                .FromComponentInNewPrefab(_prefab)
                .UnderTransform(_parent);
            
            Bind<CurrencyAttractorService>();
            Bind<CurrencyService>();
            
            Bind<GameUIService>();
        }

        private void Bind<TImplementation>() where TImplementation : class
        {
            Container.BindInterfacesTo<TImplementation>().AsSingle();
        }
    }
}