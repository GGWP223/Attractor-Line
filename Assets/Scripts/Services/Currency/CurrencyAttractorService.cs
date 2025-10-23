using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using Pool;
using Services.Currency.Context;
using Static.Enums;
using UnityEngine;
using Views;
using Random = UnityEngine.Random;

namespace Services.Currency
{
    public class CurrencyAttractorService : ICurrencyAttractorService
    {
        private readonly CurrencyContainerView _currencyView;
        private readonly InputContainerView _inputView;
        private readonly AnimationIconPool _pool;

        private readonly List<AnimationContext> _animations = new();

        public CurrencyAttractorService
        (
            CurrencyContainerView currencyView,
            InputContainerView inputView,
            AnimationIconPool pool
        )
        {
            _currencyView = currencyView;
            _inputView = inputView;
            _pool = pool;
        }

        public void PlayChargeEvent(ECurrencyType type, int count, Action OnAnyParticleComplete = null, Action OnLastParticleComplete = null)
        {
            PlayAnimation(type, count, OnAnyParticleComplete, OnLastParticleComplete);
        }

        public void ChangeEventTime(ECurrencyType type, float time)
        {
            if(time < 0)
                return;

            var contexts = GetContexts(type);
            
            foreach(var context in contexts)
                context.Tween.timeScale = time;
        }

        private void PlayAnimation(ECurrencyType type, int count, Action OnAnyParticleComplete = null, Action OnLastParticleComplete = null)
        {
            var currency = _currencyView.GetCurrencyView(type);
            var input = _inputView.GetInput(type);

            var start = input.GetButton.transform.position;
            var target = currency.CounterIcon.transform.position;

            var icons = Get(count);
            var sequence = DOTween.Sequence();

            var context = new AnimationContext(sequence, type);
            
            var delayStep = 0.05f;
            var i = 0;

            foreach (var icon in icons)
            {
                icon.Image.sprite = currency.CounterIcon.sprite;
                icon.Image.rectTransform.sizeDelta = currency.CounterIcon.rectTransform.sizeDelta;

                icon.transform.position = start;
                
                var tween = CalculateTween(type, icon, start, target)
                    .OnComplete(() => OnAnyParticleComplete?.Invoke());

                sequence.Insert(delayStep * i, tween);
                
                i++;
            }

            context.Icons.AddRange(icons);
            
            sequence.OnComplete(() =>
            {
                _animations.Remove(context);
                OnLastParticleComplete?.Invoke();
                Release(context.Icons);
            });

            _animations.Add(context);
        }

        private Tween CalculateTween(ECurrencyType type, AnimationIconView icon, Vector3 start, Vector3 target)
        {
            var input = _inputView.GetInput(type);
            var animation = input.AnimationType;

            Vector3[] path;

            switch (animation.value)
            {
                case 0:
                    path = new[] { start, target };
                    break;

                case 1:
                {
                    var steps = 50;
                    var amplitude = 200f;
                    var frequency = 3f; 
                    
                    path = new Vector3[steps + 1];

                    for (var i = 0; i <= steps; i++)
                    {
                        var t = i / (float)steps;
                        var point = Vector3.Lerp(start, target, t);
                        
                        point.x += Mathf.Cos(t * Mathf.PI * frequency) * amplitude * (1f - t);
                        path[i] = point;
                    }
                } break;

                case 2:
                {
                    var offset = new Vector3(Random.Range(-100f, 100f), Random.Range(50f, 200f), 0f);
                    var median = Vector3.Lerp(start, target, 0.5f) + offset;
                    
                    path = new[] { start, median, target };
                } break;

                default:
                    path = new[] { start, target };
                    break;
            }

            return icon.transform.DOPath(path, 1f, PathType.CatmullRom);
        }


        private List<AnimationIconView> Get(int count)
        {
            var list = new List<AnimationIconView>(count);
            
            for (int i = 0; i < count; i++)
                list.Add(_pool.Spawn());
            
            return list;
        }

        private void Release(List<AnimationIconView> icons)
        {
            foreach (var i in icons)
                _pool.Despawn(i);
        }

        private List<AnimationContext> GetContexts(ECurrencyType type)
        {
            List<AnimationContext> buffer = new();
            
            foreach (var animation in _animations)
            {
                if(animation.Type == type)
                    buffer.Add(animation);
            }
            
            return buffer;
        }
    }
}