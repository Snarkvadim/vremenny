using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class WindowEnemy : MonoBehaviour{
    private Animator anim;
    public GameObject EnemyToSpawn;

    private void Awake(){
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            anim.SetBool("Explose", true);
        }
    }

    void RespawnEnemy(){
        if (EnemyToSpawn != null){
            GameObject enemy = Instantiate(EnemyToSpawn);
            enemy.transform.position = gameObject.transform.position;
            enemy.transform.SetParent(gameObject.transform.parent);
        }
    }
   
}
