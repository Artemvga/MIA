using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxiScriptRunner : MonoBehaviour
{
    public TaxiScript taxiScript;
    public CarAIController carController;
    private bool routeStarted = false; 

    private void OnEnable()
    {
        taxiScript.ComputeRoute();
    }

    private void Update()
    {
        if (!routeStarted)
        {
            if (taxiScript.coroutines > 0)
            {
                carController.isCarControlledByAI = false;
            }
            else
            {
                carController.isCarControlledByAI = true;
                carController.nextCheckpoint = taxiScript.bestRoute[0];
                routeStarted = true;
            }
        }
    }
}
