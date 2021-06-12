using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControls : MonoBehaviour
{
    public float moveSpeed = 3;
    private bool isGrounded = true;
    public float jumpHeight = 100;
    public int jumpsLeft = 1;
    // Start is called before the first frame update
    void Start()
    {

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
            print("not ground");
        }
    }
}
