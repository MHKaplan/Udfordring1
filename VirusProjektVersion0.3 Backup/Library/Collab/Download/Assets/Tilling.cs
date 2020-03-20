using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]   // add spriterenderer 

public class Tilling : MonoBehaviour
{

    public int offsetX = 2; //offset b value because if not the error will come.

    // These are used for checking if we need to instantiate(represent) stuff.
    public bool hasARightBuddy = false; 
    public bool hasALeftBuddy = false;

    public bool reverseScale = false;  // used for the objects that are not tilable

    private float spriteWidth = 0f;  //store the width of our sprite, means how long the element is.
    private Camera cam;
    private Transform myTransform;

    void Awake()
    {
        cam = Camera.main;
        myTransform = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>(); // This is going to grab the first component of the type you insert
        spriteWidth = sRenderer.sprite.bounds.size.x; // This i going to give of the Width/length of our element.
    }

    // Update is called once per frame
    void Update()
    {
        // checking if it still need buddies? if not do nothing.
        if (hasALeftBuddy == false || hasARightBuddy == false)
        {
            //calcutlate the cameras extend (half the width) of what the camera can see in world cordinates. 
            float camHorizontalExtend = cam.orthographicSize * Screen.width / Screen.height;

            // calculate the x postion where the camera can see the edge of the sprite (element)   
            float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtend;
            float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth / 2) + camHorizontalExtend;

            // checking if we can see the edge of the element and then calling MakeNewbuddy if we can.
            if (cam.transform.position.x >= edgeVisiblePositionRight - offsetX && hasARightBuddy == false)
            {
                MakeNewBuddy (1);
                hasARightBuddy = true;
            }
            else if (cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && hasALeftBuddy == false)
            {
                MakeNewBuddy(-1);
                hasALeftBuddy = true;
            }
        }
    }

    // a function that creates a buddy on the side that requiers a buddy. 
    void MakeNewBuddy(int RightorLeft)
    {
        // caluating the new position for our new buddy
        Vector3 newPostion = new Vector3 (myTransform.position.x + spriteWidth * RightorLeft, myTransform.position.y, myTransform.position.z);
        //instantating our new body and storing him in a variable
        Transform newBuddy = Instantiate(myTransform, newPostion, myTransform.rotation) as Transform;

        //if not tilable let's reverse the x size of our object to get rid of ugly seams.
        if (reverseScale == true)
        {
            newBuddy.localScale = new Vector3(newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);
        }
        newBuddy.parent = myTransform.parent;
        if (RightorLeft > 0)
        {
            newBuddy.GetComponent<Tilling>().hasALeftBuddy = true;
        } else
        {
            newBuddy.GetComponent<Tilling>().hasARightBuddy = true;
        }
    }   
}
