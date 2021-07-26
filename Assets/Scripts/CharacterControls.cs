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
    Vector3 faceRight;
    Vector3 faceLeft;


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
    public GameObject AmmoPowerUpVFX;

    //internal
    private bool isGrounded = true;
    bool doubleJump = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        GameOver.SetActive(false);

        faceRight = transform.rotation.eulerAngles;
        faceLeft = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 180, transform.rotation.eulerAngles.z);

        Cursor.visible = false;

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && !isGrounded)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.down * 5, ForceMode.VelocityChange);
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (doubleJump)
            {
                GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 0, 0);
                GetComponent<Rigidbody>().AddForce(Vector3.up * doubleJumpHeight, ForceMode.VelocityChange);

                CharacterAnimator.ResetTrigger("Jump");
                CharacterAnimator.SetTrigger("DoubleJump");

                doubleJump = false;
                doubleJumpVFX.SetActive(true);
            }
            if (isGrounded)
            {
                isGrounded = false;
                doubleJump = true;

                CharacterAnimator.ResetTrigger("Walk");
                CharacterAnimator.ResetTrigger("Idle");
                CharacterAnimator.ResetTrigger("Landing");

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
            Cursor.visible = true;

            GameOver.SetActive(true);
            Time.timeScale = 0f;
        }

        //controls character running animation speed
        CharacterAnimator.SetFloat("Speed", GetComponent<Rigidbody>().velocity.x / speedClamp);

        if (GetComponent<Rigidbody>().velocity.x < speedClamp && GetComponent<Rigidbody>().velocity.x > -speedClamp)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0) * moveSpeed, ForceMode.VelocityChange);
        }


        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            CharacterAnimator.gameObject.transform.localPosition = new Vector3(0,
                                                                          CharacterAnimator.gameObject.transform.localPosition.y,
                                                                          CharacterAnimator.gameObject.transform.localPosition.z);
            transform.rotation = Quaternion.Euler(faceLeft);
        }

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            CharacterAnimator.gameObject.transform.localPosition = new Vector3(0,
                                                                          CharacterAnimator.gameObject.transform.localPosition.y,
                                                                          CharacterAnimator.gameObject.transform.localPosition.z);  
            transform.rotation = Quaternion.Euler(faceRight);
        }

        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x * .8f, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
        }
        else
        {
            if (isGrounded)
            {
                CharacterAnimator.SetTrigger("Walk");
            }
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            doubleJump = false;
            CharacterAnimator.ResetTrigger("Jump");
            CharacterAnimator.ResetTrigger("DoubleJump");

            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                CharacterAnimator.SetTrigger("Landing");
            }
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
            AmmoPowerUpVFX.SetActive(true);

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
