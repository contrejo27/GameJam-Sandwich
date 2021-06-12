using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float bulletSpeed = 100;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(GameObject.FindObjectOfType<WeaponBehavior>().gunDirection * 1000);
        StartCoroutine("DieAfterSeconds");
    }

    IEnumerator DieAfterSeconds()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
