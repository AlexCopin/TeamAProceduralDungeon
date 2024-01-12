using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] public GameObject _prefabProjectile;

    [SerializeField] float _speed;

    public void ShootProjectile(Vector2 startPosition)
    {
        Instantiate<GameObject>(_prefabProjectile, startPosition, Quaternion.Euler(transform.forward));
    }
}
