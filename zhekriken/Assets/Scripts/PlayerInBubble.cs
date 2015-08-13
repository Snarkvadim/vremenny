using UnityEngine;

public class PlayerInBubble : MonoBehaviour {
    public float TimeInBubble;
    private Animator _anim;
    private bool _isInBubble;

    private float _time;
    private GameObject bubble;
    public GameObject bubblePrefab;
    private Rigidbody2D m_Rigidbody2D;


    public void Bubble() {
        if (!_isInBubble) {
            gameObject.SendMessage("Controlled", false);
            bubble = Instantiate(bubblePrefab);
            bubble.transform.position = Vector3.zero;
            bubble.transform.SetParent(gameObject.transform, false);
            _isInBubble = true;
            m_Rigidbody2D.isKinematic = true;
            _time = 0F;
        }
    }

    private void Start() {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (_isInBubble) {
            _time += Time.deltaTime;
            if (_time < TimeInBubble) {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                    gameObject.transform.position.y + 0.1f);
            }
            else {
                API.Instance.PlaySound(API.Instance.BubbleCrashSound);
                _isInBubble = false;
                Destroy(bubble);
                m_Rigidbody2D.isKinematic = false;
                gameObject.SendMessage("Controlled", true);
            }
        }
//        Debug.Log(Time.deltaTime);
    }
}