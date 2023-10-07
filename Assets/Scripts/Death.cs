using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public Transform Deathpoint;

    void OnCollisionEnter(Collision coll){
        if(coll.transform.IsChildOf(GameObject.FindObjectOfType<PlayerInput>().transform)){
            GameObject.FindObjectOfType<PlayerInput>().deathPos = Deathpoint.position;
            GameObject.FindObjectOfType<PlayerInput>().Die();
        }
    }
}
