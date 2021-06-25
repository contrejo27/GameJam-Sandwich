using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour
{
    public enum GunType { Shotgun};

    public GunType currentGunType;

    public GameObject bulletPrefab;
    public Transform muzzlePos;

    public int ammo = 10;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetMouseButtonDown(0))
        {
            if (ammo > 0)
            { 
                if(currentGunType == GunType.Shotgun)
                {
                    Instantiate(bulletPrefab, muzzlePos.position, Quaternion.identity);
                    Instantiate(bulletPrefab, muzzlePos.position, Quaternion.identity);
                    Instantiate(bulletPrefab, muzzlePos.position, Quaternion.identity);
                    Instantiate(bulletPrefab, muzzlePos.position, Quaternion.identity);
                    Instantiate(bulletPrefab, muzzlePos.position, Quaternion.identity);
                }
                else Instantiate(bulletPrefab, muzzlePos.position, Quaternion.identity);

                ammo--;
                
            }
        }

    }
}
