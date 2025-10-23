using System.Collections.Generic;
using Static.Enums;
using UnityEngine;

namespace Views
{
    public class CurrencyContainerView : MonoBehaviour
    {
        [SerializeField] private List<CurrencyView> _currencies = new();

        public CurrencyView GetCurrencyView(ECurrencyType type)
        {
            foreach (var item in _currencies)
            {
                if (item.CurrencyType == type)
                    return item;
            }
            
            return null;
        }
    }
}