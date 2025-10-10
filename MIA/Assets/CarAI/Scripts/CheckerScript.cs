using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerScript : MonoBehaviour
{
    public List<StopScript> stopScripts;


    void OnTriggerEnter(Collider c)
    {
        CarAIController controller = c.GetComponent<CarAIController>();

        if(controller)
        {
            for(int i = 0; i < stopScripts.Count; i++)
                stopScripts[i].stop = true;
        }
    }

    void OnTriggerExit(Collider c)
    {
        CarAIController controller = c.GetComponent<CarAIController>();

        if(controller)
        {
            for(int i = 0; i < stopScripts.Count; i++)
                stopScripts[i].stop = false;
        }
    }
}
