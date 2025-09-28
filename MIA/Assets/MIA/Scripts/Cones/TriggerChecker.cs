using System;
using UnityEngine;

public class TriggerChecker : MonoBehaviour
{
    private int triggerCount=0;
    [SerializeField] private GameObject[] orangeTriggers;
    [SerializeField] private GameObject redTrigger;

    void Update()
    {
        triggerCount = 0;
        foreach (GameObject trigger in orangeTriggers)
        {
            if(trigger.GetComponent<OrangeConeTrigger>().OrangeIn)
                    triggerCount++;
        }
        if (redTrigger.GetComponent<RedConeTrigger>().RedIn)
            triggerCount++;
        if (triggerCount == 4)
        {
            Debug.Log("Все на месте");
        }
    }
}
