using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum ShopPortals { ToShop, FromShop}


public class ShopPortal : MonoBehaviour
{
    [SerializeField] ShopPortals portal;
    [SerializeField] Transform spawnTo;
    [SerializeField] Transform spawnFrom;
 
    private void OnTriggerEnter(Collider other){

        if(other.gameObject.tag == "Character_Player"){
            if (!other.gameObject.GetComponent<Player>().isLocalPlayer) return;
            if(portal == ShopPortals.ToShop)
               other.gameObject.transform.position = spawnTo.position;
                else if(portal == ShopPortals.FromShop)
                    other.gameObject.transform.position = spawnFrom.position;
            
        }
    }
}
