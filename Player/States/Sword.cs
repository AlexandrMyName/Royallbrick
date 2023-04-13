using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword<T>: StateAbstract<T>  where T : Player
{
    Container playerC;
    bool isSword;
    public override void Awake(T player) => playerC = player.Container;

    public override void Start(T callBack)
    {
        //playerC.SyncActions.CmdShowWeight(playerC);
    }

    public override void Update(T player)
    {
        if (playerC.IsSword){

            isSword = true;
            if (Input.GetMouseButtonDown(0))
                playerC.Animator.SetBool("Attack",true);
        }
        else
            isSword = false;
    }
    public override void OnAnimatorIK(int layerIndex, T callBack)
    {
       
            if (playerC.IsSword )
            {

                playerC.Animator.SetLookAtWeight(playerC.Weight_MAIN, playerC.Weight_BODY, playerC.Weight_HEAD, playerC.Weight_EYES, playerC.Weight_CLAMP);
                playerC.Animator.SetLookAtPosition(playerC.Main_Camera.transform.GetChild(1).transform.position);


            }
           

       
        
        
    }
}


