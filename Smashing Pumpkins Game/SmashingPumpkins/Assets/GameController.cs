using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //timer
    public TextMesh timerText;
    public TextMesh surroundedText;
    public TextMesh startGame;
    public TextMesh updateScore;

    private static int score1;

    private float gameTimer = -1;

    //pumpkins
    public GameObject pumpkinContainer;
    public GameObject pumpkinChangeXContainer;
    private Pumpkin[] pumpkins;
    private PumpkinX[] pumpkinsX; 

    public float showPumpkinTimer = 1.5f;
    public float showPumpkinTimerx = 1.5f;

    public bool playedGameover;

   

    // Start is called before the first frame update
    void Start()
    {

        updateScore.text = "Score:--";
        //put pumpkins into list
        pumpkins = pumpkinContainer.GetComponentsInChildren<Pumpkin>();
        pumpkinsX = pumpkinChangeXContainer.GetComponentsInChildren<PumpkinX>();


        Debug.Log("# of pumpkins change on x: " + pumpkinsX.Length);
        //Debug.Log("# pumpkins: " + pumpkins.Length); //print pmpkins
    }

    // Update is called once per frame
    void Update()
    {       
        //update game timer
        startGame.text = "Press trigger (space) to start game";

        //update score
        updateScore.text = "Score: " + (Hammer.score)/2;
       
        if(Input.GetKeyDown("space"))
        //if (Input.GetButtonDown("Fire1")) //change to input.getbuttondown("fire1")
        {           
            gameTimer = 30f;                              
            FindObjectOfType<AudioManager>().Play("StartAudio");
           
        }
        
       


        if (gameTimer > 0f)
            {

            playedGameover = false;

            startGame.text = "";
            //Debug.Log("gameTimer > 0 started game");
            gameTimer -= Time.deltaTime;

            //update text
            timerText.text = "TIMER: " + Mathf.Floor(gameTimer);

                //show
                if (gameTimer < 20f)
                {
                    surroundedText.text = "You're surrounded!!!";
                    showPumpkinTimerx -= Time.deltaTime;
                    if (showPumpkinTimerx < 0f)
                    {
                        pumpkinsX[Random.Range(0, pumpkinsX.Length)].ShowPumpkinX();
                        showPumpkinTimerx = 0.5f;
                    }
                }

                showPumpkinTimer -= Time.deltaTime;
                if (gameTimer > 20f && showPumpkinTimer < 0f)
                {
                    pumpkins[Random.Range(0, pumpkins.Length)].ShowPumpkin();

                    showPumpkinTimer = 1.0f;
                }

            }
            else
            {
                if (playedGameover == false)
                {
                    playedGameover = true;

                    FindObjectOfType<AudioManager>().Play("GameOver");
                    //Debug.Log("Play game over audio");
                
                }
            


            surroundedText.text = "";
                if(gameTimer != -1)
                {
               
              
                timerText.text = "GAME OVER";
                }
                
               
            }


        }
      

   
    }

