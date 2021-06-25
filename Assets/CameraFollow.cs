using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform mainChar;
    public float cameraHeight;
    public float cameraFar;
    public float cameraAhead;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(mainChar.position.x + cameraAhead, mainChar.position.y + cameraHeight, mainChar.position.z + cameraFar); ;
    }
}
