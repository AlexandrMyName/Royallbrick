using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentManagment : MonoBehaviour
{
 
    [SerializeField] bool isMobile = false;

    private float catch_money;
    private float health = 100f;




    public CurrentManagment management;
    public bool IsMobile { get => isMobile; set => isMobile = value; }
    
    void Awake()
    {
        health = 100f;
        if (management == null)
        {
            management = this;
            DontDestroyOnLoad(this);
        }
        else{
            
            Destroy(this);
            Debug.Log( "Mobile: " + management.IsMobile);
        }
    }
}
