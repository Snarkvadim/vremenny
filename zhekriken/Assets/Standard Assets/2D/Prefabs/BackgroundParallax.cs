using UnityEngine;
using System.Collections;

public class BackgroundParallax : MonoBehaviour
{
	public Transform[] backgrounds;				// Array of all the backgrounds to be parallaxed.

    public float[] reduceFactorsX;
    public float[] reduceFactorsY;
	public float smoothing;						// How smooth the parallax effect should be.

    private const int parallaxScaleY = 6; // The proportion of the camera's movement to move the backgrounds by.


	private Transform cam;						// Shorter reference to the main camera's transform.
	private Vector3 previousCamPos;				// The postion of the camera in the previous frame.

    private int count = 0;


	void Awake ()
	{
		// Setting up the reference shortcut.
        
		cam = GameObject.Find("Main Camera").transform;
	}


	void Start ()
	{
        Debug.Log(cam);
		// The 'previous frame' had the current frame's camera position.
		previousCamPos = cam.position;
	}


	void Update (){
//        stayBackGround.position = new Vector3(cam.position.x, stayBackGround.position.y, stayBackGround.position.z);
        // The parallax is the opposite of the camera movement since the previous frame multiplied by the scale.
	    float reduceFactor = previousCamPos.x - cam.position.x;

	    float parallax = (previousCamPos.x - cam.position.x); //* parallaxScaleX;
		float parallaxY = (previousCamPos.y - cam.position.y) * parallaxScaleY;
		// For each successive background...
		for(int i = 0; i < backgrounds.Length; i++){
		    float rf;
		    float rfY;
			// ... set a target x position which is their current position plus the parallax multiplied by the reduction.
		    if (reduceFactorsX.Length > i)
		        rf = reduceFactorsX[i];
		    else{
		        rf = 1;
		    }
            if (reduceFactorsY.Length > i)
                rfY = reduceFactorsY[i];
            else {
                rfY = 1;
            }
            float backgroundTargetPosX = backgrounds[i].position.x - reduceFactor + parallax * rf;
            float backgroundTargetPosY = backgrounds[i].position.y + parallaxY - parallaxY * rfY;// + parallaxY*0.1f * (i * parallaxReductionFactorY + 1);


			// Lerp the background's position between itself and it's target position.
            backgrounds[i].position = new Vector3(backgroundTargetPosX, Mathf.Lerp(backgrounds[i].position.y, backgroundTargetPosY, smoothing * Time.deltaTime));
//            backgrounds[i].position = new Vector3(backgroundTargetPosX, backgroundTargetPosY);
		}

		// Set the previousCamPos to the camera's position at the end of this frame.
		previousCamPos = cam.position;
	}
}
