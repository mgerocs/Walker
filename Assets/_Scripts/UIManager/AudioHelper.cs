using System.Collections;
using UnityEngine;

public class AudioHelper
{
    public static IEnumerator PlayClip(AudioSource audioSource, AudioClip clip)
    {
        audioSource.enabled = true;

        WaitForSeconds Wait = new(clip.length);

        audioSource.PlayOneShot(clip);

        yield return Wait;

        audioSource.enabled = false;
    }
}
