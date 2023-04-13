using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump<T>: StateAbstract<T>  where T : Player
{
    Container playerC;
    bool canJump;
    public override void Awake(T player) => playerC = player.Container;

    public override void Update(T callBack){
        playerC.Animator.SetBool("IsGround", playerC.IsGround);
        JumpStart();
        DoubleJumpStart();
    }
    void JumpStart(){
        if (Input.GetKeyDown(KeyCode.Space)  && !playerC.IsJump && playerC.IsGround){
            playerC.IsDoubleJump = false;
            playerC.Animator.SetBool("IsJump" , true);
            playerC.IsJump = true;
            canJump = true;
            JumpPhysics();
        }
    }
    void JumpPhysics() => playerC.Rb.AddForce(Vector3.up * playerC.SpeedJump, ForceMode.Impulse);
    void DoubleJumpStart(){
        if (Input.GetKeyDown(KeyCode.Space) && playerC.IsDoubleJump && !playerC.IsGround && canJump){
            playerC.Animator.SetBool("IsDubleJump", true);
            JumpPhysics();
            canJump = false;
        }
    }
}


