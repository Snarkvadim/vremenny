using UnityEngine;

public class RabbitController : MonoBehaviour {
    private const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    public float Speed = 5;
    private bool _grounded = true; // Whether or not the player is grounded.
    private Animator anim;
    private bool m_FacingRight = true;
    private Transform m_GroundCheck; // A position marking where to check if the player is grounded.

    [SerializeField] private float m_JumpForce = 400f;
    private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private LayerMask m_WhatIsGround;

    private bool m_Grounded {
        get { return _grounded; }
        set {
            anim.SetBool("Grounded", value);
            _grounded = value;
        }
    }

    private void Awake() {
        m_GroundCheck = transform.Find("GroundCheck");
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update() {
    }

    public void Move(float move) {
        anim.SetFloat("Speed", Mathf.Abs(move));
        m_Rigidbody2D.velocity = new Vector2(move*Speed, m_Rigidbody2D.velocity.y);
        if (move > 0 && !m_FacingRight) {
            Flip();
        }
        else if (move < 0 && m_FacingRight) {
            Flip();
        }
//            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
    }

    public void Jump() {
        if (m_Grounded) {
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }

    private void FixedUpdate() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius);
        for (int i = 0; i < colliders.Length; i++) {
            if (colliders[i].gameObject != gameObject && !colliders[i].isTrigger)
                if (!m_Grounded)
                    m_Grounded = true;
        }
//        anim.SetFloat("Speed", m_Rigidbody2D.velocity.y);
    }

    private void Flip() {
        m_FacingRight = !m_FacingRight;
        transform.RotateAround(transform.position, Vector3.up, 180f);
    }
}