using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    Rigidbody2D _body;
    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _body.velocity = transform.right * _speed;
    }
}
