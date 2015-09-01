using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FpsCounter : MonoBehaviour {

	private const float updateInterval = 0.5f;
	
	private float accum = 0.0f; // FPS accumulated over the interval
	private int frames = 0; // Frames drawn over the interval
	private float timeleft; // Left time for current interval
	private Text text;

	void Start () {
		timeleft = updateInterval;
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		timeleft -= Time.deltaTime;
		accum += Time.timeScale/Time.deltaTime;
		++frames;
		
		// Interval ended - update GUI text and start new interval
		if( timeleft <= 0.0f ) {
			// display two fractional digits (f2 format)
			text.text = "FPS: " + (accum/frames);
			timeleft = updateInterval;
			accum = 0.0f;
			frames = 0;
		}	
	}
}
