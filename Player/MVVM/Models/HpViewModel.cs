using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal sealed class HpViewModel : IHpViewModel
{
    private bool _isDead;
    public event Action<float> OnHpChange;
    public IHpModel HpModel { get; }
    public bool IsDead => _isDead;
    public HpViewModel(IHpModel hpModel)
    {
        HpModel = hpModel;
    }
    public void ApplyDamage(float damage)
    {
        HpModel.CurrentHp -= damage;
        if (HpModel.CurrentHp <= 0)
        {
            _isDead = true;
        }
        OnHpChange?.Invoke(HpModel.CurrentHp);
    }
}
