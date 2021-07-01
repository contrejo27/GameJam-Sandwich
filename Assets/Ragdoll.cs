using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
	public int maxHp = 10;
	private int hp;

	void SetKinematic(bool newValue)
	{
		Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
		foreach (Rigidbody rb in bodies)
		{
			rb.isKinematic = newValue;
		}

		//Invoke("Die",2);
	}

	void Start()
	{
		SetKinematic(true);
		hp = maxHp;
	}

	public void Damage()
	{
		if (hp <= 0) return;
		hp -= 10;
		if (hp <= 0) Die();
	}

	void Die()
	{
		print("turning kinematic off");
		SetKinematic(false);
		GetComponent<Animator>().enabled = false;
		Destroy(gameObject, 5);
	}
}
