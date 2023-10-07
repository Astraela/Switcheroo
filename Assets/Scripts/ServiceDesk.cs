using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceDesk : MonoBehaviour
{
    public List<Service> services = new List<Service>();

    public GameObject GetItem(string key){
        if(services.Find(x => x.name == key) != null){
            var obj = services.Find(x => x.name == key).obj;
            return obj;
        }
        return null;
    }

    public void SetItem(string key, GameObject obj){
        if(services.Find(x => x.name == key) != null){
            services.Find(x => x.name == key).obj = obj;
        }else{
            services.Add(new Service(key,obj));
        }
    }

}

[Serializable]
public class Service{
    public string name;
    public GameObject obj;

    public Service(string name,GameObject obj){
        this.name = name;
        this.obj = obj;
    }
}
