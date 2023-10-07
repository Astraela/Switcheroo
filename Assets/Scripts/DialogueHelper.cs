using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class DialogueHelper : MonoBehaviour
{
    public Dictionary<string,Activator> Activators = new Dictionary<string, Activator>(); 
    public Dictionary<string,NPC> Npcs =  new Dictionary<string, NPC>();
    DialogueRunner dialogueRunner;
    private HashSet<string> _visitedNodes = new HashSet<string>();
    public RectTransform dialogueText;
    public GameObject Options;
    void Awake(){
        if(FindObjectsOfType<DialogueHelper>().Length > 1) Destroy(gameObject);
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        gameObject.name = "ORIGINAL";

        dialogueRunner.AddFunction("visited", 1, (Yarn.Value[] parameters) => {return Visited(parameters);});
        dialogueRunner.AddFunction("hide",1,Hide);
        dialogueRunner.AddFunction("show",1,Show);
        dialogueRunner.AddFunction("uninteractable",1,UnInteractable);
        dialogueRunner.AddFunction("interactable",1,Interactable);
        dialogueRunner.AddFunction("scene",1,Scene);
        dialogueRunner.AddFunction("activate",2,Activate);
    }
    
    bool Visited(Yarn.Value[] parameters)
    {
        var nodeName = parameters[0];
        return _visitedNodes.Contains(nodeName.AsString);
    }

    void Hide(Yarn.Value[] parameters){
        var name = parameters[0].AsString;
        Npcs[name].UpdateVisiblity(false);
    }

    void UnInteractable(Yarn.Value[] parameters){
        var name = parameters[0].AsString;
        Npcs[name].interactable = false;
    }
    void Interactable(Yarn.Value[] parameters){
        var name = parameters[0].AsString;
        Npcs[name].interactable = true;
    }

    void Show(Yarn.Value[] parameters){
        var name = parameters[0].AsString;
        Npcs[name].UpdateVisiblity(true);
    }

    void Scene(Yarn.Value[] parameters){
        int sceneInt = (int)parameters[0].AsNumber;
        SceneManager.LoadScene(sceneInt);
    }

    void Activate(Yarn.Value[] parameters){
        string name = parameters[0].AsString;
        Activators[name].Activate(parameters[1]);
    }

    public void NodeComplete(string nodeName) {
        _visitedNodes.Add(nodeName);
        Cursor.lockState = CursorLockMode.Locked;
    }

	public void NodeStart(string nodeName) {
        Cursor.lockState = CursorLockMode.None;
        var tags = new List<string>(dialogueRunner.GetTagsForNode(nodeName));
        
		Debug.Log($"Starting the execution of node {nodeName} with {tags.Count} tags.");
	}

    public void DialogueCompleted(){
        FindObjectOfType<PlayerInteraction>().DialogueEnd();
        dialogueText.GetComponent<Text>().text = "";
        Options.SetActive(false);
    }

    public void Reset(){
        Npcs = new Dictionary<string, NPC>();
        _visitedNodes = new HashSet<string>();
        FindObjectOfType<DialogueRunner>().Clear();
    }
}