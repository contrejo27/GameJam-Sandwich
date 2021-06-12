using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public Transform BulletSpawn;
    public GameObject enemyBulletPrefab;
    public float bulletSpeed = 20;
    bool movingUp;
    public float enemySpeed = 5f;
    Vector3 startPosition;
    public GameObject Explosion;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        StartCoroutine("ShootAtRandomSeconds");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y < startPosition.y + .5)
        {

            movingUp = false;
        }
     
        if (transform.position.y > startPosition.y + .5)
        {
            movingUp = true;
        }

        if (movingUp == true)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.down * enemySpeed);
        }
        else
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * enemySpeed);
        }
    }
    IEnumerator ShootAtRandomSeconds()
    {
            yield return new WaitForSeconds(Random.Range(1,3));
            Instantiate(enemyBulletPrefab, BulletSpawn.transform.position, Quaternion.identity);
            StartCoroutine("ShootAtRandomSeconds");
      
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
            Instantiate(Explosion, transform.position, Quaternion.identity);
        }
    }
}
