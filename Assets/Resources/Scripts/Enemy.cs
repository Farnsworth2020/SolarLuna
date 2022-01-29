using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D enemyRB;
    private Transform enemyPos, playerPos;
    private Player _player;
    public float attackValue;
    public float speed;
    public float Health;
    private float startHealth;
    private bool isNearPlayer = false;
    private bool blockAttack=false;
    public float blockAttackTime;
    public bool rangedAttack;
    public GameObject _shellPrefab;
    private Shell _shell;
    public Transform AttackPosition;
    public float distanceToAttack;
    private bool dontDoAnything;

    public bool DontDoAnything
    {
        get => dontDoAnything;
        set => dontDoAnything = value;
    }

    private void Awake()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        enemyPos = transform;
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        _player = playerPos.GetComponent<Player>();
        startHealth = Health;
        DontDoAnything = false;
    }

    private void Update()
    {
        if (DontDoAnything) return;
        if (Vector2.Distance(playerPos.position, enemyPos.position) > distanceToAttack)
        {
            FollowPlayer();
            isNearPlayer = false;
        }
        else
        {
            isNearPlayer = true;
            if (rangedAttack) 
                RangeAttack();
            else
                Attack();
        }
            
    }

    void FollowPlayer()
    {
        if (isNearPlayer) return;
        enemyPos.position = Vector2.MoveTowards(enemyPos.position, playerPos.position, 
            Time.deltaTime * speed);
    }

    void Attack()
    {
        if (blockAttack) return;
        Debug.Log("attacked player");
        _player.GetDamage(attackValue);
        blockAttack = true;
        Invoke("UnlockAttack",blockAttackTime);
    }

    public void GetDamage(float value)
    {
        Health -= value;
    }

    void UnlockAttack()
    {
        blockAttack = false;
    }

    void RangeAttack()
    {
        if (blockAttack) return;
        blockAttack = true;
        Invoke("UnlockAttack",blockAttackTime);
        _shell = _shellPrefab.GetComponent<Shell>();
        _shell.DamageValue = attackValue;
        Shell newBullet = Instantiate(_shell, AttackPosition.position, _shell.transform.rotation);
        newBullet.Parent = gameObject;
        newBullet.Direction = AttackPosition.transform.localPosition;
    }
}
