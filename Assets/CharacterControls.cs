using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControls : MonoBehaviour
{
    public float moveSpeed = 3;
    private bool isGrounded = false;
    public float jumpHeight = 100;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (!isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight);

            }
        }

        GetComponent<Rigidbody>().AddForce(new Vector3(Input.GetAxis("Horizontal"), 0, 0) * moveSpeed);

    }
}
