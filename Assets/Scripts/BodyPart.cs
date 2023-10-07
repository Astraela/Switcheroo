using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public Transform otherAttachment;
    

    void Attach(){
        transform.rotation = otherAttachment.rotation;
        transform.position = otherAttachment.position;
    }

    public void Attach(Transform attachment){
        otherAttachment = attachment;
        transform.rotation = otherAttachment.rotation;
        transform.position = otherAttachment.position;
    }

    void Update()
    {
        if(otherAttachment != null){
            Attach();
        }
    }
}
