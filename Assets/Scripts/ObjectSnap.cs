using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSnap : MonoBehaviour
{
    Vector3 mousePosition;
    public Vector3 snapPosition;
    public float snapDistance = 1;
   private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        mousePosition = Input.mousePosition - GetMousePos();
    }

    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
    }
    void Update()
    {
        transform.position = transform.position;

        if (Vector3.Distance(snapPosition, transform.position) < snapDistance) 
        { 
        transform.position = snapPosition; 
        }
    }
 
}
