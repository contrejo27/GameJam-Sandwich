using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacerVisuals : MonoBehaviour
{
    public GameObject mainChar;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnChar",.5f);
    }

    void SpawnChar()
    {
        mainChar.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
