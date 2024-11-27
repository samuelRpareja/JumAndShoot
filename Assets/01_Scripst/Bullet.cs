using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // Velocidad de la bala
    public float lifetime = 5f; // Tiempo de vida de la bala antes de destruirse

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtenemos el Rigidbody2D de la bala

        // La bala se moverá en la dirección en que se apuntó el arma
        rb.velocity = transform.up * speed;

        // Destruir la bala después de un tiempo
        Destroy(gameObject, lifetime);
    }

    // Si la bala colisiona con algo
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);  // Destruye al enemigo
            Destroy(gameObject);            // Destruye la bala
        }
    }
}
