using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameController GC; 

    public float visibleY = 5.14f;
    public float hiddenY = 2.14f;
    public float hideTime = 2.0f;
    private Vector3 newPos;

    //Animator anim;

    private void Awake()
    {
        HideTarget();
        transform.localPosition = newPos; 
    }
    void Start()
    {
        //anim = GetComponent<Animator>(); 
    }

    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        //ShowTarget(); 
       transform.localPosition = new Vector3(newPos.x, newPos.y, newPos.z); 
        hideTime -= Time.deltaTime;
        if (hideTime < 0)
        {
            HideTarget();
            //anim.SetBool("isMoving", false); 
        }
        
    }
    public void HideTarget()
    {
         newPos = new Vector3(
             transform.localPosition.x,
             hiddenY,
             transform.localPosition.z);
             
       // anim.SetBool("isMoving", false);


    }
    public void ShowTarget()
    {
         newPos = new Vector3(
             transform.localPosition.x,
             visibleY,
             transform.localPosition.z
             );
             
        //anim.SetBool("isMoving", true); 

        hideTime = 2.0f;
    }

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("Target hit"); 
        GC.targetHit = true;
        this.HideTarget();
        
    }
}
