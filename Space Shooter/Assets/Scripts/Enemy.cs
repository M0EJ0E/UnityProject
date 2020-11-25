﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float _speed = 4.0f;
    private Player _player;
    private Animator _anim;
 

    // Start is called before the first frame update
    void Start()
    {
       
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player is NULL");
        }

        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError("Animator is NULL");
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

           if (transform.position.y <= -8f)
        {
            float randomx = Random.Range(-9f, 9f);
            transform.position = new Vector3(randomx, 8, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 2.8f);

            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }

        }

        if (other.tag == "Laser")
        {
            _speed = 0;
            _anim.SetTrigger("OnEnemyDeath");
            Destroy(this.gameObject, 2.8f);

            if (_player != null)
            {
                _player.AddScore(Random.Range(3,12));
            }

            Destroy(other.gameObject); 
            
        }
    }
}