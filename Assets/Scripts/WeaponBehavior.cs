using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour
{
    public Vector3 gunDirection;
    public GameObject bulletPrefab;
    public Transform muzzlePos;

    public int ammo = 10;
    // Start is called before the first frame update
    void Start()
    {
        gunDirection = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        transform.LookAt(Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 40)));
        gunDirection = transform.forward;

        if (Input.GetMouseButtonDown(0))
        {
            if (ammo > 0)
            { 
                Instantiate(bulletPrefab, muzzlePos.position, Quaternion.identity);
                ammo--;
            }
        }

    }
}
