using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioClip audioToPlay;
    public bool playOnDestroy = false;

    // Start is called before the first frame update
    void Start()
    {
        if (audioToPlay == null)
        {
            Debug.LogWarning("Audio clip not found in SoundPlayer");
        }
    }


    public void Play()
    {
        Debug.Log("Playing...");
        AudioSource.PlayClipAtPoint(audioToPlay, transform.position);
    }

    private void OnDestroy()
    {
        if (playOnDestroy) Play();
    }
}
