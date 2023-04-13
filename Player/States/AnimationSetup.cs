using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSetup<T>: StateAbstract<T>  where T : Player
{

    Container playerC;
    public override void Awake(T player)
    {
       // if (!playerC.isLocalPlayer) return;// [MIRROR]
        playerC = player.Container;
    }

    public override void Update(T callBack)
    {

        // if (!callBack.isLocalPlayer) return;// [MIRROR]

        if (playerC.AllowMove)
        {
            playerC.Animator.SetFloat("DOWNWALL", Input.GetAxis("Vertical"));

            if (!playerC.Rb.isKinematic){

                playerC.Animator.SetFloat("Horizontal", playerC.Horizontal, 0.1f, Time.deltaTime);
                playerC.Animator.SetFloat("Vertical", playerC.Vertical, 0.1f, Time.deltaTime);
            }
            else{

                playerC.Animator.SetFloat("Horizontal", 0);
                playerC.Animator.SetFloat("Vertical", 0);
            }
        }
        else
        {
            playerC.Animator.SetFloat("Horizontal", 0);
            playerC.Animator.SetFloat("Vertical", 0);
        }
    }
   
}


