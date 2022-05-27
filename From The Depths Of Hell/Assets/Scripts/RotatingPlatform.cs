using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [SerializeField] float RotationSpeed = 0.0f;


    private float NewRotation = 0.0f;
    private BoxCollider2D BoxCollider2DReference;
    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2DReference = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
   
    }
    
    private void OnCollisionStay2D(Collision2D other)
    {    
        float Distance  = other.gameObject.transform.position.x - BoxCollider2DReference.bounds.center.x;
        NewRotation -= Distance * RotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(new Vector3(0,0,NewRotation));
    }
}
