using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        source.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
    }

}
