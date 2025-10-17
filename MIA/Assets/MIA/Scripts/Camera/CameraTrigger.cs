using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [Header("Trigger Settings")]
    public string triggerName = "0"; // 0, 90, 180, 270

    void OnDrawGizmos()
    {
        // Визуализация триггера в редакторе
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, GetComponent<Collider>().bounds.size);

        // Подпись триггера
#if UNITY_EDITOR
        UnityEditor.Handles.Label(transform.position, $"Trigger: {triggerName}");
#endif
    }
}