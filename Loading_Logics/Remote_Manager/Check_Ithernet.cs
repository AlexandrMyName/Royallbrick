using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Check_Ithernet : MonoBehaviour
{
    [Obsolete]
    void Start()
    {
        StartCoroutine(CheckIthernet(result =>{
            if (!result) textWarning.SetActive(true);
            else textWarning.SetActive(false);
        }));
    }

  

   [SerializeField] GameObject textWarning;
   [SerializeField] string ulr;

    [Obsolete]
    IEnumerator CheckIthernet(Action<bool> result)
    {
        UnityWebRequest web = UnityWebRequest.Get(ulr);

        
        
        yield return web.SendWebRequest();

        if(web.isNetworkError || web.isNetworkError)
        {
            result(false);
            yield break;
        }
        result(true);
    }
}
