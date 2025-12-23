using TMPro;
using UnityEngine;
using Valve.VR;
public class TabletCanvas : MonoBehaviour
{
    public GameObject mapUI;
    public GameObject tasksUI;
    public TextMeshProUGUI distanceText;
    public Roulette roulette;
    public MapCanvas canvas;
    private SteamVR_Action_Boolean action = SteamVR_Input.actionsBoolean[0];
    void OnEnable()
    {
        canvas.onPlace.AddListener(PlaceText);
        canvas.removeText.AddListener(removeText);
        foreach (var el in SteamVR_Input.actionsBoolean)
        {
            if (el.GetShortName() == "InteractUI" || el.GetShortName() == "TryTakePhoto")
            {
                action = el;
                break;
            }
        }
    }

    private void OnDisable()
    {
        canvas.onPlace.RemoveListener(PlaceText);
        canvas.removeText.RemoveListener(removeText);
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
    private void removeText()
    {
        distanceText.text = string.Empty;
    }
    private void PlaceText()
    {
        distanceText.text = "���������: " + roulette.DistanceBetweenPoints.ToString();
    }
}
