using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move<T>: StateAbstract<T>  where T : Player
{
    Container playerC;
    Vector3 directionPosition;
    Quaternion rotPlayer;
    Vector3 direction;
    Vector3 Target_Rotate => playerC.Main_Camera.transform.forward * playerC.CameraMaxDistance;

    public override void Awake(T player){
       
        playerC = player.Container;
        playerC.AllowMove = true;
    }
    public override void FixedUpdate(T player){

       if (playerC.IsWall || !player.isLocalPlayer) return;
        if (!playerC.AllowMove) return;
        direction = player.transform.TransformDirection(playerC.Horizontal, 0, playerC.Vertical).normalized;
        float speedPos = 0;
        if (Input.GetKey(KeyCode.LeftShift) && player.isLocalPlayer){

            if (Mathf.Abs(playerC.Vertical) > 0 || Mathf.Abs(playerC.Horizontal) > 0)
                speedPos = playerC.Speed_Sprint;
        }
        else speedPos = playerC.Speed;

         directionPosition = direction * speedPos * Time.deltaTime;

         if (speedPos == playerC.Speed_Sprint ) 
            playerC.Animator.SetBool("RUN", true);
                    else playerC.Animator.SetBool("RUN", false);
         Vector3 directionRotate = Target_Rotate;
         directionRotate.y = 0;
         Quaternion look = Quaternion.LookRotation(directionRotate);
         float speed =  playerC.AngularSpeed * Time.deltaTime;
         rotPlayer = Quaternion.RotateTowards(player.transform.rotation, look, speed);
    }
   
    public override void OnAnimatorMove(T player){

        if (player.isLocalPlayer){

            if (!playerC.AllowMove) return;
            if (playerC.IsWall) return;
            playerC.Rb.MovePosition(player.transform.position + directionPosition);

            if (Mathf.Abs(playerC.Horizontal) > 0 || Mathf.Abs(playerC.Vertical) > 0)
                playerC.Rb.MoveRotation(rotPlayer);

            if(playerC.IsSword) playerC.Rb.MoveRotation(rotPlayer);
        }
    }
    public override void StartLogic(T player) {}
}


