using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControls : MonoBehaviour
{
    //movement tuning
    public float moveSpeed = 3;
    public float jumpHeight = 100;
    public float doubleJumpHeight = 100;
    public float speedClamp = 6f;
    Vector3 OGRotation;


    //health
    public int maxHealth = 100;
    public int currentHealth;

    //UI
    public HealthBar healthBar;
    public GameObject GameOver;
    public Animator CharacterAnimator;

    //FX
    public GameObject HitPlayer;
    public GameObject doubleJumpVFX;

    //internal
    private bool isGrounded = true;
    bool doubleJump = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        GameOver.SetActive(false);

         OGRotation = transform.rotation.eulerAngles;
    }


    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (doubleJump)
            {

                GetComponent<Rigidbody>().AddForce(Vector3.up * doubleJumpHeight, ForceMode.VelocityChange);
                doubleJump = false;
                doubleJumpVFX.SetActive(true);
            }
            if (isGrounded)
            {
                isGrounded = false;
                doubleJump = true;

                CharacterAnimator.ResetTrigger("Walk");
                CharacterAnimator.ResetTrigger("Idle");
                CharacterAnimator.SetTrigger("Jump");

                GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
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

        CharacterAnimator.SetFloat("Speed", GetComponent<Rigidbody>().velocity.x / speedClamp);

        if (GetComponent<Rigidbody>().velocity.x < speedClamp && GetComponent<Rigidbody>().velocity.x > -speedClamp)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0) * moveSpeed, ForceMode.VelocityChange);
        }



        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x * .8f, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
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
            doubleJump = false;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage(16);
        }
        if (collision.gameObject.tag == "BigEnemy")
        {
            TakeDamage(20);
            GetComponent<Rigidbody>().AddForce(Vector3.left * 1000 + Vector3.up * 100);
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
