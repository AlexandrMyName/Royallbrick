using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateAbstract <T> where T : /*MonoBehaviour*/ NetworkBehaviour
{
    public virtual void Awake(T callBack) { }
    public virtual void Start(T callBack) { }
    public virtual void Update(T callBack) { }
    public virtual void LateUpdate(T callBack) { }
    public virtual void FixedUpdate(T callBack) { }
    public virtual void OnAnimatorIK(int layerIndex, T callBack) { }
    public virtual void OnAnimatorMove(T callBack) { }
    public virtual void StartLogic(T callBack) { }

}
