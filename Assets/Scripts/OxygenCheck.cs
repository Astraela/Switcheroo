using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenCheck : MonoBehaviour
{
    public OxygenObject Slider;
    public float oxygen = 20;
    float currentOxygen = 20;
    GameObject[] waters;
    // Start is called before the first frame update
    void Start()
    {
        if(!transform.IsChildOf(GameObject.FindObjectOfType<PlayerInput>().transform)) Destroy(this);
        waters = GameObject.FindGameObjectsWithTag("Water");
        Slider = GameObject.FindObjectOfType<OxygenObject>();
    } 

    void Death(){
        GameObject.FindObjectOfType<PlayerInput>().Die();
    }

    void Update()
    {
        foreach(GameObject water in waters){
            if(water.GetComponent<Collider>().bounds.Contains(transform.position)){
                Slider.SetActive(true);
                currentOxygen -= Time.deltaTime;
                Slider.SetValue(currentOxygen/oxygen);
                if(currentOxygen <= 0) Death();
                return;
            }
        }
        Slider.SetActive(false);
        currentOxygen = oxygen;
    }
}
