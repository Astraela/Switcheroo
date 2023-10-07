using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenObject : MonoBehaviour
{
    public GameObject child;

    public void SetActive(bool boolean){
        child.SetActive(boolean);
    }

    public void SetValue(float value){
        child.GetComponent<Image>().fillAmount = value;
    }
}
