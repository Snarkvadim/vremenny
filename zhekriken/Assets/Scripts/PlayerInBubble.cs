using UnityEngine;

public class PlayerInBubble : MonoBehaviour{
    private Animator _anim;
    public GameObject bubblePrefab;
    private GameObject bubble;
    private bool _isInBubble = false;
    Rigidbody2D m_Rigidbody2D;

    public float TimeInBubble;

    private float _time;



    public void Bubble(){
        if (!_isInBubble){
            gameObject.SendMessage("Controlled", false);
            bubble = Instantiate(bubblePrefab);
            bubble.transform.position = Vector3.zero;
            bubble.transform.SetParent(gameObject.transform, false);
            _isInBubble = true;
            m_Rigidbody2D.isKinematic = true;
            _time = 0F;
        }
    }

    private void Start(){
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update(){
        if (_isInBubble){
            _time += Time.deltaTime;
            if (_time < TimeInBubble){
                gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                    gameObject.transform.position.y + 0.1f);
            }
            else{
                _isInBubble = false;
                Destroy(bubble);
                m_Rigidbody2D.isKinematic = false;
                gameObject.SendMessage("Controlled", true);
            }
        }
//        Debug.Log(Time.deltaTime);
    }
}