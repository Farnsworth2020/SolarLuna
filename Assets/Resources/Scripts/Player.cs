using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Transform playerPos;
    private Rigidbody2D playerRB;
    public float attackValue;
    public float speed;
    public float Health;
    private float startHealth;
    public bool useGoldInsteadOfHP;
    private Transform attackPos;
    private void Awake()
    {
        playerPos = transform;
        playerRB=GetComponent<Rigidbody2D>();
        startHealth = Health;
        attackPos = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetMouseButton(0))
            MoveMouse();
        if (Input.GetMouseButton(1))
            Attack();
    }

    void Move()
    {
        var moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        var moveY = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        playerPos.position = new Vector3(playerPos.position.x + moveX, playerPos.position.y + moveY, -0.5f);
    }

    void MoveMouse()
    {
        var newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerPos.position = Vector2.MoveTowards(playerPos.position, 
            newPos, speed * Time.deltaTime);
    }

    public void GetDamage(float value)
    {
        if (useGoldInsteadOfHP)
        {
            //gold-=value
        }
        else
        {
            Health -= value;
        }
    }

    void Attack()
    {
        attackPos.gameObject.SetActive(true);
        Invoke("TurnOffAttack",0.5f);
    }

    void TurnOffAttack()
    {
        attackPos.gameObject.SetActive(false);
    }
}
