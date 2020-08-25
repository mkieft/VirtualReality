using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingCollision : MonoBehaviour
{
    public float distance;
    public float speed;
    public Vector3 offset; 

    void OnMouseDrag()
    {
        transform.position = MouseWorldPoint() + offset;
    }
    private Vector3 MouseWorldPoint()
    {
        Vector3 mPoint = Input.mousePosition;
        mPoint.z = distance;
        return Camera.main.ScreenToWorldPoint(mPoint) * speed; 
    }

    public void OnMouseDown()
    {
        distance = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        offset = gameObject.transform.position - MouseWorldPoint();
    }

}
