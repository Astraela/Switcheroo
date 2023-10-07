using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KangarooLegs : MonoBehaviour
{

    public float startSpeed = 10;
    public float maxSpeed = 90;
    public float speedStep = 5;
    public float startJump = 14;
    public float maxJump = 35;
    public float jumpStep = 3;

    float currentJump;
    float currentSpeed;

    Camera camera;
    float[] inputs = new float[2];
    Rigidbody rigidbody;
    Transform parent;

    bool jumping = false;
    bool grounded = true;

    void Start()
    {
        currentSpeed = startSpeed-speedStep;
        currentJump = startJump-jumpStep;
        if(GetComponentInParent<PlayerInput>()){
            PlayerInput plr = GetComponentInParent<PlayerInput>();
            plr.Subscribe(KeyCode.Space,Forward);
        }
        rigidbody = GetComponentInParent<Rigidbody>();
        camera = Camera.main;
        parent = transform.parent;
    }

    void OnDisable(){
        if(GetComponentInParent<PlayerInput>()){
            PlayerInput plr = GetComponentInParent<PlayerInput>();
            plr.Unsubscribe(KeyCode.Space);
        }
    }

    bool debounce = false;
    void Forward(float value){
        inputs[0] = value;
    }

    void Update(){
        if(!rigidbody) return;
        
        if(inputs[0] == 1 && grounded && !debounce){
            StartCoroutine(Debounce());
            grounded = false;
            rigidbody.AddForce(Vector3.up * currentJump,ForceMode.Impulse);
            jumping = true;

            currentSpeed = Mathf.Min(currentSpeed + speedStep,maxSpeed);
            currentJump = Mathf.Min(currentJump + jumpStep,maxJump);
        }

        if(inputs[0] == 0 && grounded && !debounce){
            currentSpeed = startSpeed - speedStep;
            currentJump = startJump - jumpStep;
        }

        if(jumping){
            float z = 1;

            Vector3 direction = new Vector3(0,0,z);
            Vector3 movement = camera.transform.TransformDirection(direction);
            movement.y = 0;
            movement.Normalize();
            rigidbody.velocity = new Vector3(movement.x*currentSpeed,rigidbody.velocity.y,movement.z*currentSpeed);
            if(movement != Vector3.zero){
                movement = new Vector3(movement.z, 0, -movement.x);
                Quaternion newRotation = Quaternion.LookRotation(-movement, parent.up);
                parent.rotation = Quaternion.Slerp(parent.rotation, newRotation, Time.deltaTime * 8);
            }
        }


        if(rigidbody.velocity.y < 0){
            rigidbody.velocity += Vector3.up * Physics.gravity.y * 1.5f * Time.deltaTime;
        }else if(rigidbody.velocity.y > 0){
            rigidbody.velocity += Vector3.up * Physics.gravity.y * 1f * Time.deltaTime;
        }
    }

    IEnumerator Debounce(){
        debounce = true;
        yield return new WaitForSeconds(.1f);
        debounce = false;
    }
    void OnTriggerStay(Collider coll){
        if(coll.gameObject.layer != 0) return;
        if(!debounce){
            grounded = true;
            jumping = false;
            rigidbody.velocity = Vector3.Scale(rigidbody.velocity, Vector3.up);
        }
    }

    void OnTriggerExit(Collider coll){
        if(coll.gameObject.layer != 0) return;
        grounded = false;
    }
}
