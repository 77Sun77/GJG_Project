using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Camera camera;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    public void UpdateValue(float current, float max)
    {
        slider.value = current / max;
    }

    private void Update()
    {
        transform.position = target.position + offset;
        transform.rotation = camera.transform.rotation;
    }
}
