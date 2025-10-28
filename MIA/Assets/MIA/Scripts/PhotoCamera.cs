using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PhotoCamera : MonoBehaviour
{
    [Header("Camera Settings")]
    public float rayDistance = 100f;
    public LayerMask triggerLayerMask = -1; // Все слои по умолчанию

    [Header("Debug")]
    public bool showDebugRay = true;
    public Color validRayColor = Color.green;
    public Color invalidRayColor = Color.red;
    public AudioSource audioSource;
    public AudioClip photoSound;

    // Переменные для хранения состояния фотографий
    private bool[] correctPhotos = new bool[4]; // 0: 0->180, 1: 180->0, 2: 90->270, 3: 270->90
    private bool isInStayTrigger = false;
    private StayTrigger currentStayTrigger = null;
    public bool isHanded;

    public void SetIsHanded(bool a) => isHanded = a;

    // Ссылка на линию для визуализации луча
    private LineRenderer lineRenderer;

    void Start()
    {
        // Инициализация LineRenderer для отображения луча
        InitializeLineRenderer();
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
    void InitializeLineRenderer()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = showDebugRay;
    }

    void Update()
    {
        // Обновляем отображение луча
        UpdateRayVisualization();

        // Проверяем нажатие кнопки только если в зоне StayTrigger
        if (action.GetStateDown(SteamVR_Input_Sources.Any) && isHanded)
        {
            TryTakePhoto();
        }

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.R))
        {
            TryTakePhoto();
        }
