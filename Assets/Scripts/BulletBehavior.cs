using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
	public float bulletSpeed = 100;
	public GameObject sparkVFX;

    // Start is called before the first frame update
	void Start()
	{
		Vector3 min = new Vector3(-90f, -90f, 0);
		Vector3 max = new Vector3(90f, 90f, 0);
		Vector3 randomVector = new Vector3(UnityEngine.Random.Range(min.x, max.x), UnityEngine.Random.Range(min.y, max.y), UnityEngine.Random.Range(min.z, max.z));
		Vector3 gunDirectionVariation = GameObject.FindObjectOfType<FollowMouse>().gunDirection * bulletSpeed + randomVector;

		GetComponent<Rigidbody>().AddForce(gunDirectionVariation);
		StartCoroutine("DieAfterSeconds");
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("BigEnemy")){

			hitObjectVFX();
			Destroy(gameObject);

		}
	}

	public void hitObjectVFX(){
		sparkVFX.transform.SetParent(null, true);
				sparkVFX.SetActive(true);


	}

	IEnumerator DieAfterSeconds()
	{
		yield return new WaitForSeconds(1);
		Destroy(gameObject);
	}
}
