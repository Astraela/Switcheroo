using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
    void OnCollisionEnter(){
        SceneManager.LoadScene(1);
    }
}
