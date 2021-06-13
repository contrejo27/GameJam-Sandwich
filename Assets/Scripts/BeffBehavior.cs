using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeffBehavior : MonoBehaviour
{
    bool movingLeft;
    public float enemySpeed = 5f;
    Vector3 startPosition;
    public GameObject Explosion;
    public int enemyHealth = 15;
    public GameObject hitPlayer;
    public GameObject hitAnimationPosition;

    public bool enemyStartPaused = false;
    public Animator CharacterAnimator;


    private bool isGrounded = true;

    float ogEnemySpeed;
    // Start is called before the first frame update
    void Start()
    {
        if (enemyStartPaused)
        {
            ogEnemySpeed = enemySpeed;
            enemySpeed = 0;
        }
        startPosition = transform.position;

        InvokeRepeating("Jump", 3f, 4);
        InvokeRepeating("Jump", 1f, 7);

    }
    public void Jump()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * 450);
        isGrounded = false;
        CharacterAnimator.ResetTrigger("walk");
        CharacterAnimator.ResetTrigger("idle");
        CharacterAnimator.SetTrigger("jump");

    }

    public void UnpauseEnemy()
    {
        enemySpeed = ogEnemySpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (transform.position.x > startPosition.x - 7)
        {
            movingLeft = false;
        }

        if (transform.position.x < startPosition.x + 4)
        {
            movingLeft = true;
        }

        if (isGrounded)
        {


            if (movingLeft == true)
            {
                GetComponent<Rigidbody>().AddForce(Vector3.right * enemySpeed);
                CharacterAnimator.ResetTrigger("idle");
                CharacterAnimator.ResetTrigger("jump");

                CharacterAnimator.SetTrigger("walk");
            }
            else
            {
                GetComponent<Rigidbody>().AddForce(Vector3.left * enemySpeed);
                CharacterAnimator.ResetTrigger("idle");
                CharacterAnimator.ResetTrigger("jump");

                CharacterAnimator.SetTrigger("walkBack");
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
        private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Instantiate(hitPlayer, hitAnimationPosition.transform.position, Quaternion.identity);
            enemyHealth--;

            if (enemyHealth == 0)
            {
                Destroy(gameObject);
                Instantiate(Explosion, transform.position, Quaternion.identity);
            }
        }
    }
}
