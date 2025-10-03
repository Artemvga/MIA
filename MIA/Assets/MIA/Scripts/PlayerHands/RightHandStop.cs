using UnityEngine;

public class RightHandStop : MonoBehaviour
{
    public Transform rightHandTransform;
    public Transform playerTransform;
    [Space]
    public float upOffset;
    public float forwardOffset;


    private void Update()
    {
        if (rightHandTransform.position.y - playerTransform.position.y >= upOffset &&
            Mathf.Abs(rightHandTransform.position.x - playerTransform.position.x) < forwardOffset &&
            Mathf.Abs(rightHandTransform.position.z - playerTransform.position.z) < forwardOffset)
        {
            Debug.Log("BLYHA MUHA NAM PIZDA");
        }
    }
}
