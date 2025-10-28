using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    [Tooltip("The speed limit applied to the ai when the car touches the checkpoint. If this value is -1 the script won't modify the speed limit of the car.")]
    public int speedLimit;

    [Tooltip("List of the next checkpoints. If you add more that one checkpoint, the ai will choose one randomly.")]
    public List<Transform> nextCheckpoints = new List<Transform> ();

    private int _staticSpeedlimit = 45;

    void Awake()
    {
        if (_staticSpeedlimit != 0 && speedLimit != 30) { speedLimit = _staticSpeedlimit; }
        for(int i = 0; i < nextCheckpoints.Count; i++)
        {
            if(nextCheckpoints[i] == null)
            {
                nextCheckpoints.RemoveAt(i);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CarAIController controller = other.GetComponent<CarAIController>();

        if(controller && controller.nextCheckpoint.gameObject == transform.gameObject)
        {
            if(speedLimit != -1)
                controller.speedLimit = speedLimit;

            if(controller.taxiMode)
            {
                TaxiScript taxiScript = other.GetComponent<TaxiScript>();
                if(taxiScript)
                {
                    if(taxiScript.bestRoute[taxiScript.bestRoute.Count - 1] == transform)
                    {
                        controller.speedLimit = 0;
                    }
                    else
                    {
                        int index = taxiScript.bestRoute.IndexOf(transform);
                        controller.nextCheckpoint = taxiScript.bestRoute[index+1];
                    }
                }
            }
            else
            {
                int index = Random.Range(0, nextCheckpoints.Count);

                if (nextCheckpoints.Count > 0)
                {
                    Transform next = nextCheckpoints[index];
                    if (next.gameObject.activeSelf == false)
                    {
                        foreach (var c in nextCheckpoints)
                        {
                            if (c.gameObject.activeSelf == true)
                            {
                                next = c;
                                break;
                            }
                        }
                    }

                    controller.nextCheckpoint = next;
                }
                else
                {
                    Destroy(controller.gameObject);
                }
            }

        }
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for(int i=0; i < nextCheckpoints.Count; i++)
        {
            Gizmos.DrawLine(transform.position, nextCheckpoints[i].position);
        }
    }
    
    #endif
}
