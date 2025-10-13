using UnityEngine;

public class MainPoliceman : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;

    public void PlaySoundWithAnimation(AudioClip clip, float volume)
    {
        animator.SetTrigger("Tell");
        audioSource.PlayOneShot(clip, volume);
    }
}
