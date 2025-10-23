using UnityEngine;
using Views;
using Zenject;

namespace Installers.Game
{
    public class ViewInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Bind<CurrencyContainerView>();
            Bind<InputContainerView>();
        }

        private void Bind<T>() where T : MonoBehaviour
        {
            Container
                .Bind<T>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}
