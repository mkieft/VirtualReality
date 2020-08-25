using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioClip musicClip;
    public AudioSource musicSource;




    // Start is called before the first frame update
    void Start()
    {
     

        //gameoverSource.clip = gameoverClip;
        musicSource.clip = musicClip;
        //gameoverSource.clip = gameoverClip;
        //AudioSource.OnAwake() = false;
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            musicSource.Play();
        }

  


    } 
    /*
    public void OnGameOver()
    {
        gameoverSource.Play(); 
    }
    */
}
