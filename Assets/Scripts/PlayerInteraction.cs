using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject Circle;
    public KeyCode interactionKey = KeyCode.E;
    public float interactionRange = 2;
    NPC currentNpc = null;
    DialogueRunner dialogueRunner;
    void Start(){
    }

    void Update(){
        GetClosest();
        if(currentNpc != null && currentNpc.autoInteract == false && currentNpc.interactable == true){
            Circle.SetActive(true);
            Circle.transform.position = currentNpc.transform.position + currentNpc.circleOffset;
        }else{
            Circle.SetActive(false);
        }
        if(FindObjectOfType<DialogueRunner>().IsDialogueRunning) {
            Circle.SetActive(false);
            return;
        };
        if(Input.GetKeyUp(interactionKey) && currentNpc != null){
            Interact();
        }
        if(currentNpc != null && currentNpc.autoInteract && Vector3.Distance(currentNpc.transform.position,transform.position) <= currentNpc.interactRange){
            Interact();
        }
    }

    public void DialogueEnd(){
        
    }

    void Interact(){
        dialogueRunner = FindObjectOfType<DialogueRunner>();
            Circle.SetActive(false);
            
        if(FindObjectOfType<DialogueRunner>().NodeExists(currentNpc.talkToNode)){
            dialogueRunner.StartDialogue (currentNpc.talkToNode);
        }else{
            Debug.LogWarning("node does not exist | "+ currentNpc.talkToNode);
        }
    }

    void GetClosest(){
        var npcs = FindObjectsOfType<NPC>();
        NPC closestNpc = null;
        float dis = 1000000;
        foreach(NPC npc in npcs){
            if(npc.interactable && npc.show){
                float distance = Vector3.Distance(this.transform.position,npc.transform.position);
                if(distance <= interactionRange && distance < dis){
                    dis = Vector3.Distance(this.transform.position,npc.transform.position);
                    closestNpc = npc;
                }
            }
        }
        currentNpc = closestNpc;
    }

}