#endif
    }

    void UpdateRayVisualization()
    {
        if (!lineRenderer || !showDebugRay) return;

        Vector3 rayStart = transform.position;
        Vector3 rayDirection = -transform.right;
        Vector3 rayEnd = rayStart + rayDirection * rayDistance;

        lineRenderer.SetPosition(0, rayStart);
        lineRenderer.SetPosition(1, rayEnd);

        // Меняем цвет луча в зависимости от возможности сделать фото
        List<string> triggerSequence = GetCameraTriggerSequence();
        bool isValidPhoto = CheckPhotoValidity(triggerSequence);

        lineRenderer.startColor = isValidPhoto ? validRayColor : invalidRayColor;
        lineRenderer.endColor = isValidPhoto ? validRayColor : invalidRayColor;
    }

    List<string> GetCameraTriggerSequence()
    {
        Vector3 rayStart = transform.position;
        Vector3 rayDirection = -transform.right;

        RaycastHit[] hits = Physics.RaycastAll(rayStart, rayDirection, rayDistance, triggerLayerMask);

        // Сортируем попадания по расстоянию от начала луча
        System.Array.Sort(hits, (x, y) => x.distance.CompareTo(y.distance));

        List<string> cameraTriggerNames = new List<string>();
        foreach (RaycastHit hit in hits)
        {
            CameraTrigger cameraTrigger = hit.collider.GetComponent<CameraTrigger>();
            if (cameraTrigger != null)
            {
                cameraTriggerNames.Add(cameraTrigger.gameObject.name);
                //Debug.Log($"Луч попал в CameraTrigger: {cameraTrigger.gameObject.name} на расстоянии {hit.distance:F2}");
            }
        }

        return cameraTriggerNames;
    }

    bool CheckPhotoValidity(List<string> triggerSequence)
    {
        if (triggerSequence.Count < 2)
        {
            if (triggerSequence.Count == 1)
            {
                //Debug.Log($"Недостаточно триггеров: только {triggerSequence[0]}");
            }
            else {
                //Debug.Log($"Недостаточно триггеров: {triggerSequence.Count}");
            }
            return false;
        }

        // Берем первые два триггера в порядке попадания луча
        string first = triggerSequence[0];
        string second = triggerSequence[1];

        Debug.Log($"Проверка последовательности CameraTrigger: {first} -> {second}");

        bool isValid = (first == "0" && second == "180") ||
                      (first == "180" && second == "0") ||
                      (first == "90" && second == "270") ||
                      (first == "270" && second == "90");

        Debug.Log($"Последовательность {(isValid ? "ВАЛИДНА" : "НЕВАЛИДНА")}");
        return isValid;
    }

    void TryTakePhoto()
    {
        if (lineRenderer.startColor == validRayColor)
        {
            List<string> triggerSequence = GetCameraTriggerSequence();

            if (CheckPhotoValidity(triggerSequence))
            {
                int photoIndex = GetPhotoIndex(triggerSequence);

                if (photoIndex != -1 && !correctPhotos[photoIndex])
                {
                    correctPhotos[photoIndex] = true;

                    // Визуальная обратная связь об успешном фото
                    StartCoroutine(ShowPhotoSuccess());

                    Debug.Log($"<color=green><b>✅ ФОТО УСПЕШНО!</b></color>");
                    Debug.Log($"<color=green>Фото #{photoIndex + 1} сделано! (Триггеры: {string.Join(" -> ", triggerSequence)})</color>");

                    // Проверяем, все ли фото сделаны
                    CheckAllPhotosCompleted();
                }
                else if (photoIndex != -1)
                {
                    Base.instance.PlayPoliceAudio("Camera");
                    Debug.Log($"<color=yellow>⚠️ Фото #{photoIndex + 1} уже было сделано ранее!</color>");
                }
                else
                {
                    Base.instance.PlayPoliceAudio("Camera");
                }
            }
            else
            {
                //
                //
                Base.instance.PlayPoliceAudio("Camera");
                //
                //
                Debug.Log($"<color=red>❌ Не удалось сделать фото: неправильная последовательность триггеров</color>");
                Debug.Log($"<color=red>Текущая последовательность: {string.Join(" -> ", triggerSequence)}</color>");
            }
        }
        else
        {
            Base.instance.PlayPoliceAudio("Camera");
        }
    }

    IEnumerator ShowPhotoSuccess()
    {
        // Визуальный эффект успешного фото - мигание луча
        if (lineRenderer)
        {
            Color originalColor = lineRenderer.startColor;
            //lineRenderer.startColor = Color.yellow;
            //lineRenderer.endColor = Color.yellow;
            audioSource.PlayOneShot(photoSound);
            yield return new WaitForSeconds(0.3f);
            /*if (lineRenderer)
            {
                lineRenderer.startColor = validRayColor;
                lineRenderer.endColor = validRayColor;
            }*/
        }
    }

    int GetPhotoIndex(List<string> triggerSequence)
    {
        if (triggerSequence.Count >= 2)
        {
            string first = triggerSequence[0];
            string second = triggerSequence[1];

            if (first == "0" && second == "180") return 0;
            if (first == "180" && second == "0") return 1;
            if (first == "90" && second == "270") return 2;
            if (first == "270" && second == "90") return 3;
        }

        return -1;
    }

    void CheckAllPhotosCompleted()
    {
        /*bool allCompleted = true;
        for (int i = 0; i < correctPhotos.Length; i++)
        {
            if (!correctPhotos[i])
            {
                allCompleted = false;
                break;
            }
        }*/
        int bad = 0;
        for (int i = 0; i < correctPhotos.Length; i++)
        {
            if (!correctPhotos[i])
            {
                bad++;
            }
        }
        
        if (bad <= 0)
        {
            Debug.Log("<color=green><b>🎉 ВСЕ 4 ФОТОГРАФИИ УСПЕШНО СДЕЛАНЫ!</b></color>");
            if (Base.instance != null)
            {
                Base.instance.MakePhoto();
            }
            else
            {
                Debug.LogWarning("Base.instance не найден!");
            }
        }
        else
        {
            int completedCount = GetCompletedPhotosCount();
            Debug.Log($"<color=blue>📊 Прогресс: {completedCount}/4 фото</color>");

            // Показываем какие именно фото сделаны
            PrintCurrentProgress();
        }
    }

    void PrintCurrentProgress()
    {
        string[] photoNames = { "0→180", "180→0", "90→270", "270→90" };
        string progress = "Текущий прогресс: ";

        for (int i = 0; i < correctPhotos.Length; i++)
        {
            progress += $"{photoNames[i]} {(correctPhotos[i] ? "✅" : "❌")}";
            if (i < correctPhotos.Length - 1) progress += " | ";
        }

        Debug.Log($"<color=blue>{progress}</color>");
    }

    int GetCompletedPhotosCount()
    {
        int count = 0;
        foreach (bool completed in correctPhotos)
        {
            if (completed) count++;
        }
        return count;
    }

    void OnTriggerEnter(Collider other)
    {
        StayTrigger stayTrigger = other.GetComponent<StayTrigger>();
        if (stayTrigger != null)
        {
            isInStayTrigger = true;
            currentStayTrigger = stayTrigger;
            Debug.Log($"<color=cyan>🎯 Вошел в зону StayTrigger: {other.gameObject.name}</color>");

            // Запускаем корутину для проверки входа/выхода
            StartCoroutine(StayTriggerRoutine());
        }
    }

    void OnTriggerExit(Collider other)
    {
        StayTrigger stayTrigger = other.GetComponent<StayTrigger>();
        if (stayTrigger != null && stayTrigger == currentStayTrigger)
        {
            isInStayTrigger = false;
            currentStayTrigger = null;
            Debug.Log($"<color=cyan>🚪 Вышел из зоны StayTrigger: {other.gameObject.name}</color>");
        }
    }

    IEnumerator StayTriggerRoutine()
    {
        while (isInStayTrigger && currentStayTrigger != null)
        {
            // В этом цикле можно добавить дополнительную логику для StayTrigger
            yield return null;
        }
    }

    // Методы для отладки
    public void ResetPhotos()
    {
        for (int i = 0; i < correctPhotos.Length; i++)
        {
            correctPhotos[i] = false;
        }
        Debug.Log("<color=orange>Все фотографии сброшены</color>");
    }

    public void PrintPhotoStatus()
    {
        string status = "<b>СТАТУС ФОТОГРАФИЙ:</b>\n";
        status += $"0→180: {(correctPhotos[0] ? "<color=green>✅ СДЕЛАНО</color>" : "<color=red>❌ НЕ СДЕЛАНО</color>")}\n";
        status += $"180→0: {(correctPhotos[1] ? "<color=green>✅ СДЕЛАНО</color>" : "<color=red>❌ НЕ СДЕЛАНО</color>")}\n";
        status += $"90→270: {(correctPhotos[2] ? "<color=green>✅ СДЕЛАНО</color>" : "<color=red>❌ НЕ СДЕЛАНО</color>")}\n";
        status += $"270→90: {(correctPhotos[3] ? "<color=green>✅ СДЕЛАНО</color>" : "<color=red>❌ НЕ СДЕЛАНО</color>")}";
        Debug.Log(status);
    }
}

// Класс для StayTrigger (активация проверки)
public class StayTrigger : MonoBehaviour
{
    // Можно добавить дополнительную логику для StayTrigger
}