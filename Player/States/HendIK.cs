using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HendIK<T>: StateAbstract<T>  where T : Player
{
    Container playerC;

    float RightWeight;
    float LeftWeight;
    public override void Awake(T player) => playerC = player.Container;
    public override void Update(T player){
        RightWeight = 1f;
        LeftWeight = 1f;
    }
    public override void OnAnimatorIK(int layerIndex, T player)
    {
        // 1 - SWORD 
        if(playerC.Animator.GetLayerWeight(1) == 1) return;
        playerC.Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, RightWeight);
        playerC.Animator.SetIKRotationWeight(AvatarIKGoal.RightHand, RightWeight);

        Ray ray = new Ray();
        RaycastHit hit;

       //Реализована правая рука
        if(playerC.IsRightHandIK)
        FootIKSet(out hit, ray, AvatarIKGoal.RightHand);
    }
    private void FootIKSet(out RaycastHit hit, Ray ray, AvatarIKGoal ikGoal){
        ray = new Ray(playerC.Animator.GetIKPosition(ikGoal),
            playerC.TargetHandIKP1.transform.position);
      
            Vector3 newPosition = playerC.TargetHandIKP1.transform.position;
            Quaternion newRotation = playerC.TargetHandIKP1.transform.rotation;
            hit = new RaycastHit();

            playerC.Animator.SetIKPosition(ikGoal, newPosition);
            playerC.Animator.SetIKRotation(ikGoal, newRotation);
    }
}


