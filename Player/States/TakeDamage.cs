using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage<T>: StateAbstract<T>  where T : Player
{

    Container playerC;

    
    


    public override void Awake(T player) => playerC = player.Container;
    public override void Start(T player)
    {
        playerC.HealthBar.maxValue = 100f;
        playerC.HealthBar.value = 100f;

        playerC.HpModel = new HpModel(100f);
        playerC.HpViewModel = new HpViewModel(playerC.HpModel);
        playerC.HpView.Initialize(playerC.HpViewModel);
        

    }
   



    public override void LateUpdate(T callBack)
    {
        if (playerC.isLocalPlayer)
        {
            playerC.HealthBar.gameObject.SetActive(false);
        }
       
        foreach(var player in playerC.OtherPlayers)
            player.GetComponent<Container>().HealthBar.transform.LookAt(playerC.Main_Camera.transform);
        


    }
  
}


