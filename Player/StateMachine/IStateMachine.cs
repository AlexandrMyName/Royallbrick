using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateMachine<T> where T : class
{
    List<T> States { get; set; }

    void Awake();
    void Start();
    void Update();
    void LateUpdate();
    void FixedUpdate();
    void OnAnimatorIK(int layerIndex);
    void OnAnimatorMove();
    void StartLogic(T state);
}
