using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public Transform[] backgrounds;     //Array of list for back and foregrounds to be parallaxed
    private float[] parallaxScales;     //The proportion of the camera's movement to move the backgrounds by.
    public float smoothing = 1f;        //how smooth the parallax is going to be. Needs to be above 0. 

    private Transform cam;              // reference to the main cameraas transform   
    private Vector3 previousCamPos;     // the position of the camera in the previous frame


    // void Awake is used before the start funktion. Very good for references. 
    void Awake()
    {
        // set up camera reference
        cam = Camera.main.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        // The previousframe had the current frame's camera position.
        previousCamPos = cam.position;

        //asigning corespondig  parallaxScales
        parallaxScales = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z*-1;
        }
    }   

    // Update is called once per frame
    void Update()
    {
        // for each background
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // The parallax is the opposite of the camera movement because the previous frame multiplied by the scale.
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            // set target x position which is the current position plus the parallax.
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            // create a target position which is the background's current position with it's target x position
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            // fade between current position and the target position using lerp
            // Time.deltaTime means that it converts frames to seconds
            backgrounds[i].position = Vector3.Lerp (backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);

            // set the previousCamPos to the camera's position at the end of the frame
            previousCamPos = cam.position;

        }


    }
}
