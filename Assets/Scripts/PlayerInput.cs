using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    public Vector3 deathPos;
    public delegate void Command(float value);
    Dictionary<KeyCode,Command> commands = new Dictionary<KeyCode, Command>();
    
    public GameObject head2;

    public void Subscribe(KeyCode key, Command command){
        commands.Add(key,command);
    }

    public void Unsubscribe(KeyCode key){
        commands.Remove(key);
    }

    public void Die(){
        GameObject.FindGameObjectWithTag("Death").GetComponent<Image>().enabled = true;
        transform.position = deathPos;
        StartCoroutine(Death());
    }

    IEnumerator Death(){
        yield return new WaitForSeconds(.5f);
        GameObject.FindGameObjectWithTag("Death").GetComponent<Image>().enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(KeyValuePair<KeyCode,Command> pair in commands){
            if(Input.GetKeyDown(pair.Key))
                pair.Value(1);
            else if(Input.GetKeyUp(pair.Key))
                pair.Value(0);
        }
    }
}
