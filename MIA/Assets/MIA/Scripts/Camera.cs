using System.Collections;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Camera : MonoBehaviour
{

    public int pointOfReference = 0;
    private Coroutine checkingCoroutine;
    private bool _isHanded;
    private bool truePosition = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CameraTrigger"))
        {
            truePosition = true;
            checkingCoroutine = StartCoroutine(Checking());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CameraTrigger"))
        {
            if (checkingCoroutine != null)
            {
                StopCoroutine(checkingCoroutine);
                checkingCoroutine = null;
                truePosition = false;
            }
        }
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

    public void SetIsHanded(bool isHanded) => _isHanded = isHanded;

    private IEnumerator Checking()
    {

        while (true)
        {
            if (_isHanded && action.GetStateDown(SteamVR_Input_Sources.Any))
            {
                Debug.Log("Кнопка нажата");

                if (truePosition == true)
                {
                    Debug.Log("Камера в правильном положении!");
                }
                else
                {
                    Debug.Log("Камера в неправильном положении!");
                }
            }
            yield return null;
        }
    }
}