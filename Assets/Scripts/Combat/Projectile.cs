using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    public Attack attack;

    Rigidbody2D _body;
    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _body.velocity = transform.right * _speed;
    }
    private void Start()
    {
        Destroy(gameObject, 5.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            collision.gameObject.GetComponent<Player>().ApplyHit(attack);
            Destroy(gameObject);
        }
    }
}
