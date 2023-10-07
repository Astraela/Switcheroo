using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public enum bodyParts{Head,Body,Legs};

    public GameObject Head;
    public GameObject Body;
    public GameObject Legs;

    public LayerMask layers;

    [NonSerialized]
    public GameObject defaultHead;
    [NonSerialized]
    public GameObject defaultBody;
    [NonSerialized]
    public GameObject defaultLegs;
    

    private Bounds GetBounds(){
        var combinedBounds = GetComponent<Renderer>().bounds;
        var renderers = GetComponentsInChildren<Renderer>();
        foreach(Renderer renderer in renderers) {
            combinedBounds.Encapsulate(renderer.bounds);
        }
        return combinedBounds;
    }

    /*
    private void OldPlaceOnGround(){
        RaycastHit hit;
        if(Physics.Raycast(transform.position,Vector3.down,out hit,100,layers)){
            transform.position = hit.point + new Vector3(0,transform.position.y - Legs.transform.Find("Bottom").position.y,0 );
        }
    }*/

    private void PlaceOnGround(){
        transform.position += new Vector3(0,transform.position.y - Legs.transform.Find("Bottom").position.y,0);
    }



    public void ResetHead() => Swap(bodyParts.Head,Instantiate(defaultHead));
    public void ResetBody() => Swap(bodyParts.Body,Instantiate(defaultBody));
    public void ResetLegs() => Swap(bodyParts.Legs,Instantiate(defaultLegs));

    public void Swap(bodyParts bodyPart, GameObject newPart){
        switch (bodyPart)
        {
            case bodyParts.Head:
                Destroy(Head);
                Head = newPart;
                Head.transform.parent = transform;
                Attach();
            return;

            case bodyParts.Body:
                Destroy(Body);
                Body = newPart;
                Body.transform.position = transform.position;
                Body.transform.rotation = transform.rotation;
                Body.transform.parent = transform;
                Attach();
            return;

            case bodyParts.Legs:
                Destroy(Legs);
                Legs = newPart;
                Legs.transform.parent = transform;
                Attach();
            return;
        }
    }

    void Attach(){
        Legs.GetComponent<BodyPart>().Attach(Body.transform.Find("LegsAttachment"));
        Head.GetComponent<BodyPart>().Attach(Body.transform.Find("HeadAttachment"));
        PlaceOnGround();
    }

    // Start is called before the first frame update
    void Awake()
    {
        defaultHead = Head;
        defaultBody = Body;
        defaultLegs = Legs;

        Head = Instantiate(Head);
        Head.transform.parent = transform;
        Body = Instantiate(Body);
        Body.transform.parent = transform;
        Body.transform.rotation = transform.rotation;
        Legs = Instantiate(Legs);
        Legs.transform.parent = transform;
        Body.transform.position = transform.position;
        Attach();

    }
}
