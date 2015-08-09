﻿using System.Linq;
using UnityEngine;

public class BallBullet : MonoBehaviour{
    public int bulletSpeed = 2;
    private GameObject player;
    private bool bubbled = false;

    private void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player"&&!bubbled) {
            col.gameObject.SendMessage("Bubble");
            bubbled = true;
            Destroy(gameObject);
        }
    }

    private void Update(){
        if (player != null){
            Vector3 currentPosition = gameObject.transform.position;
            gameObject.transform.position = Vector3.Lerp(currentPosition, player.transform.position,
                Time.deltaTime/(player.transform.position - currentPosition).magnitude*bulletSpeed);
            Debug.Log(gameObject.transform.localScale.x);
            if (gameObject.transform.localScale.x < 4F)
            gameObject.transform.localScale = gameObject.transform.localScale + new Vector3(0.005F, 0.005F);
        }
    }
}