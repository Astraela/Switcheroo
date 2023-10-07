using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bread : MonoBehaviour
{

    Vector3 origin;
    void Start(){
        origin = transform.position;
    }

    void Update(){
        transform.Rotate(Vector3.up*Time.deltaTime*100,Space.World);
        transform.position = origin + Vector3.up * Mathf.Sin(Time.time) * .25f;
    }


    void OnTriggerEnter(Collider collider){
        if(collider.transform.IsChildOf(GameObject.FindObjectOfType<PlayerInput>().transform)){
            GameObject.FindObjectOfType<BreadMission>().BreadGet(gameObject);
            gameObject.SetActive(false);
        }
    }
}
