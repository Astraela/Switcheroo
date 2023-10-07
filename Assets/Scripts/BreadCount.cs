using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreadCount : MonoBehaviour
{
   public void UpdateText(int breads){
       GetComponentInChildren<Text>().text = "x<size=50>" + breads + "</size>";
   }
}
