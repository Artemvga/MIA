using UnityEngine;

public class RightHandStop : MonoBehaviour
{
    public Transform rightHandTransform;
    public Transform playerTransform;
    [Space]
    public float upOffset;
    public float forwardOffset;
    public float rotationOffset;


    private void Update()
    {
        if (rightHandTransform.position.y - playerTransform.position.y >= upOffset &&
            Mathf.Abs(rightHandTransform.position.x - playerTransform.position.x) < forwardOffset &&
            Mathf.Abs(rightHandTransform.position.z - playerTransform.position.z) < forwardOffset &&
            Mathf.Abs(rightHandTransform.rotation.eulerAngles.y) > 50 && 
            Mathf.Abs(rightHandTransform.rotation.eulerAngles.y) < 90 &&
            Mathf.Abs(rightHandTransform.rotation.eulerAngles.x) > 110 &&
            Mathf.Abs(rightHandTransform.rotation.eulerAngles.x) < 150)
        {
            Debug.Log("BLYHA MUHA NAM PIZDA");
        }
    }
}
