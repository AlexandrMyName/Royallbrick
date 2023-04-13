using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector_OtherPlayers : MonoBehaviour 
{
     Container playerC;
    void Awake()
    {
        playerC = transform.parent.transform.GetComponent<Container>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player_Detector" && other.gameObject != this.gameObject && playerC.isLocalPlayer )
        {
            int index = playerC.OtherPlayers.FindIndex(x => x.gameObject == other.gameObject.transform.parent.gameObject);
            if (index == -1) playerC.OtherPlayers.Add(other.gameObject.transform.parent.gameObject);
            
        }
        

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player_Detector" && other.gameObject != this.gameObject && playerC.isLocalPlayer)
        {
            int index = playerC.OtherPlayers.FindIndex(x => x.gameObject == other.gameObject.transform.parent.gameObject);
            if (index != -1) playerC.OtherPlayers.RemoveAt(index);
           
        }
       
    }
}
