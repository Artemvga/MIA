using UnityEngine;

public class EyeLook : MonoBehaviour
{
    public Transform eyeDestination;
    
    void Update()
    {
        transform.LookAt(eyeDestination);
    }
}
