using UnityEngine;

[RequireComponent(typeof (Collider2D))]
public class WindowEnemy : MonoBehaviour {
    public GameObject EnemyToSpawn;

    private bool _isExplose;
    private Animator anim;
    private GameObject enemy;


    private void Awake() {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            if (!_isExplose) {
                anim.SetBool("Explose", true);
                API.Instance.PlaySound(API.Instance.ExplosionSound, transform.position);
                _isExplose = true;
            }
            if (enemy != null) {
                enemy.SendMessage("PlayerInTrigger", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            if (enemy != null) {
                enemy.SendMessage("PlayerInTrigger", false);
            }
        }
    }

    private void RespawnEnemy() {
        if (EnemyToSpawn != null) {
            enemy = Instantiate(EnemyToSpawn);
            enemy.transform.position = gameObject.transform.position;
            enemy.transform.SetParent(gameObject.transform.parent);
        }
    }
}