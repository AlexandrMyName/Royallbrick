using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetup<T>: StateAbstract<T>  where T : Player
{
    Container playerC;
    const float camera_offset_UP = 1.6f;//Высота игрока
    Quaternion rotation;
    float speed;
    public override void Awake(T player){
        playerC = player.Container;
        speed = playerC.CameraSpeed;
    }
    public override void Start(T callBack) =>  playerC.Main_Camera.transform.position = 
        playerC.Game_CameraGM.transform.TransformPoint(playerC.CameraOffSet);
    public override void Update(T player){

        if (!playerC.AllowMove) return;

        playerC.Game_CameraGM.transform.position = player.transform.position + Vector3.up * camera_offset_UP;
        RaycastHit hit;
        Ray ray = new Ray(playerC.Game_CameraGM.transform.position,
            playerC.Main_Camera.transform.position - playerC.Game_CameraGM.transform.position
            );
        if (Physics.Raycast(ray, out hit, playerC.CameraMaxDistance, playerC.Camera_LAYER_Main))
         playerC.Main_Camera.transform.position = hit.point;
            else playerC.Main_Camera.transform.position = playerC.Game_CameraGM.transform.TransformPoint(playerC.CameraOffSet);

        if(Vector3.Distance(
            playerC.Game_CameraGM.transform.position,
            playerC.Main_Camera.transform.position ) < playerC.CameraMinDistanceToPlayer)
                    playerC.Main_Camera.cullingMask = playerC.Camera_LAYER_NoPlayer;
            else playerC.Main_Camera.cullingMask = playerC.Camera_LAYER_WithPlayer;
    }
    public override void LateUpdate(T callBack){

        if (!playerC.AllowMove) return;

        rotation.x += -playerC.MouseY * speed;
        rotation.y += playerC.MouseX * speed;
        rotation.x = Mathf.Clamp(rotation.x, playerC.CameraCLAMP_Min, playerC.CameraCLAMP_Max);
        rotation.z = 0;
        playerC.Game_CameraGM.transform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
        playerC.Main_Camera.transform.LookAt(playerC.Game_CameraGM.transform);
    }

}


