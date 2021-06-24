using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX_AutoDestruct : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        Invoke("Deactivate", 1);
    }

    void Deactivate()
    {
        print("deactivateFX");
        gameObject.SetActive(false);
    }
}
