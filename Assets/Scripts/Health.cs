using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float _health;
    private float health {get =>_health;  set{
        _health = value;
        healthChange?.Invoke(value);
    }}
    
    public delegate void Event(float newValue);
    public Event healthChange; 

    public void Damage(float value){
        health -= value;
    }

    public void Heal(float value){
        health += value;
    }
}
