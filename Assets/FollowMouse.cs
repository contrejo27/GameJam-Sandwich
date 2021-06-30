using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public Vector3 gunDirection;
    float cameraDistance;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        gunDirection = Vector3.zero;
        cameraDistance = Camera.main.transform.position.z * -1;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        target.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cameraDistance));
        target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, 0);
        gunDirection = transform.forward;
        transform.LookAt(new Vector3(target.transform.position.x, target.transform.position.y, 0));

    }
}
