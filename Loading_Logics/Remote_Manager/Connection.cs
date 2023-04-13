using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection : MonoBehaviour
{
    
   [SerializeField] NetworkManager networkManager;
    void Start()
    {

       

        if (!Application.isBatchMode)
          networkManager.StartClient();
        
        
        //networkManager.networkAddress = "localhost";
       // networkManager.StartClient();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
