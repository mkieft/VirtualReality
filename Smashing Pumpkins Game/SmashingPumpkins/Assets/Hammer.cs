using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hammer : MonoBehaviour
{
    public static int score = 1;
    public GameObject textPrefab;
    
    private GameObject pumpkinToAccess;
   

    private Vector3 initialPos;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        //scoreText.text = "Score: " + score;
       // Debug.Log("printing score" + scoreText.text);
    }


    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "pumpkin" || hit.gameObject.tag == "pumpkinX")
        {
            Vector3 posGO = hit.gameObject.transform.position;
            Debug.Log("hit game object pos: " + posGO);
            //+1 at runtime
            GameObject newText = textPrefab;
            var newOne = Instantiate(newText, posGO, Quaternion.identity);
            Destroy(newOne.gameObject, 0.5f);

            score++;

            Debug.Log("score: " + score);
            FindObjectOfType<AudioManager>().Play("PumpkinHit");
            //Debug.Log("Play hit pumpkin audio");

            if (hit.gameObject.tag == "pumpkin")
            {
                pumpkinToAccess = hit.gameObject;
                Pumpkin scriptToAccess = pumpkinToAccess.GetComponent<Pumpkin>();
                scriptToAccess.HidePumpkin();
                //scoreText.text = "Score: " + score;
            }
            else if(hit.gameObject.tag == "pumpkinX")
            {
                pumpkinToAccess = hit.gameObject;
                PumpkinX scriptToAccessX = pumpkinToAccess.GetComponent<PumpkinX>();
                scriptToAccessX.HidePumpkinX();
                //scoreText.text = "Score: " + score;
            }
            
            //instantiate +1 point above object

            
            
        }
    }
}
