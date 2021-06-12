using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControls : MonoBehaviour
{
    public float moveSpeed = 3;
    private bool isGrounded = true;
    public float jumpHeight = 100;
    public int jumpsLeft = 1;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public GameObject GameOver;
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
            Destroy(gameObject);
            GameOver.SetActive(true);
        }

        GetComponent<Rigidbody>().AddForce(new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0) * moveSpeed);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            jumpsLeft++;
        }
        else
        {
            isGrounded = false;
        }
    }
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
