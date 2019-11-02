using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform target;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            MoveToCursor();  

        }

        // test

        //Debug.DrawRay(lastRay.origin, lastRay.direction * 100);
        //lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        
    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);

        if (hasHit)
        {
            Debug.Log("we have hit");
            GetComponent<NavMeshAgent>().destination = hit.point;
        }
    }
}
