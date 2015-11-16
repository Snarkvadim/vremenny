using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class snake : MonoBehaviour {
	public GameObject Canvas;
	public GameObject snake_body;
	public GameObject snake_head;
	public GameObject food;
	private GameObject currentSnake;
	private GameObject currentFood;
	public GameObject end_but;
	public GameObject end_text;
	public float moveSpeed= 1F;
	List<GameObject> snakes = new List<GameObject>();
	List<int> speed = new List<int>();
	public int kadr = 1;
	private int bufer;
	public float tx;
	public float ty;
	public int snakesNum = 0;
	public int randFoodX;
	public int randFoodY;
	public int maxScreenX;
	public int maxScreenY;

	void Awake(){
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 3;
	}

	void Start () {
		Vector3 resolution_max = Camera.main.ScreenToWorldPoint(new Vector3((Screen.width), (Screen.height), 0));
		Vector3 resolution_min = Camera.main.ScreenToWorldPoint(new Vector3(25, 25, 0));
		tx = 140;
		ty = (((Screen.height / 40) / 2)*40)+20;

		for (var i = 0; i < 4; i++){
			if (i < 1){
				currentSnake = (GameObject)Instantiate(snake_head, new Vector3(tx, ty , 0), Quaternion.identity);
			}
			else currentSnake = (GameObject)Instantiate(snake_body, new Vector3(tx, ty , 0), Quaternion.identity);
			currentSnake.transform.parent = Canvas.transform ;
			currentSnake.name = "snake" + snakesNum;
			snakes.Add(currentSnake);
			tx -= 40;
			snakesNum++;
		}


		speed.Add (0);
		speed.Add (-40);
		speed.Add (0);
		speed.Add (40);

		maxScreenX = (((Screen.width-40) / 40) * 40) + 20;
		maxScreenY = (((Screen.height-40) / 40) * 40) + 20;

		randFoodX = ((Random.Range (0, ((Screen.width - 40) / 40)))*40)+20;
		randFoodY = ((Random.Range (0, ((Screen.height - 40) / 40)))*40)+20;
		currentFood =  (GameObject)Instantiate(food, new Vector3(randFoodX,randFoodY,0), Quaternion.identity);
		currentFood.transform.parent = Canvas.transform;


	
	}
	

	void Update () {

		if (Input.GetMouseButtonDown (0)) {


			if ((Input.mousePosition.x>=(Screen.width/2)) && (kadr == 1)){
				bufer = speed[0];
				for (var i = 0 ; i < 3; i++){
					speed[i] = speed[i+1];
				}
				speed[3] = bufer;
				kadr = 0;
			}
			
			if ((Input.mousePosition.x < (Screen.width/2)) && (kadr == 1)){
				bufer = speed[3];
				for (var i = 3; i > 0; i--){
					speed[i] = speed[i-1];
				}
				speed[0] = bufer;
				kadr = 0;
			}
		}

		for (var i = (snakes.Count-1); i>0; i--) {
			snakes[i].transform.position = (new Vector3 (snakes[i-1].transform.position.x, snakes[i-1].transform.position.y, 0));
		}
		snakes [0].transform.position += new Vector3 (speed[3], speed[0] ,0);

		kadr = 1;

		if (snakes [0].transform.position.x > (Screen.width - 20)) {
			snakes[0].transform.position = new Vector3 (20, snakes[0].transform.position.y, 0);
		}

		if (snakes [0].transform.position.x < 20) {
			snakes[0].transform.position = new Vector3 (maxScreenX, snakes[0].transform.position.y, 0);
		}

		if (snakes [0].transform.position.y > (Screen.height - 20)) {
			snakes[0].transform.position = new Vector3 (snakes[0].transform.position.y, 20 , 0);
		}

		if (snakes [0].transform.position.y < 20) {
			snakes[0].transform.position = new Vector3 (snakes[0].transform.position.y, maxScreenY, 0);
		}

		if ((snakes [0].transform.position.x == currentFood.transform.position.x) && (snakes [0].transform.position.y == currentFood.transform.position.y)) {
			randFoodX = ((Random.Range (0, ((Screen.width - 40) / 40)))*40)+20;
			randFoodY = ((Random.Range (0, ((Screen.height - 40) / 40)))*40)+20;
			currentFood.transform.position =  new Vector3(randFoodX,randFoodY,0);

			currentSnake = (GameObject)Instantiate(snake_body, new Vector3(snakes[snakes.Count-1].transform.position.x, snakes[snakes.Count-1].transform.position.y , 0), Quaternion.identity);
			currentSnake.transform.parent = Canvas.transform ;
			currentSnake.name = "snake" + snakesNum;
			snakes.Add(currentSnake);
			snakesNum++;
		}

		for (var i = 1; i<snakes.Count; i++){
			if ((snakes [0].transform.position.x == snakes [i].transform.position.x) && (snakes [0].transform.position.y == snakes [i].transform.position.y)){
				remove();
				Debug.Log("!!!!!!");
			}
		}
	}

	void remove(){
		//Instantiate (end_text);
		//end_text.transform.parent.gameObject = Canvas.transform;
		//Instantiate (end_but);
		//end_but.transform.parent.gameObject = Canvas.transform;
	}
}
