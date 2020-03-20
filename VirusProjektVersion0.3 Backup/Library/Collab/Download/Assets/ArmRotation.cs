using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour
{
    public int rotationOffset = 90; 

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; //position of our mouse in the 3D space minus the space between our character.
        difference.Normalize(); //Make all the values of x,y and z of the vector equal to 1. Normalize the vector means that the sum of the vector wil be equal to 1.

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; //go from Radians to degrees. 
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
    }
}
