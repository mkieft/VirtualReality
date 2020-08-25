using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinX : MonoBehaviour
{
    //for pumpkins coming through walls
    public float visibleX = .31f;
    public float hiddenX = -2.08f;
    public float speed = 6.0f;
    public float hideTimeX = 2.0f;


    private Vector3 newPosX;

    void Awake()
    {
        HidePumpkinX();

        //set position
        transform.localPosition = newPosX;
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
            newPosX,
            Time.deltaTime * speed);

        hideTimeX -= Time.deltaTime;

        if (hideTimeX < 0)
        {
            HidePumpkinX();
        }
    }

    public void HidePumpkinX()
    {
        //current position is hidden height
        newPosX = new Vector3(
            transform.localPosition.x,
            hiddenX,
            transform.localPosition.z);
    }

    public void ShowPumpkinX()
    {
        newPosX = new Vector3(
           transform.localPosition.x,
           visibleX,
           transform.localPosition.z
           );

        //reset timer
        hideTimeX = 2.0f;
    }
}