using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Container :  NetworkBehaviour
{

    [SerializeField] Slider healthBar;

    [SerializeField] GameObject sword;
    [SerializeField] GameObject sword_Decorator;

    [SyncVar]
    bool isSword;



    bool isBow;
    bool isGun;
    bool isLookAtWeight;

    #region DATA

    IHpModel hpModel;
    IHpViewModel hpViewModel;
    [SerializeField] HpView hpView;

    float health;
    float getDamage;

    #region CONFIG
    [SerializeField] Player_Config playerConfig;
    float cameraSpeed,cameraCLAMP_Min,cameraCLAMP_Max,cameraMaxDistance,cameraMinDistanceToPlayer;
    Vector3 cameraOffSet;
    [Range(0, 3f)] float footIK_OffSet;
    [Range(0, 3f)] float footIK_DistanceToGround;
    [Range(0, 1f)] float weight_MAIN;
    [Range(0, 1f)] float weight_BODY;
    [Range(0, 1f)] float weight_HEAD;
    [Range(0, 1f)] float weight_EYES;
    [Range(0, 1f)] float weight_CLAMP;

    float speed, speed_Sprint, angularSpeed, speedJump;
    #endregion

    [SerializeField] List<GameObject> otherPlayers;
    CurrentManagment management;
    SyncActions syncActions;
    PVP_Lobby pvp;
    Player player;
    Vector3 position;
    Quaternion rotation;
    Transform tr;
    Animator animator;
    Rigidbody rb;
    CapsuleCollider capsule;
    Camera main_Camera;
    GameObject game_CameraGM;
    BoxCollider box;
    AudioSource audioS;

    [SerializeField] LayerMask camera_LAYER_Main;
    [SerializeField] LayerMask camera_LAYER_NoPlayer;
    [SerializeField] LayerMask camera_LAYER_WithPlayer;

    [SerializeField] GameObject bullet;
    
    [SerializeField,Space(5f),Header("Инверсная кинематика ног")] LayerMask footIK_Layer;
    [SerializeField, Range(0,1f)] float weight_LeftFoot;
    [SerializeField, Range(0, 1f)] float weight_RightFoot;

    [SerializeField] GameObject targetHandIKP;

    #region FLAGS
    bool isRightHandIK;
    bool isWalk;
    bool isJump;
    bool isDoubleJump;
    bool isGround;
    bool isWall;
    bool isJumpForWall;
    bool allowMove;
    bool allowPVP;
    bool onPVP_area;
    #endregion

    [SerializeField] LayerMask jumpLayer;

    #region PARKUR
    [SerializeField, Space] List<Collider> helpers;
    [SerializeField] LayerMask wallParkurLayer;
    #endregion

    #region Sounds
    [SerializeField, Header("SOUNDS"), Space(5)] AudioClip stepGLUXOI;
    #endregion

    #endregion

    #region Property


    #region Base components
    public Vector3 Position { get => position; set => position = value; }
    public Quaternion Rotation { get => rotation; set => rotation = value; }
    public Transform Transform { get => tr; set => tr = value; }
    public Animator Animator { get => animator; set => animator = value; }
    public Rigidbody Rb { get => rb; set => rb = value; }
    public CapsuleCollider Capsule { get => capsule; set => capsule = value; }

    #endregion

    #region Inputs
    float mouseX;
    float mouseY;
    float horizontal;
    float vertical;
    bool mouseButton_Right;
    bool mouseButtonDown_Right;
    bool mouseButtonUp_Right;
    bool mouseButton_Left;
    bool mouseButtonDown_Left;
    bool mouseButtonUp_Left;
    #endregion

    #region Player config
    public float Health { get => health; set => health = value; }
    public float GetDamage { get => getDamage; set => getDamage = value; }
    public IHpModel HpModel { get => hpModel; set => hpModel = value; }
    public IHpViewModel HpViewModel { get => hpViewModel; set => hpViewModel = value; }
    public HpView HpView { get => hpView; set => hpView = value; }

    #endregion

    #region IK
    //Foot IK
    public LayerMask FootIK_Layer { get => footIK_Layer; set => footIK_Layer = value; }
    public float FootIK_OffSet { get => footIK_OffSet; set => footIK_OffSet = value; }
    public float FootIK_DistanceToGround { get => footIK_DistanceToGround; set => footIK_DistanceToGround = value; }
    public float Weight_LeftFoot { get => weight_LeftFoot; set => weight_LeftFoot = value; }
    public float Weight_RightFoot { get => weight_RightFoot; set => weight_RightFoot = value; }

    //Body and head IK
    public float Weight_MAIN { get => playerConfig.weight_MAIN; set => playerConfig.weight_MAIN = value; }
    public float Weight_BODY { get => playerConfig.weight_BODY; set => playerConfig.weight_BODY = value; }
    public float Weight_HEAD { get => playerConfig.weight_HEAD; set => playerConfig.weight_HEAD = value; }
    public float Weight_EYES { get => playerConfig.weight_EYES; set => playerConfig.weight_EYES = value; }
    public float Weight_CLAMP { get => playerConfig.weight_CLAMP; set => playerConfig.weight_CLAMP = value; }
    #endregion
    public GameObject Bullet { get => bullet; set => bullet = value; }
    #region Camera
    public Camera Main_Camera { get => main_Camera; set => main_Camera = value; }
    public GameObject Game_CameraGM { get => game_CameraGM; set => game_CameraGM = value; }
    public Vector3 CameraOffSet { get => cameraOffSet; set => cameraOffSet = value; }

    #endregion

    #region Inputs
    public float MouseX { get => mouseX; set => mouseX = value; }
    public float MouseY { get => mouseY; set => mouseY = value; }
    public float Horizontal { get => horizontal; set => horizontal = value; }
    public float Vertical { get => vertical; set => vertical = value; }
    public bool MouseButton_Right { get => mouseButton_Right; set => mouseButton_Right = value; }
    public bool MouseButtonDown_Right { get => mouseButtonDown_Right; set => mouseButtonDown_Right = value; }
    public bool MouseButtonUp_Right { get => mouseButtonUp_Right; set => mouseButtonUp_Right = value; }
    public bool MouseButton_Left { get => mouseButton_Left; set => mouseButton_Left = value; }
    public bool MouseButtonDown_Left { get => mouseButtonDown_Left; set => mouseButtonDown_Left = value; }
    public bool MouseButtonUp_Left { get => mouseButtonUp_Left; set => mouseButtonUp_Left = value; }
    public float CameraSpeed { get => cameraSpeed; set => cameraSpeed = value; }
    public float CameraCLAMP_Min { get => cameraCLAMP_Min; set => cameraCLAMP_Min = value; }
    public float CameraCLAMP_Max { get => cameraCLAMP_Max; set => cameraCLAMP_Max = value; }
    public float CameraMaxDistance { get => cameraMaxDistance; set => cameraMaxDistance = value; }
    public LayerMask Camera_LAYER_Main { get => camera_LAYER_Main; set => camera_LAYER_Main = value; }
    public LayerMask Camera_LAYER_NoPlayer { get => camera_LAYER_NoPlayer; set => camera_LAYER_NoPlayer = value; }
    public LayerMask Camera_LAYER_WithPlayer { get => camera_LAYER_WithPlayer; set => camera_LAYER_WithPlayer = value; }
    public float CameraMinDistanceToPlayer { get => cameraMinDistanceToPlayer; set => cameraMinDistanceToPlayer = value; }
    public float Speed { get => speed; set => speed = value; }
    public float AngularSpeed { get => angularSpeed; set => angularSpeed = value; }
    public float Speed_Sprint { get => speed_Sprint; set => speed_Sprint = value; }
    public AudioClip StepGLUXOI { get => stepGLUXOI; set => stepGLUXOI = value; }
    public AudioSource AudioS { get => audioS; }
    public GameObject TargetHandIKP1 { get => targetHandIKP; set => targetHandIKP = value; }
    public bool IsRightHandIK { get => isRightHandIK; set => isRightHandIK = value; }
    public bool IsWalk { get => isWalk; set => isWalk = value; }
    public bool IsDoubleJump { get => isDoubleJump; set => isDoubleJump = value; }
    public bool IsJump { get => isJump; set => isJump = value; }
    public float SpeedJump { get => speedJump; set => speedJump = value; }
    public bool IsGround { get => isGround; set => isGround = value; }
    public LayerMask JumpLayer { get => jumpLayer; set => jumpLayer = value; }
    public BoxCollider Box { get => box; set => box = value; }
    public List<Collider> Helpers { get => helpers; set => helpers = value; }
    public bool IsWall { get => isWall; set => isWall = value; }
    public bool IsJumpForWall { get => isJumpForWall; set => isJumpForWall = value; }
    public LayerMask WallParkurLayer { get => wallParkurLayer; set => wallParkurLayer = value; }
    public CurrentManagment Management { get => management; set => management = value; }
    public List<GameObject> OtherPlayers { get => otherPlayers; set => otherPlayers = value; }
    public bool AllowMove { get => allowMove; set => allowMove = value; }
    public PVP_Lobby Pvp { get => pvp; set => pvp = value; }
    public bool OnPVP_area { get => onPVP_area; set => onPVP_area = value; }
    public bool IsSword { get => isSword; set => isSword = value; }
    public GameObject Sword { get => sword; set => sword = value; }
    public GameObject Sword_Decorator { get => sword_Decorator; set => sword_Decorator = value; }
    public SyncActions SyncActions { get => syncActions; set => syncActions = value; }
    public bool IsLookAtWeight { get => isLookAtWeight; set => isLookAtWeight = value; }
    public Slider HealthBar { get => healthBar; set => healthBar = value; }
    public bool AllowPVP { get => allowPVP; set => allowPVP = value; }
   


    #endregion

    #endregion

    public Container Create(){
        allowPVP = true;
        cameraSpeed = playerConfig.cameraSpeed;
        cameraCLAMP_Min = playerConfig.cameraCLAMP_Min;
        cameraCLAMP_Max = playerConfig.cameraCLAMP_Max;
        cameraMaxDistance = playerConfig.cameraMaxDistance;
        cameraMinDistanceToPlayer = playerConfig.cameraMinDistanceToPlayer;
        cameraOffSet = playerConfig.cameraOffSet;
        footIK_DistanceToGround = playerConfig.footIK_DistanceToGround;
        weight_MAIN = playerConfig.weight_MAIN;
        weight_BODY = playerConfig.weight_BODY;
        weight_HEAD = playerConfig.weight_HEAD;
        weight_EYES = playerConfig.weight_EYES;
        weight_CLAMP = playerConfig.weight_CLAMP;
        speed = playerConfig.speed;
        speed_Sprint = playerConfig.speed_Sprint;
        angularSpeed = playerConfig.angularSpeed;
        speedJump = playerConfig.speedJump;
        //
        helpers = new List<Collider>();
        audioS = this.gameObject.transform.GetChild(4).gameObject.GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        box = GetComponent<BoxCollider>();
      
        game_CameraGM =  this.gameObject.transform.GetChild(3).gameObject;
        main_Camera =  game_CameraGM.transform.GetChild(0).GetComponent<Camera>();

        position = player.transform.position;
        rotation = player.transform.rotation;
        this.tr =  player.transform;

        health = 100f;
        getDamage = 10f;
        syncActions = GameObject.Find("LOGIC_SCENES").transform.GetChild(1).GetComponent<SyncActions>();
        if (!syncActions) Debug.Log("NULL");
         return this;
    }
    private void Awake(){
        player = GetComponent<Player>();
        Create();
    }
    private void Update(){
        if (!isLocalPlayer)
            game_CameraGM.SetActive(false);
            else
                game_CameraGM.SetActive(true);
    }
}
