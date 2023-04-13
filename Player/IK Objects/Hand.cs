using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hand : MonoBehaviour
{
    [SerializeField] GameObject target;
    bool stayRight;
    Transform player;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerRight")
        {
            other.gameObject.GetComponentInParent<Container>().TargetHandIKP1 = target;
            other.gameObject.GetComponentInParent<Container>().IsRightHandIK = true;
            stayRight = true;
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PlayerRight")
        {

            stayRight = true;
            player = other.gameObject.GetComponentInParent<Transform>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerRight")
        {
            other.gameObject.GetComponentInParent<Container>().IsRightHandIK = false;
            stayRight = false;
        }
    }
    private void Update()
    {
        if (!stayRight) return;
        target.transform.position = new Vector3(target.transform.position.x,
           target.transform.position.y, player.transform.position.z);
    }
}
