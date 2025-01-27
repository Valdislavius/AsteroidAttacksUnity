using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    private AudioSource audioSource;

    public static SoundManager instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayShot(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
