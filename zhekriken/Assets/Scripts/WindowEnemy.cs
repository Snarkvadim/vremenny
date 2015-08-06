using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class WindowEnemy : MonoBehaviour{
    private Animator anim;

    private void Awake(){
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            anim.SetBool("Explose", true);
        }
    }
   
}
