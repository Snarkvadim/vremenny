using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class WindowEnemy : MonoBehaviour{
    private Animator anim;
    public GameObject EnemyToSpawn;
    private GameObject enemy;

    private bool _isExplose = false;

    public AudioClip ExplosionSound;

    private void Awake(){
        anim = GetComponent<Animator>();
        
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            if (!_isExplose){
                anim.SetBool("Explose", true);
                if (ExplosionSound != null)
                    AudioSource.PlayClipAtPoint(ExplosionSound, transform.position);
                _isExplose = true;
            }
            if (enemy != null) {
                enemy.SendMessage("PlayerInTrigger", true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col){
        if (col.gameObject.tag == "Player") {
            if (enemy != null){
                enemy.SendMessage("PlayerInTrigger", false);
            }
        }
    }

    void RespawnEnemy(){
        if (EnemyToSpawn != null){
            enemy = Instantiate(EnemyToSpawn);
            enemy.transform.position = gameObject.transform.position;
            enemy.transform.SetParent(gameObject.transform.parent);
        }
    }
   
}
