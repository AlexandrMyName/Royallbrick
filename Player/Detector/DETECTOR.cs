using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DETECTOR : MonoBehaviour
{
    //Player player;
    Container playerC;

    void Start() => playerC = GetComponent<Container>();
    

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Helper" && other.isTrigger)
        {
            int index = playerC.Helpers.FindIndex(x => x.gameObject == other.gameObject);
            if (index == -1) playerC.Helpers.Add(other);
        }

        if (other.gameObject.tag == "JumpingDown" && other.isTrigger)
        {
            Debug.Log("SSS");
            playerC.Animator.SetTrigger("JumpingDown");
        }
        if(other.gameObject.tag == "Fall_Start" && other.isTrigger)
        {
            playerC.Animator.SetTrigger("Fall_Start");
        }
        if (other.gameObject.tag == "Fall_End" && other.isTrigger)
        {
            playerC.Animator.SetTrigger("Fall_End");
        }

       
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Helper" && other.isTrigger)
        {
            int index = playerC.Helpers.FindIndex(x => x.gameObject == other.gameObject);
            if (index != -1) playerC.Helpers.RemoveAt(index);
        }
       
    }

}
