using QuestBase.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISettingSlider : MonoBehaviour
{
    [SerializeField]
    private UIText labelText;

    [SerializeField]
    private Slider slider;

    public void Init(string label, float defaultValue, UnityAction<float> onValueChanged)
    {
        this.labelText.SetTextDirectly(label);
        this.slider.value = defaultValue;
        this.slider.onValueChanged.AddListener(onValueChanged);
    }
}
