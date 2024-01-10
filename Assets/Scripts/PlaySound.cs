using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    AudioSource audio;
    void Start()
    {
        audio = GetComponent<AudioSource>();

        audio.loop = true;

        audio.Play();
    }

    void Update()
    {
        
    }
}
