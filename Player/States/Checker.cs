using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker<T>: StateAbstract<T>  where T : Player
{
    Container playerC;
    public override void Awake(T player)
    {
        //if (!playerC.isLocalPlayer) return;// [MIRROR]
        playerC = player.Container;
    }
    
 
    public override void Update(T callBack){
        // if (playerC.IsWall) return;
        if (!callBack.isLocalPlayer) return;// [MIRROR]

        playerC.MouseX = Input.GetAxisRaw("Mouse X");
        playerC.MouseY = Input.GetAxisRaw("Mouse Y");
        playerC.Horizontal = Input.GetAxisRaw("Horizontal");
        playerC.Vertical = Input.GetAxisRaw("Vertical");

        playerC.MouseButton_Right = Input.GetMouseButton(1);
        playerC.MouseButtonUp_Right = Input.GetMouseButtonUp(1);
        playerC.MouseButtonDown_Right = Input.GetMouseButtonDown(1);

        playerC.MouseButton_Left = Input.GetMouseButton(0);
        playerC.MouseButtonUp_Left = Input.GetMouseButtonUp(0);
        playerC.MouseButtonDown_Left = Input.GetMouseButtonDown(0);

        playerC.IsWalk = IsWalk();
        playerC.IsGround = IsGround();
    }
    bool IsWalk(){
        bool isHorizontalInput = Mathf.Approximately(Mathf.Abs(playerC.Horizontal), 1);
        bool isVerticalInput = Mathf.Approximately(Mathf.Abs(playerC.Vertical), 1);
        return  isHorizontalInput || isVerticalInput ? true : false;
    }
    bool IsGround(){
        Vector3 bottom = new Vector3(playerC.Capsule.bounds.center.x, playerC.Capsule.bounds.min.y, playerC.Capsule.bounds.center.z);
        return Physics.CheckCapsule(playerC.Capsule.bounds.center, bottom, 0.1f ,playerC.JumpLayer,QueryTriggerInteraction.Ignore);
    }
}


