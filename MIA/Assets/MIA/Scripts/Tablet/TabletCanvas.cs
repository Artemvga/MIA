using UnityEngine;
using Valve.VR;
public class TabletCanvas : MonoBehaviour
{
    public GameObject mapUI;
    public GameObject tasksUI;
    private SteamVR_Action_Boolean action = SteamVR_Input.actionsBoolean[0];
    void OnEnable()
    {
        foreach (var el in SteamVR_Input.actionsBoolean)
        {
            if (el.GetShortName() == "TryTakePhoto")
            {
                action = el;
                break;
            }
        }
    }
    // Start is called before the first frame update
    public void HoldTabletUpdate()
    {
        if (action.GetStateDown(SteamVR_Input_Sources.Any))
        {
            if (mapUI.activeSelf)
            {
                mapUI.SetActive(false);
                tasksUI.SetActive(true);
            }
            else
            {
                mapUI.SetActive(true);
                tasksUI.SetActive(false);
            }
        }
    }
}
