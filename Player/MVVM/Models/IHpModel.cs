using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHpModel
{
    float MaxHp { get; }
    float CurrentHp { get; set; }
}
