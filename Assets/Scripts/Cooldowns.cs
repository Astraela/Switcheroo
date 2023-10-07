using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Cooldowns : MonoBehaviour
{
    public GameObject HeadCooldown;
    public GameObject BodyCooldown;
    public GameObject LegsCooldown;

    private EntityCooldowns player;

    private Vector3 scaleMultiplier = new Vector3(60,60,60);
    private Vector3 offset = new Vector3(4,-15.5f,0);
    private Quaternion rotation = Quaternion.Euler(0,-44.56f,0);

    void Start(){
        player = GameObject.FindObjectOfType<PlayerInput>().GetComponent<EntityCooldowns>();
        player.headChange += HeadChange;
        player.bodyChange += BodyChange;
        player.legsChange += LegsChange;
    }

    void SetLayerRecursively(GameObject obj, int layer){
        obj.layer = layer;
    
        foreach(Renderer renderer in obj.GetComponentsInChildren<Renderer>()){
            renderer.gameObject.layer = layer;
        }
    }

    void ChangeMaterials(GameObject gameObject){
        foreach (Renderer renderer in gameObject.GetComponentsInChildren<Renderer>())
        {
            foreach (Material material in renderer.materials)
            {
                material.SetFloat("_OutlineSize", 0);
            }
        }
    }

    void HeadChange(float value){
        HeadCooldown.GetComponent<Image>().fillAmount = value;

        if(value > 0 && !HeadCooldown.activeSelf){
            HeadCooldown.SetActive(true);
            Destroy(HeadCooldown.transform.GetChild(0).gameObject);
            var newHead = Instantiate(player.GetComponent<Entity>().Head);
            SetLayerRecursively(newHead,5);
            Destroy(newHead.GetComponent<BodyPart>());
            Vector3 scale = newHead.transform.localScale;
            newHead.transform.SetParent(HeadCooldown.transform);
            newHead.transform.localPosition = offset;
            newHead.transform.localRotation = rotation;
            newHead.transform.localScale = Vector3.Scale(scale,scaleMultiplier);
            ChangeMaterials(newHead);
        }else if(value < 0 && HeadCooldown.activeSelf){
            HeadCooldown.SetActive(false);
        }
    }

    void BodyChange(float value){
        BodyCooldown.GetComponent<Image>().fillAmount = value;

        if(value > 0 && !BodyCooldown.activeSelf){
            BodyCooldown.SetActive(true);
            Destroy(BodyCooldown.transform.GetChild(0).gameObject);
            var newBody = Instantiate(player.GetComponent<Entity>().Body);
            SetLayerRecursively(newBody,5);
            Destroy(newBody.GetComponent<BodyPart>());
            Vector3 scale = newBody.transform.localScale;
            newBody.transform.SetParent(BodyCooldown.transform);
            newBody.transform.localPosition = offset;
            newBody.transform.localRotation = rotation;
            newBody.transform.localScale = Vector3.Scale(scale,scaleMultiplier);
            ChangeMaterials(newBody);
        }else if(value < 0 && BodyCooldown.activeSelf){
            BodyCooldown.SetActive(false);
        }
    }

    void LegsChange(float value){
        LegsCooldown.GetComponent<Image>().fillAmount = value;

        if(value > 0 && !LegsCooldown.activeSelf){
            LegsCooldown.SetActive(true);
            Destroy(LegsCooldown.transform.GetChild(0).gameObject);
            var newLegs = Instantiate(player.GetComponent<Entity>().Legs);
            SetLayerRecursively(newLegs,5);
            Destroy(newLegs.GetComponent<BodyPart>());
            Vector3 scale = newLegs.transform.localScale;
            newLegs.transform.SetParent(LegsCooldown.transform);
            newLegs.transform.localPosition = offset;
            newLegs.transform.localRotation = rotation;
            newLegs.transform.localScale = Vector3.Scale(scale,scaleMultiplier);
            ChangeMaterials(newLegs);
        }else if(value < 0 && LegsCooldown.activeSelf){
            LegsCooldown.SetActive(false);
        }
    }

}
