using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class NPC : MonoBehaviour
{
    public bool show = true;
    public string characterName = "";

    public Vector3 offset = Vector3.zero;
    public Vector3 circleOffset;

    public string talkToNode = "";

    public bool interactable = false;
    public YarnProgram scriptToLoad;
    
    public bool autoInteract = false;
    public float interactRange = 2f;

    public virtual void UpdateVisiblity(bool newShow){
        if(show == newShow) return;
        
        foreach(Renderer renderer in GetComponentsInChildren<Renderer>()){
            renderer.enabled = newShow;
        }

        show = newShow;
    }

    IEnumerator Start () { 
        yield return new WaitForEndOfFrame();
        DialogueHelper dialogueHelper = FindObjectOfType<DialogueHelper>();
        if(dialogueHelper.Npcs.ContainsKey(characterName)){
            show = dialogueHelper.Npcs[characterName].show;
            dialogueHelper.Npcs[characterName] = this;
        }else{
                dialogueHelper.Npcs.Add(characterName,this);
        }
        UpdateVisiblity(show);
        if (scriptToLoad != null) {
            DialogueRunner dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
                    dialogueRunner.Add(scriptToLoad);  
        }
    }
}
