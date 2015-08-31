﻿using System.Linq;
using UnityEngine;
using System.Collections;

public class RabbitController : MonoBehaviour {

    private bool m_Grounded;            // Whether or not the player is grounded.
    private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
    private Rigidbody2D m_Rigidbody2D;
    [SerializeField]
    private float m_JumpForce = 400f;
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    [SerializeField]
    private LayerMask m_WhatIsGround;  
    private bool m_FacingRight = true; 
    public float Speed = 5;

	void Awake () {
        m_GroundCheck = transform.Find("GroundCheck");
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
	
	}

    public void Move(float move, bool jump)
    {

            // The Speed animator parameter is set to the absolute value of the horizontal input.
//            m_Anim.SetFloat("Speed", Mathf.Abs(move));

            // Move the character
            m_Rigidbody2D.velocity = new Vector2(move * Speed, m_Rigidbody2D.velocity.y);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }

        // If the player should jump...
//        if (m_Grounded && jump && m_Anim.GetBool("Ground"))
        if (m_Grounded && jump)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
//            m_Anim.SetBool("Ground", false);
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }

    private void FixedUpdate()
    {
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius);
        Debug.Log("colliders - " + colliders.Count());
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject && !colliders[i].isTrigger)
                m_Grounded = true;
        }
        Debug.Log("Grounded - "+m_Grounded);
//        m_Anim.SetBool("Ground", m_Grounded);

        // Set the vertical animation
//        m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
