using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parkur<T>: StateAbstract<T>  where T : Player
{
    Container playerC;
    Collider collMove;
    Vector3 newPoint;
    bool isUp = false;


    public override void Awake(T player)
    {

        //if (!playerC.isLocalPlayer) return;// [MIRROR]
        playerC = player.Container;
    }
    
    public override void OnAnimatorMove(T player){

       // if (!player.isLocalPlayer) return;// [MIRROR]

        if (!playerC.IsJumpForWall && !isUp && !playerC.IsSword){

            if (Input.GetKey(KeyCode.W)  && !isUp )
            {

                    if (playerC.IsWall && !playerC.IsJumpForWall && !Physics.Raycast(player.transform.position + Vector3.up * 1.5f, player.transform.forward, 1f)){

                    playerC.Animator.SetBool("OnWall", false);
                    isUp = true;
                        newPoint = player.transform.position + Vector3.up * 1.2f + player.transform.forward * 1f;
                       
                    }
                    else{

                        Collider coll = null;

                        for (int i = 0; i < playerC.Helpers.Count; i++){
                            if (Quaternion.Angle(player.transform.rotation, playerC.Helpers[i].transform.rotation) < 50){
                                if (coll == null) coll = playerC.Helpers[i];
                            
                                else if (playerC.Helpers[i].bounds.max.y > coll.bounds.max.y)
                                                  coll = playerC.Helpers[i];
                            }
                        }
                        if (coll != null){
                            Ray ray = new Ray(
                                new Vector3(player.transform.position.x, coll.bounds.max.y - 0.1f, player.transform.position.z),
                                new Vector3(coll.bounds.center.x, coll.bounds.max.y - 0.1f, coll.bounds.center.z) -
                                new Vector3(player.transform.position.x, coll.bounds.max.y - 0.1f, player.transform.position.z)
                                );
                            RaycastHit hit;

                            if (Physics.Raycast(ray, out hit, 0.7f, playerC.WallParkurLayer)){

                                playerC.IsJumpForWall = true;
                                playerC.Rb.isKinematic = true;
                                if (!playerC.IsWall){
                                    playerC.IsWall = true;
                                    playerC.Animator.SetBool("OnWall", true);
                                }
                                else{
                                    playerC.Animator.SetBool("JumpWall", true);
                                    playerC.Animator.SetTrigger("JumpWallTrigger");
                                }
                                collMove = coll;
                                newPoint = new Vector3(
                                    hit.point.x, coll.bounds.max.y, hit.point.z) - coll.transform.forward * 0.36f + Vector3.up * -1.17f;
                            }
                        }
                    }
                }
            }
            if (playerC.IsJumpForWall && !isUp ){
                    if (Vector3.Distance(player.transform.position, newPoint) > 0.02f){

                        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, collMove.transform.rotation, 5f * Time.deltaTime);
                        player.transform.position = Vector3.Slerp(
                            player.transform.position, 
                            
                            newPoint,
                            
                            7f * Time.deltaTime);
                    }
                    else{
                        playerC.IsJumpForWall = false;
                        playerC.Animator.SetBool("JumpWall", false);
                    }
            }
            if (playerC.IsWall && isUp )
            {
                        if (Vector3.Distance(player.transform.position, newPoint) > 0.02f){

                                if (player.transform.position.y < (newPoint.y - 0.1f)){
                                    player.transform.position = Vector3.Slerp(player.transform.position,
                                        new Vector3(player.transform.position.x, newPoint.y+4, player.transform.position.z),
                                        1.54f * Time.deltaTime);
                                }
                                else player.transform.position = Vector3.Slerp(player.transform.position, newPoint, 5f * Time.deltaTime);
                        }
                        else{
                            isUp = false;
                            playerC.Rb.isKinematic = false;
                            playerC.IsWall = false;
                            playerC.IsJumpForWall = false;
                        }
            }
            if (playerC.IsWall && !isUp){
                
                float h = Input.GetAxis("Horizontal");
                bool isWalk = false;

                if (h != 0) {
                    if (collMove != null){

                        float dir = (h > 0) ? 0.5f : -0.5f;
                        RaycastHit hit;
                        if (Physics.Raycast(player.transform.position + player.transform.up * 1.12f + player.transform.right * dir,
                            player.transform.forward, out hit, 1f)){

                            if (hit.collider == collMove){

                                player.transform.position += player.transform.right * h * 2f * Time.deltaTime;
                                isWalk = true;
                                playerC.Animator.SetFloat("MoveWall", h);
                            }

                        }
                    }
                }
                playerC.Animator.SetBool("MoveWalk", isWalk);
            }
    }
}


