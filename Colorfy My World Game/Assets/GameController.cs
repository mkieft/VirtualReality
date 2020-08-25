using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //environment -> 22%
    //house -> 16 %
    //trees -> 18%
    //small trees -> 14%
    //flowers 10% each 
    //y--> Fire 1
    //trigger --> Jump
  

    public float worldColored = 0;
    public bool targetHit = false;
    public bool gameStarted = false;
    public bool gameOver;
    public bool wonGame; 


    public GameObject redFlowersContainer;
    public GameObject yellowFlowersContainer;
    public GameObject blueFlowersContainer;
    public GameObject environmentContainer;
    public GameObject smallTreesContainer;
    public GameObject treesContainer;
    public GameObject houseContainer; 

    public GameObject targetContainer;
    private Target[] targets;
    private float showTargetTimer = 0f; 

    public Material original;
    public Material grayMatTrees;
    public Material grayMatFlowers;
    public Material grayMatEnv;
    public Material grayMatHouse;

    public float gameTimer = 0;
    private int numToColor = -1; 


    private Renderer[] redFlowers;
    private Renderer[] yellowFlowers;
    private Renderer[] blueFlowers;
    private Renderer[] environment;
    private Renderer[] smallTrees;
    private Renderer[] trees;
    private Renderer[] house; 

    // Start is called before the first frame update
    void Start()
    {
        StartGame();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonUp("Jump") && gameStarted == false) //press S to start --> change for live
        {

            Debug.Log("Fire1 pressed..starting game");
            gameTimer = 30f;
            gameStarted = true;
            wonGame = false; 
            worldColored = 0;
            numToColor = -1;
            StartGame(); 

        }

        if (gameTimer > 0f && worldColored < 100)
        {
            gameOver = false; 
            gameTimer -= Time.deltaTime;
            showTargetTimer -= Time.deltaTime; 

            if(showTargetTimer <= 0f)
            {
                targets[Random.Range(0, targets.Length)].ShowTarget();

                showTargetTimer = 1.0f; 
            }

            if (targetHit == true)
            {
                FindObjectOfType<AudioManager>().Play("Sparkle");

                numToColor += 1;
                Debug.Log("Coloring num: " + numToColor);

                if (numToColor == 0) //red flowers
                {
                    GetColor(redFlowers, original);
                    worldColored += 10;
                    targetHit = false;
                }
                else if (numToColor == 1) //yellow flowers
                {
                    GetColor(yellowFlowers, original);
                    worldColored += 10;
                    targetHit = false;
                }
                else if (numToColor == 2) //blue flowers
                {
                    GetColor(blueFlowers, original);
                    worldColored += 10;
                    targetHit = false;
                }
                else if (numToColor == 3) //trees 
                {
                    GetColor(trees, original);
                    worldColored += 18;
                    targetHit = false;
                }
                else if (numToColor == 4) // small trees
                {
                    GetColor(smallTrees, original);
                    worldColored += 14;
                    targetHit = false;
                }
                else if (numToColor == 5) //house
                {
                    GetColor(house, original);
                    worldColored += 16;
                    targetHit = false;
                }
                else if (numToColor == 6) //environment
                {
                    GetColor(environment, original);
                    worldColored += 22;
                    targetHit = false;
                }


            }

        }
        else if(worldColored == 100)
        {
            //add winning audio 
            FindObjectOfType<AudioManager>().Play("GameOver");
            wonGame = true;
            gameStarted = false;
            gameOver = true;
            gameTimer = 0;
            numToColor = -1;

        }
        else {
            gameStarted = false;
            gameOver = true; 
            gameTimer = 0;
            numToColor = -1;

            
            
        }
    }
    


   

    void StartGray(Renderer[] gameobjectRenderer, Material gray)
    {
        for(int i =0; i < gameobjectRenderer.Length; i++)
        {
            gameobjectRenderer[i].GetComponent<Renderer>().material = gray;  
        }
    }

    void GetColor(Renderer[] gameobjectRenderer, Material originalMat)
    {
        for (int i = 0; i < gameobjectRenderer.Length; i++)
        {
            gameobjectRenderer[i].GetComponent<Renderer>().material = originalMat;
            Debug.Log("Coloring: " + gameobjectRenderer);
        }
    }

    void StartGame()
    {
        targets = targetContainer.GetComponentsInChildren<Target>();

        redFlowers = redFlowersContainer.GetComponentsInChildren<Renderer>();
        yellowFlowers = yellowFlowersContainer.GetComponentsInChildren<Renderer>();
        blueFlowers = blueFlowersContainer.GetComponentsInChildren<Renderer>();
        environment = environmentContainer.GetComponentsInChildren<Renderer>();
        smallTrees = smallTreesContainer.GetComponentsInChildren<Renderer>();
        trees = treesContainer.GetComponentsInChildren<Renderer>();
        house = houseContainer.GetComponentsInChildren<Renderer>();

        StartGray(redFlowers, grayMatFlowers);
        StartGray(yellowFlowers, grayMatFlowers);
        StartGray(blueFlowers, grayMatFlowers);
        StartGray(environment, grayMatEnv);
        StartGray(smallTrees, grayMatTrees);
        StartGray(trees, grayMatTrees);
        StartGray(house, grayMatHouse);

        
    }
 
}
