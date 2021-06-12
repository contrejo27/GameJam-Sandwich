using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControls : MonoBehaviour
{
    public float moveSpeed = 3;
    private bool isGrounded = true;
    public float jumpHeight = 100;
    public int jumpsLeft = 0;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public GameObject GameOver;
    public float speedClamp = 6f;
    public Animator CharacterAnimator;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        GameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (isGrounded && jumpsLeft > 0)
        {
            if (Input.GetButtonDown("Jump"))
            {
                isGrounded = false;

                CharacterAnimator.ResetTrigger("Walk");
                CharacterAnimator.ResetTrigger("Idle");
                CharacterAnimator.SetTrigger("Jump");

                GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight);
                jumpsLeft--;
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(10);
        }
        if (currentHealth <= 0)
        {
            GameOver.SetActive(true);
        }


        if (GetComponent<Rigidbody>().velocity.x < speedClamp && GetComponent<Rigidbody>().velocity.x > -speedClamp)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0) * moveSpeed);
        }

        if (Input.GetAxisRaw("Horizontal") ==0 && isGrounded)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
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
            jumpsLeft++;
        }

        if (collision.gameObject.tag == "EnemyBullet")
        {
            print("taking damage");
            TakeDamage(20);
            Destroy(collision.gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            GameObject.FindObjectOfType<WeaponBehavior>().ammo += 10;
            Destroy(other.gameObject);
        }
    }
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
