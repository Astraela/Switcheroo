using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legs : MonoBehaviour
{

    public float speed = 10;
    public float jumpStrength = 24;
    Camera camera;
    float[] inputs = new float[5];
    Rigidbody rigidbody;
    Transform parent;

    public LayerMask groundLayers;

    bool grounded = true;

    void Start()
    {
        if(GetComponentInParent<PlayerInput>()){
            PlayerInput plr = GetComponentInParent<PlayerInput>();
            plr.Subscribe(KeyCode.W,Forward);
            plr.Subscribe(KeyCode.S,Backwards);
            plr.Subscribe(KeyCode.A,Left);
            plr.Subscribe(KeyCode.D,Right);
            plr.Subscribe(KeyCode.Space,Jump);
        }
        rigidbody = GetComponentInParent<Rigidbody>();
        camera = Camera.main;
        parent = transform.parent;
    }

    void OnDisable(){
        if(GetComponentInParent<PlayerInput>()){
            PlayerInput plr = GetComponentInParent<PlayerInput>();
            plr.Unsubscribe(KeyCode.W);
            plr.Unsubscribe(KeyCode.S);
            plr.Unsubscribe(KeyCode.A);
            plr.Unsubscribe(KeyCode.D);
            plr.Unsubscribe(KeyCode.Space);
        }
    }

    void Jump(float value){
        if(value == 1 && grounded){
            grounded = false;
            rigidbody.AddForce(Vector3.up * jumpStrength,ForceMode.Impulse);
        }
        inputs[4] = value;
    }

    void Forward(float value){
        inputs[0] = value;
    }

    void Backwards(float value){
        inputs[1] = value;
    }

    void Left(float value){
        inputs[2] = value;
    }

    void Right(float value){
        inputs[3] = value;
    }

    void Update(){
        if(!rigidbody) return;
        
        float x = inputs[2] != 0 ? -inputs[2] : inputs[3];
        float z = inputs[0] != 0 ? inputs[0] : -inputs[1];

        Vector3 direction = new Vector3(x,0,z);
        Vector3 movement = camera.transform.TransformDirection(direction);
        movement.y = 0;
        movement.Normalize();
        rigidbody.velocity = new Vector3(movement.x*speed,rigidbody.velocity.y,movement.z*speed);
        if(rigidbody.velocity.y < 0){
            rigidbody.velocity += Vector3.up * Physics.gravity.y * 1.5f * Time.deltaTime;
        }else if(rigidbody.velocity.y > 0){
            rigidbody.velocity += Vector3.up * Physics.gravity.y * 1f * Time.deltaTime;
        }


        if(movement != Vector3.zero){
            movement = new Vector3(-movement.z, 0, movement.x);
            Quaternion newRotation = Quaternion.LookRotation(movement, parent.up);
            parent.rotation = Quaternion.Slerp(parent.rotation, newRotation, Time.deltaTime * 8);
        }
    }

    void OnTriggerStay(Collider coll){
        if (coll.gameObject.layer != 0) return;
        grounded = true;
    }

    void OnTriggerExit(Collider coll){
        if (coll.gameObject.layer != 0) return;
        grounded = false;
    }
}
