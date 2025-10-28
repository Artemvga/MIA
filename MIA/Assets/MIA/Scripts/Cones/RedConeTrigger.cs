using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedConeTrigger : MonoBehaviour
{
    private bool redIn = false;
    public bool RedIn {get {return redIn;}}

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.name == "RedCone")
        {
            if ( Math.Abs(collider.gameObject.transform.rotation.x)<5 && Math.Abs(collider.gameObject.transform.rotation.z)<5)
                    {
                        if (!redIn)
                        {
                            redIn = true;
                            //triggerChecker.triggerCount++;
                        }
                    }
                    else
                    {
                        redIn = false;
                        //triggerChecker.triggerCount--;
                    }
        }
        
        //Debug.Log(Math.Abs(collider.gameObject.transform.rotation.x));
        //Debug.Log(Math.Abs(collider.gameObject.transform.rotation.z)<5);
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "RedCone")
                redIn = false;
    }
    
}
