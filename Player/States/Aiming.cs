using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming<T>: StateAbstract<T>  where T : Player
{
    Container playerC;
    public override void Awake(T player) => playerC = player.Container;

    public override void Update(T callBack){
        
    }
    public override void OnAnimatorIK(int layerIndex, T player){

    }
}


