using UnityEngine;

public class BallBullet : MonoBehaviour {
    public float LifeTime;
    private float _time;
    private bool bubbled;
    public int bulletSpeed = 2;
    private GameObject player;

    private void Start() {
        API.Instance.PlaySound(API.Instance.BubbleShootSound, transform.position);
        player = GameObject.FindGameObjectWithTag("Player");
        _time = 0F;
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player" && !bubbled) {
            col.gameObject.SendMessage("Bubble");
            bubbled = true;
            Destroy(gameObject);
        }
    }

    private void Update() {
        if (player != null) {
            Vector3 currentPosition = gameObject.transform.position;
            gameObject.transform.position = Vector3.Lerp(currentPosition, player.transform.position,
                Time.deltaTime/(player.transform.position - currentPosition).magnitude*bulletSpeed);
            if (gameObject.transform.localScale.x < 1F)
                gameObject.transform.localScale = gameObject.transform.localScale + new Vector3(0.005F, 0.005F);

            _time += Time.deltaTime;
            if (_time > LifeTime) {
                Destroy(gameObject);
            }
        }
    }
}