using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boolrealis : MonoBehaviour
{
    public float lifetime = 2.0f;
    public float bulletSpeed = 10f;
    public int damage = 1;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
