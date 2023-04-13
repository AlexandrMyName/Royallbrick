using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHpViewModel
{
    IHpModel HpModel { get; }
    bool IsDead { get; }
    void ApplyDamage(float damage);
    event Action<float> OnHpChange;
}

