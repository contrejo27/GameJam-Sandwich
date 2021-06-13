using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControls : MonoBehaviour
{
    public float moveSpeed = 3;
    private bool isGrounded = true;
    public float jumpHeight = 100;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public GameObject GameOver;
    public float speedClamp = 6f;
    public Animator CharacterAnimator;
    public GameObject HitPlayer;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        GameOver.SetActive(false);
    }


    void Update()
    {
        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                isGrounded = false;

                CharacterAnimator.ResetTrigger("Walk");
                CharacterAnimator.ResetTrigger("Idle");
                CharacterAnimator.SetTrigger("Jump");

                GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (currentHealth <= 0)
        {
            GameOver.SetActive(true);
            Time.timeScale = 0f;
        }


        if (GetComponent<Rigidbody>().velocity.x < speedClamp && GetComponent<Rigidbody>().velocity.x > -speedClamp)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0) * moveSpeed);
        }


        if (Input.GetAxisRaw("Horizontal") ==0)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0f, GetComponent<Rigidbody>().velocity.y, 0f);
        }

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            CharacterAnimator.ResetTrigger("Idle");

            CharacterAnimator.SetTrigger("Walk");
        }
        else
        {
            CharacterAnimator.ResetTrigger("Walk");

            CharacterAnimator.SetTrigger("Idle");

        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage(16);
        }
        if (collision.gameObject.tag == "BigEnemy")
        {
            TakeDamage(20);
            GetComponent<Rigidbody>().AddForce(Vector3.left * 1000 + Vector3.up *100);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            GameObject.FindObjectOfType<WeaponBehavior>().ammo += 10;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            TakeDamage(20);
            GameObject test = Instantiate(HitPlayer, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }

    }
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
