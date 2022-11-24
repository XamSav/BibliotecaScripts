using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField][Range(0,5)] private float _speed = 10f;
    [SerializeField][Range(0,5)] private float _speed = 10f;
    [SerializeField] [Range(0, 10)] private float _jumpForce = 2;
    private Rigidbody2D _rb;
    private Animator _anim;
     void Start()
    {
        _rb = transform.parent.GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }
    void Update()
    {
        Move();
        Jump();
    }
    void Jump()
    {
        if ((_y > 0.1 || _isJump)&& !_isJumping)
        {
            _isJump = false;
            _rb.AddForce(new Vector2(0,_jumpForce), ForceMode2D.Impulse);
            //PlaySound("Jump");
        }
    }
    void Move(){
    float h = Input.GetAxis("Horizontal")
      _rb.velocity = new Vector2(h * _speed, _rb.velocity.y);
    }
}
