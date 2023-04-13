using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInvoker : NetworkBehaviour
{
    Container playerC;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        playerC = this.gameObject.GetComponent<Player>().Container;
    }

    #region Jumping state
    public void DoubleJumpEvent(){
        if (isLocalPlayer){

            playerC = GetComponent<Player>().Container;
            playerC.IsDoubleJump = true;
        }
    }
    public void JumpDownEvent(){
        if (isLocalPlayer) {
            playerC = GetComponent<Player>().Container;
          
            playerC.IsJump = false;
            playerC.IsDoubleJump = false;
            playerC.Animator.SetBool("IsJump", playerC.IsJump);
            playerC.Animator.SetBool("IsDubleJump", playerC.IsDoubleJump);
        }
    }

    #endregion

    #region Inventory logics

    //SWORD invokes
    public void HideSword(){
        if (isLocalPlayer)
            playerC.SyncActions.CmdHideSword(playerC);
    }
    public void Hide_DecoratorSword(){
        if (isLocalPlayer)
            playerC.SyncActions.CmdHideDecoratorSword(playerC);
    }
    public void Attack1_Enable(){
        if(isLocalPlayer)
        playerC.Animator.SetBool("Attack", false);
    }
    #endregion
}
