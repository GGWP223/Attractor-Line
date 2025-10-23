using Static.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class CurrencyView : MonoBehaviour
    {
        [field: SerializeField] public ECurrencyType CurrencyType { get; private set; }
        [field: SerializeField] public TextMeshProUGUI CounterText { get; private set; }
        [field: SerializeField] public Image CounterIcon { get; private set; }
    } 
}