using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject followingObject;
    [SerializeField] private float range = 4.0f;
    [SerializeField] private float offset = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (followingObject)
        {
            float objectY = followingObject.transform.position.y;
            if (Mathf.Abs(objectY - this.transform.position.y) > range)
            {
                this.transform.position = new Vector3(this.transform.position.x, objectY + offset, this.transform.position.z);
            }
        }
    }
}
