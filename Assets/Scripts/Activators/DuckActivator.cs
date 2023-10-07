using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckActivator : Activator
{
    public override void Activate(Yarn.Value value)
    {
        var plr = GameObject.FindObjectOfType<PlayerInput>().GetComponent<Entity>();

        switch(value.AsString){
            case "BreadActivate":
                GameObject.FindObjectOfType<BreadMission>().Activate();
            return;
            
            case "BreadDeactivate":
                GameObject.FindObjectOfType<BreadMission>().Deactivate();
                GetComponent<NPC>().talkToNode = "Duck.BodyTalk";
            return;
            
            case "HeadChoose":
                if(!plr.GetComponent<EntityCooldowns>().HasCooldown(Entity.bodyParts.Head)){
                    GetComponent<Entity>().Swap(Entity.bodyParts.Head,Instantiate(plr.defaultHead));
                    plr.Swap(Entity.bodyParts.Head, Instantiate(GetComponent<Entity>().defaultHead));
                    GetComponent<EntityCooldowns>().ResetHead();
                    plr.GetComponent<EntityCooldowns>().ResetHead();
                }
            return;

            case "BodyChoose":
                if(!plr.GetComponent<EntityCooldowns>().HasCooldown(Entity.bodyParts.Body)){
                    GetComponent<Entity>().Swap(Entity.bodyParts.Body,Instantiate(plr.defaultBody));
                    plr.Swap(Entity.bodyParts.Body, Instantiate(GetComponent<Entity>().defaultBody));
                    GetComponent<EntityCooldowns>().ResetBody();
                    plr.GetComponent<EntityCooldowns>().ResetBody();
                }
            return;

            case "LegsChoose":
                if(!plr.GetComponent<EntityCooldowns>().HasCooldown(Entity.bodyParts.Legs)){
                    GetComponent<Entity>().Swap(Entity.bodyParts.Legs,Instantiate(plr.defaultLegs));
                    plr.Swap(Entity.bodyParts.Legs, Instantiate(GetComponent<Entity>().defaultLegs));
                    GetComponent<EntityCooldowns>().ResetLegs();
                    plr.GetComponent<EntityCooldowns>().ResetLegs();
                }
            return;
        }
        
    }

}
