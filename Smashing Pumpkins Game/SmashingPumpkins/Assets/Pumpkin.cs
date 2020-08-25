using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : MonoBehaviour
{
    //for traditional wack a mole pumpkins 
    public float visibleYHeight = 1.0f;
    public float hiddenYHeight = -3.6f;
    public float speed = 3f;
    public float hideTime = 1.0f;

    private Vector3 newPos;

    void Awake()
    {
        HidePumpkin();

        //set position
        transform.localPosition = newPos;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            newPos,
            Time.deltaTime * speed);

        hideTime -= Time.deltaTime;

        if(hideTime < 0)
        {
            HidePumpkin();
        }
    }

    public void HidePumpkin()
    {
        //current position is hidden height
        newPos = new Vector3(
            transform.localPosition.x,
            hiddenYHeight,
            transform.localPosition.z);
    }

    public void ShowPumpkin()
    {
        newPos = new Vector3(
           transform.localPosition.x,
           visibleYHeight,
           transform.localPosition.z
           );

        //reset timer
        hideTime = 1.0f;
    }
}
