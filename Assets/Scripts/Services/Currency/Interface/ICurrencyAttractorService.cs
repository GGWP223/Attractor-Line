using System;
using Static.Enums;

namespace Services.Currency
{
    public interface ICurrencyAttractorService
    {
        public void PlayChargeEvent(ECurrencyType type, int count, Action OnAnyParticleComplete = null, Action OnLastParticleComplete = null);
        
        public void ChangeEventTime(ECurrencyType type, float time);
    }
}