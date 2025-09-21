using System.Collections;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public int pointOfReference = 0;
    private Coroutine checkingCoroutine;

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

    private IEnumerator Checking()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.T))
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