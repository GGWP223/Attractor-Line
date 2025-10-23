using System;
using System.Collections.Generic;
using Services.Currency.Context;
using Static.Enums;
using UnityEngine;
using Views;
using Zenject;
using Random = UnityEngine.Random;

namespace Services.Currency
{
    public class CurrencyService : ICurrencyService, IInitializable
    {
        private readonly ICurrencyAttractorService _attractor;
        private readonly CurrencyContainerView _view;

        private readonly List<CurrencyContext> _contexts = new();

        public CurrencyService
        (
            ICurrencyAttractorService attractor,
            CurrencyContainerView view
        )
        {
            _attractor = attractor;
            _view = view;
        }

        public void Initialize()
        {
            LoadContexts();
        }

        public void AddCurrency(ECurrencyType type, int amount)
        {
            var context = GetCurrencyContext(type);
            
            if (context is null || amount <= 0)
                return;
            
            var particlesCount = Random.Range(5, 15);
            var balancePiece = amount / particlesCount;
            var remainder = amount - balancePiece * (particlesCount - 1); 

            var currentParticle = 0;

            _attractor.PlayChargeEvent(type, particlesCount, () =>
            {
                var add = (currentParticle < particlesCount - 1) ? balancePiece : remainder;
                
                context.Amount += add;
                UpdateText(type);
                currentParticle++;
            });
        }

        private void LoadContexts()
        {
            foreach (ECurrencyType type in Enum.GetValues(typeof(ECurrencyType)))
            {
                var context = new CurrencyContext(type)
                {
                    Amount = 0
                };
                
                _contexts.Add(context);
                UpdateText(type);
            }
        }

        private void UpdateText(ECurrencyType type)
        {
            var view = _view.GetCurrencyView(type);
            var context = GetCurrencyContext(type);
            
            view.CounterText.text = context.Amount.ToString();
        }

        private CurrencyContext GetCurrencyContext(ECurrencyType type)
        {
            foreach (var context in _contexts)
            {
                if(context.CurrencyType == type)
                    return context;
            }

            return null;
        }
    }
}