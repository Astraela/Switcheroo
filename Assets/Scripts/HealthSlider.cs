using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
        GameObject.FindObjectOfType<Health>().healthChange += onHealthChange;
    }

    void onHealthChange(float value){
        slider.value = value;
    }
}
