using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVP<T>: StateAbstract<T>  where T : Player
{
    Container playerC;
    public override void Awake(T callBack) => playerC = callBack.Container;
    public override void Update(T player){

       if(!playerC.AllowMove && playerC.OnPVP_area){

            if (Input.GetKeyDown(KeyCode.F)){
                if (playerC.Pvp)
                    playerC.Pvp.BackToMainLobby(player);
            }
        }
    }
   
}


