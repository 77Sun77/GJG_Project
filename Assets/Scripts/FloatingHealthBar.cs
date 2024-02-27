using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    public void UpdateValue(float current, float max)
    {
        slider.value = current / max;
    }
}
