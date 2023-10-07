using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform subject;
    public float maxDistance;
    public float minDistance;

    public float currentDistance;

    public float scrollSpeed;
    public Vector2 rotateSpeed;

    public float minHeight;
    public float maxHeight;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-Input.GetAxis("Mouse X")*rotateSpeed.x,Input.GetAxis("Mouse Y")*rotateSpeed.y,0)* Time.deltaTime);
        if(transform.position.y - subject.position.y < minHeight){
            transform.position = new Vector3(transform.position.x,subject.position.y + minHeight, transform.position.z);
        }else if(transform.position.y - subject.position.y > maxHeight){
            transform.position = new Vector3(transform.position.x,subject.position.y + maxHeight, transform.position.z);
        }
        
        transform.LookAt(subject);

        currentDistance = Mathf.Clamp(Input.mouseScrollDelta.y * scrollSpeed,minDistance,maxDistance);

        float distance = Vector3.Distance(transform.position,subject.position);
        if(distance != currentDistance){
            float value = (currentDistance-distance)/currentDistance;
            transform.position = Vector3.LerpUnclamped(transform.position,subject.position,-value);
        }
    }
}
