using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death<T>: StateAbstract<T>  where T : Player
{
    public override void Awake(T callBack)
    {
        base.Awake(callBack);
    }
    public override void Start(T callBack)
    {
        base.Start(callBack);
    }
    public override void Update(T callBack)
    {
        base.Update(callBack);
    }
    public override void LateUpdate(T callBack)
    {
        base.LateUpdate(callBack);
    }
    public override void FixedUpdate(T callBack)
    {
        base.FixedUpdate(callBack);
    }
    public override void OnAnimatorIK(int layerIndex, T callBack)
    {
        base.OnAnimatorIK(layerIndex, callBack);
    }
    public override void OnAnimatorMove(T callBack)
    {
        base.OnAnimatorMove(callBack);
    }
}


