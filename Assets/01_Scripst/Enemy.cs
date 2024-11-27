using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxLife = 3;
    private float life = 3;

    public float speed = 1f; // Velocidad reducida a la mitad
    private Transform target;

    void Start()
    {
        life = maxLife;

        // Buscar al jugador como objetivo
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
    }

    void Update()
    {
        if (target != null)
        {
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        // Calcular la dirección hacia el jugador
        Vector2 direction = (target.position - transform.position).normalized;

        // Mover al enemigo en dirección al jugador sin cambiar rotación
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bala"))
        {
            Destroy(gameObject);
        }
    }
}
