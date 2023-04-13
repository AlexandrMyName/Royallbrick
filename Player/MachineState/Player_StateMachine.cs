using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_StateMachine<T> : IStateMachine<T> where T : StateAbstract<Player>
{
    List<T> player_states;
    Player player;

    public Player_StateMachine(Player player, List<T> PlayerStates){
        this.player = player;
        this.player_states = PlayerStates;
    }
    public List<T> States { get => player_states; set => player_states = value; }
    public void Awake(){foreach(var state in player_states) state.Awake(player);}
    void IStateMachine<T>.Start(){foreach (var state in player_states) state.Start(player);}
    void IStateMachine<T>.Update(){foreach (var state in player_states) state.Update(player);}
    public void FixedUpdate(){foreach (var state in player_states) state.FixedUpdate(player);}
    public void LateUpdate(){foreach (var state in player_states) state.LateUpdate(player);}
    public void OnAnimatorIK(int layerIndex){foreach (var state in player_states) state.OnAnimatorIK(layerIndex, player);}
    public void OnAnimatorMove(){foreach (var state in player_states) state.OnAnimatorMove(player);}
    public void StartLogic(T state) => state.StartLogic(player);
    
}
