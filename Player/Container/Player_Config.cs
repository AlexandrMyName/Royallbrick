using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player_Config",menuName ="Characters/Configs/Player_Data")]
public class Player_Config : ScriptableObject
{
    public float cameraSpeed = 2.12f;
    public float cameraCLAMP_Min = -110f;
    public float cameraCLAMP_Max = 47f;
    public float cameraMaxDistance = 4f;
    public float cameraMinDistanceToPlayer = 1.4f;
   
    [Space(5f)] public Vector3 cameraOffSet = new Vector3(0,2,-3);
    
    [Range(0, 3f)] public float footIK_OffSet = 3f;
    [Range(0, 3f)] public float footIK_DistanceToGround = 0.07f;

    [Range(0, 1f)] public float weight_MAIN;
    [Range(0, 1f)] public float weight_BODY;
    [Range(0, 1f)] public float weight_HEAD;
    [Range(0, 1f)] public float weight_EYES;
    [Range(0, 1f)] public float weight_CLAMP;


    public float speed = 2f;
    public float speed_Sprint = 6f;
    public float angularSpeed = 450f;

    public float speedJump = 222f;
}
