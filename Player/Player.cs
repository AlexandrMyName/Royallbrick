using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


public class Player : /*MonoBehaviour*/ NetworkBehaviour
{
    IStateMachine<StateAbstract<Player>> stateMachine;
    List<StateAbstract<Player>> states;
    Container container;
    public override void OnStartLocalPlayer(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        container = GetComponent<Container>();
        states = new List<StateAbstract<Player>>() {
         new Move<Player>(),
         new FootIK<Player>(),
         new Checker<Player>(),
         new CameraSetup<Player>(),
         new AnimationSetup<Player>(),
         new HendIK<Player>(),
         new Jump<Player>(),
         new Parkur<Player>(),
         new PVP<Player>(),
         new Aiming<Player>(),
         new Inventory<Player>(),
         new Sword<Player>(),
         new TakeDamage<Player>() 
        };
        stateMachine = new Player_StateMachine<StateAbstract<Player>>(this, states);
        stateMachine.Awake();
        stateMachine.Start();
    }
    public Container Container { get { return container; } set { container = value; } }
   
    void Update(){
        if (!isLocalPlayer) return; //[MIRROR]
        stateMachine.Update();

        
    }
    void LateUpdate(){

        if (Input.GetKeyDown(KeyCode.G)) ApplyDamage(30);

        if (!isLocalPlayer) return; //[MIRROR]
        stateMachine.LateUpdate();
    }
    void FixedUpdate(){
        if (!isLocalPlayer) return; //[MIRROR]
        stateMachine.FixedUpdate();
    }
    void OnAnimatorIK(int layerIndex){
        if (!isLocalPlayer) return; //[MIRROR]
        stateMachine.OnAnimatorIK(layerIndex);
    }
    void OnAnimatorMove(){
        if (!isLocalPlayer) return; //[MIRROR]
        stateMachine.OnAnimatorMove();
    }

    public void ApplyDamage(float damage){

        container.HpViewModel.ApplyDamage(damage);
    }
    
}
