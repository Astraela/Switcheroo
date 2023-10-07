using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class CameraRotator : MonoBehaviour
{
    public Transform target;
    public Camera mainCamera;
    [Range(0.1f, 5f)]
    [Tooltip("How sensitive the mouse drag to camera rotation")]
    public float mouseRotateSpeed = 0.8f;
    [Tooltip("Smaller positive value means smoother rotation, 1 means no smooth apply")]
    public float slerpValue = 0.25f;
    
    public float minDistance;
    public float maxDistance;

    public float currentDistance;

    public float scrollSpeed;

    public bool antiClip;
    public LayerMask layerMask;

    private Vector2 swipeDirection; //swipe delta vector2
    private Quaternion cameraRot; // store the quaternion after the slerp operation
    private Touch touch;
    private float distanceBetweenCameraAndTarget;
    private float minXRotAngle = -30; //min angle around x axis
    private float maxXRotAngle = 60; // max angle around x axis
    //Mouse rotation related
    private float rotX; // around x
    private float rotY; // around y

    private void Awake(){
        if (mainCamera == null)
        {
        mainCamera = Camera.main;
        }
    }
    
    void Update(){
        rotX += -Input.GetAxis("Mouse Y") * mouseRotateSpeed; // around X
        rotY += Input.GetAxis("Mouse X") * mouseRotateSpeed;
        if (rotX < minXRotAngle){
            rotX = minXRotAngle;
        }
        else if (rotX > maxXRotAngle){
            rotX = maxXRotAngle;
        }
        
        currentDistance = Mathf.Clamp(currentDistance + -Input.mouseScrollDelta.y * scrollSpeed,minDistance,maxDistance);

        float distance = Vector3.Distance(transform.position,target.position);
        if(distance != currentDistance){
            float value = (currentDistance-distance)/currentDistance;
            transform.position = Vector3.LerpUnclamped(transform.position,target.position,-value);
        }
    }
    private void LateUpdate()
    {
        float rayDistance = 1000;
        if(antiClip){
            RaycastHit hit;
            if(Physics.Raycast(target.position,-(target.position-transform.position).normalized,out hit,maxDistance,layerMask)){
                if(hit.distance<currentDistance){
                    rayDistance = hit.distance - .5f;
                }
            }
        }
        
        Vector3 dir = new Vector3(0, 0, currentDistance < rayDistance ? -currentDistance : -rayDistance); //assign value to the distance between the maincamera and the target
        Quaternion newQ;
        newQ = Quaternion.Euler(rotX , rotY, 0); //We are setting the rotation around X, Y, Z axis respectively
        
        
        cameraRot = Quaternion.Slerp(cameraRot, newQ, slerpValue); //let cameraRot value gradually reach newQ which corresponds to our touch
        mainCamera.transform.position = target.position + cameraRot * dir;
        mainCamera.transform.LookAt(target.position);
    }
}