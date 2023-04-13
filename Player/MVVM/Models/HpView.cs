using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class HpView : NetworkBehaviour
{
    private Container playerC;

    private IHpViewModel _hpViewModel;
    public void Initialize(IHpViewModel hpViewModel){
        playerC = GetComponent<Player>().Container;
        _hpViewModel = hpViewModel;
        _hpViewModel.OnHpChange += OnHpChange;
        OnHpChange(100f);
    }
    private void OnHpChange(float currentHp){

        playerC.SyncActions.CmdOnDamagePlayer(playerC, currentHp);
    }
    public void DeconstructHpView(){// DECONSTRUCT ! ! ! 

        _hpViewModel.OnHpChange -= OnHpChange;
    }

}