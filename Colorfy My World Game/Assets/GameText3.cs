using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameText3 : MonoBehaviour
{
    public GameController GC;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMesh>().text = " ";
    }

    // Update is called once per frame
    void Update()
    {
        if(GC.wonGame == true)
        {
            GetComponent<TextMesh>().color = Color.red;
            GetComponent<TextMesh>().text = "YOU WON!!";
        }
    }
}
