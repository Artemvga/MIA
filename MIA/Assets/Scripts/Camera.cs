using System.Collections;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Camera : MonoBehaviour
{

    public Throwable _camera;
    public int pointOfReference = 0;
    private Coroutine checkingCoroutine;
    private bool _isHanded;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CameraTrigger"))
        {
            pointOfReference = int.Parse(other.name);
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

                float yRotation = transform.rotation.eulerAngles.y;
                float xRotation = transform.rotation.eulerAngles.x;

                if (((pointOfReference - 30) < yRotation && yRotation < (pointOfReference + 30)) &&
                    (-20 < xRotation && xRotation < 20))
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