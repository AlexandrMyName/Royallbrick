using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    Container container;
    private void Start()
    {
        container = GetComponent<Player>().Container;
    }
    public void Step(AnimationEvent evt)
    {
        if(evt.animatorClipInfo.weight > 0.5f)
        {
            container.AudioS.PlayOneShot(container.StepGLUXOI);
        }
        //if(!container.AudioS.isPlaying)
       // container.AudioS.PlayOneShot(container.StepGLUXOI);

    }
}
