using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Radio : MonoBehaviour
{
    private bool _isHanded;
    private bool _isPressed;

    public void SetIsHanded(bool isHanded) => _isHanded = isHanded;

    private void Start()
    {
        StartCoroutine(PressedRadio());
    }

    private SteamVR_Action_Boolean action = SteamVR_Input.actionsBoolean[0];
    private void OnEnable()
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
    private IEnumerator PressedRadio()
    {
        while (true)
        {
            if (_isHanded && action.GetStateDown(SteamVR_Input_Sources.Any) && _isPressed == false)
            {
                _isPressed = true;
                Debug.Log("Кнопка нажата");
                Base.instance.Radio();
                break;
            }
            yield return null;
        }
    }
}
