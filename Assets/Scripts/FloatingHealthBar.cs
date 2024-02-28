using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private Camera m_camera;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    private void Start()
    {
        m_camera = Camera.main;
    }

    public void UpdateValue(float current, float max)
    {
        slider.value = current / max;
    }

    private void Update()
    {
        transform.position = target.position + offset;
        transform.rotation = m_camera.transform.rotation;
    }
}
