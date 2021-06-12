using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBulletBehavior : MonoBehaviour
{
    public GameObject enemyBullet;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("DestroyBulletAfterSeconds");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= Vector3.right * .01f;
    }
    IEnumerator DestroyBulletAfterSeconds()
    {
        yield return new WaitForSeconds(5);
        Destroy(enemyBullet);
    }
}
