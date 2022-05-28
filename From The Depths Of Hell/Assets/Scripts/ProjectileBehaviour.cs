using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] private float ProjectileSpeed = 0.0f;

    private Rigidbody2D Rigidbody2DReference;
    

    void Start()
    {
        Rigidbody2DReference = GetComponent<Rigidbody2D>();
        Rigidbody2DReference.velocity = (new Vector2(transform.right.x, transform.right.y)) * ProjectileSpeed;
        Invoke("Destroy", 5.0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
           PlayerMovement Player = other.gameObject.GetComponent<PlayerMovement>();
           Player.Knock(new Vector2(Mathf.Sign(Rigidbody2DReference.velocity.x), 1f)); 
        }   

        Destroy(); 
    }
    
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
