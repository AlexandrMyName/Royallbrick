using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum swordLevels { simpleSword = 12, vipSword = 32 }


public class SwordBeh : MonoBehaviour 
{
    [SerializeField] swordLevels swordInfo;
    [SerializeField] Player player;


    [Server]
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Character_Player"  && player.isLocalPlayer )
        {
            if (collision.gameObject == player.gameObject) return;
            Debug.Log("ÓÄÀÐ"); Debug.Log((float)swordInfo);
            collision.gameObject.GetComponent<Player>().ApplyDamage((float)swordInfo);
        }
    }

}
