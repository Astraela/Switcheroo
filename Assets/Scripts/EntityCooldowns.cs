using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCooldowns : MonoBehaviour
{
    public float MaxCooldown = 20;

    public delegate void Change(float newValue);

    public Change headChange;
    public Change bodyChange;
    public Change legsChange;

    float _HeadCooldown = 0;
    float _BodyCooldown = 0;
    float _LegsCooldown = 0;

    float HeadCooldown {get => _HeadCooldown; 
        set{ 
            _HeadCooldown = value; 
            headChange?.Invoke(value/MaxCooldown);
            if(value < 0)
                GetComponent<Entity>().ResetHead();
        }}
    float BodyCooldown {get => _BodyCooldown; 
        set{ 
            _BodyCooldown = value; 
            bodyChange?.Invoke(value/MaxCooldown);
            if(value < 0)
                GetComponent<Entity>().ResetBody();
        }}
    float LegsCooldown {get => _LegsCooldown; 
        set{ 
            _LegsCooldown = value; 
            legsChange?.Invoke(value/MaxCooldown);
            if(value < 0)
                GetComponent<Entity>().ResetLegs();
        }}

    public bool HasCooldown(Entity.bodyParts bodyPart){
        switch(bodyPart){
            case Entity.bodyParts.Head:
                return HeadCooldown > 0;
            
            case Entity.bodyParts.Body:
                return BodyCooldown > 0;
            
            case Entity.bodyParts.Legs:
                return LegsCooldown > 0;
                
            default: 
                return false;
        }
    } 

    void Update(){
        if(HeadCooldown > 0)
            HeadCooldown -= Time.deltaTime;
        if(BodyCooldown > 0)
            BodyCooldown -= Time.deltaTime;
        if(LegsCooldown > 0)
            LegsCooldown -= Time.deltaTime;
    }

    public void ResetHead () => HeadCooldown = MaxCooldown;
    public void ResetBody () => BodyCooldown = MaxCooldown;
    public void ResetLegs () => LegsCooldown = MaxCooldown;
}
