using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaterCheck : MonoBehaviour
{
    public Image waterOverlay;
    GameObject[] waters;
    // Start is called before the first frame update
    void Start()
    {
        waters = GameObject.FindGameObjectsWithTag("Water");
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject water in waters){
            if(water.GetComponent<Collider>().bounds.Contains(transform.position)){
                waterOverlay.enabled = true;
                return;
            }
        }
        waterOverlay.enabled = false;
    }
}
