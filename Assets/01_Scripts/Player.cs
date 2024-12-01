using System.Collections;

using System.Collections.Generic;

using Unity.Mathematics;

using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour

{
    public int maxHealth = 30; // Vida máxima del jugador
    public int currentHealth;  // Vida actual
    public int score = 0;      // Puntaje actual

    public Text healthText;    // Referencia al texto de vida en el Canvas
    public Text scoreText;

    public Rigidbody2D rb;

    public float speed = 6f;

    public Bullet bulletPrefap;

    public Transform firePoint;

    public float jumpForce = 10f;

    public Transform groundCheck;

    public LayerMask capapiso;

    public Transform gun;

    public SpriteRenderer gunSR;

    Vector3 targetRotation;

    public GameObject ball;

    Vector3 finaltarget;

    private Vector2 shootDirection;

    private bool isGrounded;

    private int jumpCount = 0;

    private const int maxJumps = 2;

    void Start()
    {

        transform.rotation = Quaternion.identity;


        currentHealth = maxHealth; // Inicia con vida máxima
        UpdateUI();

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0); // Evita que la vida sea negativa
        UpdateUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth); // Evita superar la vida máxima
        UpdateUI();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateUI();
    }

    void UpdateUI()
    {
        healthText.text = "Vida: " + currentHealth;
        scoreText.text = "Score: " + score;
    }

    void Die()
    {
        Debug.Log("El jugador ha muerto.");
        // Lógica para reiniciar o terminar el juego
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
    {
            TakeDamage(10); // Recibir daño al chocar con un enemigo
        }
    }

    void Update()

    {

        Movement();  // Movimiento del jugador

        Jump();  // Salto del jugador

        // Hacer que el arma gire con el movimiento del jugador

        if (rb.velocity.x > 0) // Si el personaje se mueve hacia la derecha

        {

            gun.rotation = Quaternion.Euler(0, 0, 0); // El arma se orienta hacia la derecha

        }

        else if (rb.velocity.x < 0) // Si el personaje se mueve hacia la izquierda

        {

            gun.rotation = Quaternion.Euler(0, 180, 0); // El arma se orienta hacia la izquierda

        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) // Disparo cuando se hace clic izquierdo

        {

            Shoot();

        }

    }

    void Movement()

    {

        float x = Input.GetAxisRaw("Horizontal");  // Entrada horizontal (teclas A/D o flechas)

        Vector2 move = new Vector2(x, 0).normalized * speed;  // Movimiento del jugador

        rb.velocity = new Vector2(move.x, rb.velocity.y);  // Actualizamos la velocidad del Rigidbody2D

        // Cambiar la rotación del personaje en función del movimiento

        if (move.x > 0)

        {

            transform.rotation = Quaternion.Euler(0, 0, 0); // Mirando hacia la derecha

        }

        else if (move.x < 0)

        {

            transform.rotation = Quaternion.Euler(0, 180, 0); // Mirando hacia la izquierda

        }

    }

    void Shoot()

    {

        ShootBullet();

    }

    void ShootBullet()

    {

        // Obtener la dirección desde el FirePoint

        Vector2 shootDirection = firePoint.up; // Usamos "up" para la dirección de la rotación del firePoint

        Bullet bullet = Instantiate(bulletPrefap, firePoint.position, firePoint.rotation);  // Instanciamos la bala

        // Establecemos la dirección de la bala para que siga la rotación del firePoint

        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * bullet.speed;

    }

    void Jump()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, capapiso);  // Comprobamos si está en el suelo

        if (isGrounded)
        {

            jumpCount = 0;  // Reiniciamos el contador de saltos si estamos en el suelo

        }

        if (Input.GetKeyDown(KeyCode.W)) // Si presionamos la tecla de salto (espacio)
        {

            if (isGrounded || jumpCount < maxJumps) // Permitir hasta dos saltos
            {

                rb.velocity = new Vector2(rb.velocity.x, 0);  // Cancelamos el movimiento vertical actual

                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);  // Aplicamos fuerza hacia arriba

                jumpCount++;  // Incrementamos el contador de saltos

            }   

        }

    }

}

