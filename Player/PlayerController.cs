using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] [Range(0, 5)] private float _speed = 10f;
    [SerializeField] [Range(0, 10)] private float _jumpForce = 2;
    private bool inGround = true;
    private Rigidbody2D _rb;
    private Animator _anim;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }
    void Update()
    {
        Move();
        Jump();
    }
    void Jump()
    {
        float y = Input.GetAxis("Vertical");
        if (y > 0.1 && inGround)
        {
            inGround = false;
            _rb.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
        }
    }
    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        _anim.SetFloat("Walk", h);
        _rb.velocity = new Vector2(h * _speed, _rb.velocity.y);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ground"))
        {
            inGround = true;
        }
    }
}
