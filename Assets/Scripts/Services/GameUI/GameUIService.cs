using System;
using Services.Currency;
using Static.Enums;
using UniRx;
using Views;
using Zenject;
using Random = UnityEngine.Random;

namespace Services.GameUI
{
    public class GameUIService : IGameUIService, IInitializable
    {
        private readonly ICurrencyService _currencyService;
        private readonly ICurrencyAttractorService _attractorService;
        
        private readonly InputContainerView _inputContainerView;

        public GameUIService
        (
            ICurrencyService currencyService,
            ICurrencyAttractorService attractorService,
            
            InputContainerView inputContainerView
        )
        {
            _currencyService = currencyService;
            _attractorService = attractorService;
            _inputContainerView = inputContainerView;
        }

        public void Initialize()
        {
            foreach (ECurrencyType type in Enum.GetValues(typeof(ECurrencyType)))
            {
                var view = _inputContainerView.GetInput(type);
                
                if(view is null)
                    continue;
                
                view.GetButton.onClick
                    .AsObservable()
                    .Subscribe(_ =>
                    {
                        _currencyService.AddCurrency(type, Random.Range(100, 1000));
                        _attractorService.ChangeEventTime(type, view.TimeSlider.value);
                    })
                    .AddTo(_inputContainerView);
                
                view.TimeSlider.onValueChanged
                    .AsObservable()
                    .Subscribe(value => _attractorService.ChangeEventTime(type, value))
                    .AddTo(_inputContainerView);
            }
        }
    }
}