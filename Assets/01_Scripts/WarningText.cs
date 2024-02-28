using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class WarningText : MonoBehaviour
{

    public TextMeshProUGUI tmp;
    public void ActiveTrue(string text)
    {
        gameObject.SetActive(true);
        tmp.text = text;

    }
    void OnEnable()
    {
        Invoke("ActiveFalse", 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ActiveFalse()
    {
        gameObject.SetActive(false);
    }
}
