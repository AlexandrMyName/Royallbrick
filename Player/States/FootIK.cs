using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootIK<T>: StateAbstract<T>  where T : Player
{
    Container playerC;
    public override void Awake(T player){
       // if (!playerC.isLocalPlayer) return;// [MIRROR]
        playerC = player.Container;
    }
    public override void Update(T player){
      //  if (!player.isLocalPlayer) return;// [MIRROR]

        playerC.Weight_RightFoot = playerC.Animator.GetFloat("Weight_RightFoot");
        playerC.Weight_LeftFoot = playerC.Animator.GetFloat("Weight_LeftFoot");
    }


    public override void OnAnimatorIK(int layerIndex, T player){

       // if (!player.isLocalPlayer) return;// [MIRROR]

        playerC.Animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, playerC.Weight_LeftFoot);
        playerC.Animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, playerC.Weight_RightFoot);

        if (!playerC.IsWalk){
            playerC.Animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, playerC.Weight_LeftFoot);
            playerC.Animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, playerC.Weight_RightFoot);
        }
        

        Ray ray = new Ray(); RaycastHit hit;

        FootIKSet(out hit,ray,AvatarIKGoal.LeftFoot);
        FootIKSet(out hit, ray, AvatarIKGoal.RightFoot);
    }

    private void FootIKSet(out RaycastHit hit, Ray ray, AvatarIKGoal ikGoal){
        ray = new Ray(playerC.Animator.GetIKPosition(ikGoal) + Vector3.up * 0.5f, Vector3.down);
        if (Physics.Raycast(ray, out hit, playerC.FootIK_DistanceToGround + playerC.FootIK_OffSet, playerC.FootIK_Layer)){
            Vector3 newPosition = hit.point;
            newPosition.y += playerC.FootIK_DistanceToGround;
            Vector3 forward = Vector3.ProjectOnPlane(playerC.Transform.forward, hit.normal);
            playerC.Animator.SetIKPosition(ikGoal, newPosition);
            playerC.Animator.SetIKRotation(ikGoal, Quaternion.LookRotation(forward, hit.normal));
        }
    }
}


