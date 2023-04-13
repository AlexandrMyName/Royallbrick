using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum PvpMode { WithPvp,NoPvp}
public class NotPVP : MonoBehaviour
{
    [SerializeField] PvpMode mode;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Character_Player")
        {
            if (!other.gameObject.GetComponent<Player>().isLocalPlayer) return;

            if(mode == PvpMode.NoPvp)
            other.gameObject.GetComponent<Container>().AllowPVP = false;
            else if(mode == PvpMode.WithPvp)
                other.gameObject.GetComponent<Container>().AllowPVP = true;
        }
    }
}
