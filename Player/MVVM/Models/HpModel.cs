using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal sealed class HpModel : IHpModel
{
    public float MaxHp { get; }
    public float CurrentHp { get; set; }
    public HpModel(float maxHp)
    {
        MaxHp = maxHp;
        CurrentHp = MaxHp;
    }
}
