using System.Collections;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Camera : MonoBehaviour
{
    public int pointOfReference = 0;
    private Coroutine checkingCoroutine;
    private bool _isHanded;
    private int currentTriggerId = -1;

    private bool[] successfulPhotos = new bool[4];
    private bool allPhotosCompleted = false;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CameraTrigger"))
        {
            if (int.TryParse(other.gameObject.name, out int triggerId))
            {
                currentTriggerId = triggerId;
                checkingCoroutine = StartCoroutine(Checking());
            }
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
            currentTriggerId = -1;
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

                if (currentTriggerId != -1)
                {
                    CheckRaycastDirection(currentTriggerId);
                }
                else
                {
                    Debug.Log("Камера в неправильном положении!");
                }
            }
            yield return null;
        }
    }

    private void CheckRaycastDirection(int fromTrigger)
    {
        Vector3 rayDirection = GetRayDirection(fromTrigger);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, rayDirection, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("CameraTrigger"))
            {
                if (int.TryParse(hit.collider.gameObject.name, out int toTrigger))
                {
                    TryTakePhoto(fromTrigger, toTrigger);
                    return;
                }
            }
        }

        Debug.Log("Рейкаст не попал в нужный триггер!");
    }

    private Vector3 GetRayDirection(int fromTrigger)
    {
        switch (fromTrigger)
        {
            case 0: return Vector3.forward;
            case 180: return Vector3.back;
            case 90: return Vector3.right;
            case 270: return Vector3.left;
            default: return Vector3.zero;
        }
    }

    private void TryTakePhoto(int fromTrigger, int toTrigger)
    {
        bool isValidCombination = false;
        int photoIndex = -1;

        if (fromTrigger == 0 && toTrigger == 180)
        {
            isValidCombination = true;
            photoIndex = 0;
        }
        else if (fromTrigger == 180 && toTrigger == 0)
        {
            isValidCombination = true;
            photoIndex = 1;
        }
        else if (fromTrigger == 90 && toTrigger == 270)
        {
            isValidCombination = true;
            photoIndex = 2;
        }
        else if (fromTrigger == 270 && toTrigger == 90)
        {
            isValidCombination = true;
            photoIndex = 3;
        }

        if (isValidCombination)
        {
            if (!successfulPhotos[photoIndex])
            {
                successfulPhotos[photoIndex] = true;
                Debug.Log($"Успешное фото сделано! {fromTrigger} -> {toTrigger}");
                CheckAllPhotosCompleted();
            }
            else
            {
                Debug.Log($"Фото {fromTrigger} -> {toTrigger} уже было сделано успешно!");
            }
        }
        else
        {
            Debug.Log("Неправильное направление для фото!");
        }
    }

    private void CheckAllPhotosCompleted()
    {
        if (allPhotosCompleted) return;

        foreach (bool photo in successfulPhotos)
        {
            if (!photo) return;
        }

        allPhotosCompleted = true;
        Debug.Log("Все 4 фото сделаны успешно!");

        if (Base.instance != null)
        {
            Base.instance.MakePhoto();
        }
        else
        {
            Debug.LogWarning("Base.instance не найден!");
        }
    }

    public bool AreAllPhotosTaken()
    {
        return allPhotosCompleted;
    }

    public bool IsPhotoTaken(int photoIndex)
    {
        if (photoIndex >= 0 && photoIndex < successfulPhotos.Length)
            return successfulPhotos[photoIndex];
        return false;
    }
}