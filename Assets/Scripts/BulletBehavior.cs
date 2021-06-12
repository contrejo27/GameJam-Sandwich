using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float bulletSpeed = 100;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 min = new Vector3(-70f, -70f, 0);
        Vector3 max = new Vector3(70f, 70f, 0);
        Vector3 randomVector = new Vector3(UnityEngine.Random.Range(min.x, max.x), UnityEngine.Random.Range(min.y, max.y), UnityEngine.Random.Range(min.z, max.z));
        Vector3 gunDirectionVariation = GameObject.FindObjectOfType<WeaponBehavior>().gunDirection * 500 + randomVector;

        GetComponent<Rigidbody>().AddForce(gunDirectionVariation );
        StartCoroutine("DieAfterSeconds");
    }

    IEnumerator DieAfterSeconds()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
