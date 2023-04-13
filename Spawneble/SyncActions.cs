using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SyncActions : NetworkBehaviour
{

    [Command(requiresAuthority = false)]
    public void CmdHideSword(Container playerC) => RpcHideSword(playerC);
   
    [Command(requiresAuthority = false)]
    public void CmdHideDecoratorSword(Container playerC) => RpcdHideDecoratorSword(playerC);
    [Command(requiresAuthority = false)]
    public void CmdOnDamagePlayer(Container playerC, float currentHp) => RpcOnDamagePlayer(playerC, currentHp);
   // [Command(requiresAuthority = false)]
   // public void CmdApplyDamagePlayer(Container playerC, float currentHp) => RpcApplyDamagePlayer(playerC, currentHp);

    [ClientRpc]
    void RpcOnDamagePlayer(Container playerC, float currentHp)
    {
        playerC.HealthBar.value = currentHp;
       // healthBarOnHead.value = currentHp;
    }
    [ClientRpc]
    void RpcdHideDecoratorSword(Container playerC){
        playerC.Sword.SetActive(true);
        playerC.Sword_Decorator.SetActive(false);
    }
    [ClientRpc]
    void RpcHideSword(Container playerC)
    {
        playerC.Animator.SetLayerWeight(1, 0);
        playerC.Sword_Decorator.SetActive(true);
        playerC.Sword.SetActive(false);
    }

    



}
