using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
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
        // Asegura que el jugador inicie con una rotación neutral
        transform.rotation = Quaternion.identity;
    }

    void Update()
    {
        Movement();
        Aim();
        Jump();

        targetRotation = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(targetRotation.y, targetRotation.x) * Mathf.Rad2Deg;
        gun.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (angle > 90 || angle < -90)
        {
            gunSR.flipY = true;
        }
        else
        {
            gunSR.flipY = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        Vector2 move = new Vector2(x, 0).normalized * speed;

        rb.velocity = new Vector2(move.x, rb.velocity.y);

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

    void Aim()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;
        shootDirection = direction;
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ShootBullet();
        }
    }

    void ShootBullet()
    {
        // Obtener la dirección desde el FirePoint
        Vector2 shootDirection = firePoint.up; // Usamos "up" para la dirección de la rotación del firePoint
        Bullet bullet = Instantiate(bulletPrefap, firePoint.position, firePoint.rotation);

        // Establecemos la dirección de la bala para que siga la rotación del firePoint
        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * bullet.speed;
    }


    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, capapiso);

        if (isGrounded)
        {
            jumpCount = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded || jumpCount < maxJumps)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpCount++;
            }
        }
    }
}
