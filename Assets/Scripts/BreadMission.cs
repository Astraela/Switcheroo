using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadMission : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> breads = new List<GameObject>();

    public NPC npc;
    public BreadCount breadCount;

    void Start()
    {
        foreach(GameObject bread in breads){
            bread.SetActive(false);
        }
        breadCount.gameObject.SetActive(false);
    }

    public void Activate(){
        foreach(GameObject bread in breads){
            bread.SetActive(true);
        }
        breadCount.gameObject.SetActive(true);
    }
    
    public void Deactivate(){
        breadCount.gameObject.SetActive(false);
    }

    void Check(){
        if(breads.Count <= 0){
            npc.talkToNode = "Duck.BreadGet";
            print("Mission Completed");
        }
    }

    public void BreadGet(GameObject bread){
        if(breads.Contains(bread)){
            breads.Remove(bread);
            breadCount.UpdateText(5-breads.Count);
            Check();
        }
    }
}
