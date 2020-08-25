using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameText : MonoBehaviour
{
    public GameController GC;

    public GameObject winText;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMesh>().text = "Press Y to Start Game "; 

    }

    // Update is called once per frame
    void Update()
    {
        var time = GC.gameTimer;

        if (GC.gameStarted == false)
        {
            GetComponent<TextMesh>().text = "Press Y to Start Game ";
        }
        else if (GC.gameOver == true)
        {
            GetComponent<TextMesh>().text = "GAME OVER. Play Again? (Y) ";
        }
        else if (GC.wonGame == true)
             
        {
            GetComponent<TextMesh>().color = Color.red; 
            GetComponent<TextMesh>().text = "You colored 100%!!";
        }
        else{
            GetComponent<TextMesh>().text = "Time Remaining: " + Mathf.Floor(time);

            if (time < 0)
            {
                GetComponent<TextMesh>().text = "Press Y to Start Game ";
            }
        }
        
    }
}
