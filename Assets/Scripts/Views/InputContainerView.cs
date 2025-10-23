using System.Collections.Generic;
using Static.Enums;
using UnityEngine;

namespace Views
{
    public class InputContainerView : MonoBehaviour
    {
        [SerializeField] private List<InputView> _inputs;

        public InputView GetInput(ECurrencyType type)
        {
            foreach (var input in _inputs)
            {
                if(input.CurrencyType == type)
                    return input;
            }
            
            return null;
        }
    }
}