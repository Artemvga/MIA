using System.Collections;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Roulette : MonoBehaviour
{
    public LineRenderer line;
    public GameObject firstPoint;
    public GameObject secondPoint;

    private SteamVR_Action_Boolean action = SteamVR_Input.actionsBoolean[0];
    private void OnEnable()
    {
        line.positionCount = 2;

        foreach (var el in SteamVR_Input.actionsBoolean)
        {
            if (el.GetShortName() == "TryTakePhoto")
            {
                action = el;
                break;
            }
        }
    }

    private void Update()
    {
        var v1 = line.GetPosition(0);
        var v2 = line.GetPosition(1);
        Debug.Log(Mathf.Sqrt((v1.x - v2.x) * (v1.x - v2.x) + (v1.y - v2.y) * (v1.y - v2.y)));
        line.SetPosition(0, firstPoint.transform.position);
        line.SetPosition(1, secondPoint.transform.position);
    }

    public void Checking()
    {
        if (action.GetStateDown(SteamVR_Input_Sources.Any))
        {
            Debug.Log("nice");
        }
    }
}
