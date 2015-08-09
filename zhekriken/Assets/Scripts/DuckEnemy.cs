using UnityEngine;
using System.Collections;

public class DuckEnemy : MonoBehaviour{
    private GameObject player;
    private Quaternion rotation;

    public GameObject bubblePosition;
    public GameObject bubblePrefab;


	// Use this for initialization
	void Awake () {
        rotation = Quaternion.identity;
	}

    void Start(){
//        InvokeRepeating("BubbleShot", 2f, 3f);
    }
	// Update is called once per frame
	void Update () {
	    if (player != null){
            Vector3 tempVector = player.transform.position - transform.position;
            float angle = Vector3.Angle(new Vector3(1f, 0f, 0f), tempVector);
            if (tempVector.y < 0f)
                angle = 360 - angle + 160;
            else
                angle = angle + 160;
//            Debug.Log(angle);
            rotation.eulerAngles = new Vector3(0f, 0f, angle);
	        transform.rotation = rotation;


//            transform.LookAt(player.transform);
//            Quaternion q = Quaternion.FromToRotation(Vector3.forward, player.transform.position - transform.position);
//	        transform.rotation = q;
	    }
	}

    void BubbleShot(){
        GameObject bubble = Instantiate(bubblePrefab);
        bubble.transform.SetParent(gameObject.transform);
        bubble.transform.position = bubblePosition.transform.position;
        
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player"){
            player = col.gameObject;
        }
    }
}
