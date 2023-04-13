using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory<T>: StateAbstract<T>  where T : Player
{
    Container playerC;
    public override void Awake(T player) => playerC = player.Container;

    public override void Update(T callBack){
        if(!playerC.AllowMove || !playerC.AllowPVP)
        {

            playerC.IsSword = false;
            playerC.Animator.SetBool("Sword", false);
            return;
        }

        if (playerC.IsWall) return;

        if (Input.GetKeyDown(KeyCode.Alpha1)){
            playerC.IsSword = true;
            playerC.Animator.SetLayerWeight(1, 1);
            playerC.Animator.SetBool("Sword", true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0)){
            playerC.IsSword = false;
            playerC.Animator.SetBool("Sword", false);
        }
    }
}


