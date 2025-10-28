using System;
using UnityEngine;

public class OrangeConeTrigger : MonoBehaviour
{
    private bool orangeIn = false;
    public bool OrangeIn {get {return orangeIn;}}

    private void OnTriggerStay(Collider collider)
    {
        bool isOk = Math.Abs(collider.gameObject.transform.rotation.x) < 10 &&
                    Math.Abs(collider.gameObject.transform.rotation.z) < 10;
        if (collider.gameObject.CompareTag("OrangeCone"))
        {
            if (isOk)
            {
                if (!orangeIn)
                {
                    orangeIn = true; //triggerChecker.triggerCount++;
                }
            }
            else
            {
                orangeIn = false;
                //triggerChecker.triggerCount--;
            }
        }
        
        //Debug.Log(Math.Abs(collider.gameObject.transform.rotation.x));
        //Debug.Log(Math.Abs(collider.gameObject.transform.rotation.z)<5);
    }
    
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("OrangeCone"))
            orangeIn = false;
    }
   
}
