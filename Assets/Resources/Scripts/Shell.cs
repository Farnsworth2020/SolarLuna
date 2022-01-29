using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    private GameObject parent;
    public GameObject Parent { set { parent = value; } get { return parent; } }

    private float speed = 10.0F;
    private Vector3 direction;
    public Vector3 Direction { set { direction = value; } }

    public float DamageValue;
    public Color Color
    {
        set { sprite.color = value; }
    }
    private SpriteRenderer sprite;
    private Player _player;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Start()
    {
        Destroy(gameObject, 1.4F);
    }


    private void Update()
    {
        var position = transform.position;
        position = Vector3.MoveTowards(position, position + direction, speed * Time.deltaTime);
        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            _player.GetDamage(DamageValue);
            Destroy(gameObject);
        }                  
        if (collider.CompareTag("enemy"))
        {
            collider.GetComponent<Enemy>().GetDamage(DamageValue);
            Destroy(gameObject);
        }  
    }
}