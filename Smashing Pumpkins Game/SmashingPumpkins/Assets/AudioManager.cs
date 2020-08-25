using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    // Start is called before the first frame update
    void Awake()
    {
        foreach(Sound s in sounds)
        {
           s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

        }
    }

    // Update is called once per frame
    public void Play(string name)
    {
       Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s== null)
        {
            Debug.Log("s == null sound not found");
            return;
        }
       s.source.Play();
    }
}
