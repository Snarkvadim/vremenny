using UnityEngine;
using System.Collections;

public class BackgroundParallax : MonoBehaviour
{
	public Transform[] backgrounds;				// Array of all the backgrounds to be parallaxed.
	public float parallaxScaleX;					// The proportion of the camera's movement to move the backgrounds by.
	public float parallaxReductionFactorX;		// How much less each successive layer should parallax.
	public float smoothing;						// How smooth the parallax effect should be.

    public float parallaxScaleY;					// The proportion of the camera's movement to move the backgrounds by.
    public float parallaxReductionFactorY;		// How much less each successive layer should parallax.


	private Transform cam;						// Shorter reference to the main camera's transform.
	private Vector3 previousCamPos;				// The postion of the camera in the previous frame.

//    public Transform stayBackGround;


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
		float parallax = (previousCamPos.x - cam.position.x) * parallaxScaleX;
		float parallaxY = (previousCamPos.y - cam.position.y) * parallaxScaleY;
		// For each successive background...
		for(int i = 0; i < backgrounds.Length; i++)
		{
			// ... set a target x position which is their current position plus the parallax multiplied by the reduction.
			float backgroundTargetPosX = backgrounds[i].position.x + parallax * (i * parallaxReductionFactorX + 1);
			float backgroundTargetPosY = backgrounds[i].position.y + parallaxY * (i * parallaxReductionFactorY + 1);

			// Create a target position which is the background's current position but with it's target x position.
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgroundTargetPosY, backgrounds[i].position.z);

			// Lerp the background's position between itself and it's target position.
			backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
		}

		// Set the previousCamPos to the camera's position at the end of this frame.
		previousCamPos = cam.position;
	}
}
