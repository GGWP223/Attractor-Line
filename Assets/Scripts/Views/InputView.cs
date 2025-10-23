using Static.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class InputView : MonoBehaviour
    {
        [field: SerializeField] public ECurrencyType CurrencyType { get; private set; }
        [field: SerializeField] public Button GetButton { get; private set; }
        [field: SerializeField] public Slider TimeSlider { get; private set; }
        [field: SerializeField] public TMP_Dropdown AnimationType { get; private set; }
    }
}