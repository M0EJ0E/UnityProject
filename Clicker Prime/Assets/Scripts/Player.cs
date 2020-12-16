using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _jump;
    public float upForce;
    public Rigidbody2D rb2d;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            //anim.SetTrigger("Flap");
            //...zero out the birds current y velocity before...
            rb2d.velocity = Vector2.zero;
            new Vector2(rb2d.velocity.x, 0);
            //..giving the bird some upward force.
            rb2d.AddForce(new Vector2(0, upForce));
        }
    }
}