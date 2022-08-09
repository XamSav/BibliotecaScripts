using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] [Range(0, 100)] private float _vel = 2f;
    [SerializeField] [Range(0, 10)] private float _velCamera = 2f;
    private Animator _anim;
    private bool isAlive = true;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (isAlive)
        {
            Move();
        }   
    }
    private void Move()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        _anim.SetFloat("Speed_V", v);
        if (Input.GetKey(KeyCode.LeftShift) && v > 0)
        {
            _anim.SetBool("Run", true);
            v *= 2;
        }
        else
            _anim.SetBool("Run", false);
        if (Input.GetKey(KeyCode.LeftControl))
        {
            _anim.SetBool("Agachar", true);
            v /= 2;
        }
        else
            _anim.SetBool("Agachar", false);
        transform.Translate(new Vector3(h, 0, v) * _vel * Time.deltaTime);
    }
    private void MoveMouse()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.visible = !Cursor.visible;
        }
        if (Input.GetAxis("Mouse X") > 0)
        {
            transform.Rotate(Vector3.up * _velCamera);
        }
        if (Input.GetAxis("Mouse X") < 0)
        {
            transform.Rotate(Vector3.up * -_velCamera);
        }
    }
    private void LateUpdate()
    {
        if (isAlive)
        {
            MoveMouse();
        }
    }
    public void Muerte()
    {
        isAlive = false;
        _anim.SetBool("Muerte", true);
    }
}
