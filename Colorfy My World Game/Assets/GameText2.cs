using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameText2 : MonoBehaviour
{
    public GameController GC;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMesh>().text = "Percent Colored: 0% ";

    }

    // Update is called once per frame
    void Update()
    {
        var colored = GC.worldColored;
      

        if(GC.wonGame == true)
        {
            GetComponent<TextMesh>().color = Color.red;
          
            GetComponent<TextMesh>().text = "Play Again? Press Y.";
        }
        else
        {
         
            GetComponent<TextMesh>().text = "Percent Colored: " + colored + "%";
        }
    }
}
