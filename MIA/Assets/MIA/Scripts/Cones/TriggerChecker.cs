using System.Collections.Generic;
using UnityEngine;

public class TriggerChecker : MonoBehaviour
{
    private int triggerCount = 0;
    [SerializeField] private List<GameObject> orangeTriggers;
    [SerializeField] private GameObject redTrigger;

    [SerializeField] private List<GameObject> defaultCheckpoints;
    [SerializeField] private List<GameObject> afterPlacingCheckpoints;

    void Update()
    {
        triggerCount = 0;
        foreach (GameObject trigger in orangeTriggers)
        {
            if(trigger.GetComponent<OrangeConeTrigger>().OrangeIn)
                    triggerCount++;
        }
        if (redTrigger.GetComponent<RedConeTrigger>().RedIn)
        {
            triggerCount++;
            foreach (var obj in afterPlacingCheckpoints)
            {
                obj.SetActive(true);
            }
            foreach (var obj in defaultCheckpoints)
            {
                obj.SetActive(false);
            }
        }
        else if (triggerCount == 0)
        {
            foreach (var obj in afterPlacingCheckpoints)
            {
                obj.SetActive(false);
            }
            foreach (var obj in defaultCheckpoints)
            {
                obj.SetActive(true);
            }
        }

        if (triggerCount == 4)
        {
            Debug.Log("Все на месте");
        }
    }
}
