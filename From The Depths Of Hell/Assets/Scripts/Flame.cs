using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    [SerializeField] private float startOffset = 0.0f;
    [SerializeField] private float rate = 0.0f;
    [SerializeField] private AudioClip FlameSound;
    [SerializeField] private GameObject Player;
    
    private BoxCollider2D BoxCollider2DReference;
    private SpriteRenderer SpriteRenderedReference;
    private AudioSource AudioSrc;
    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2DReference = GetComponent<BoxCollider2D>();
        SpriteRenderedReference = GetComponent<SpriteRenderer>();
        AudioSrc = GetComponent<AudioSource>();
        
        InvokeRepeating("TriggerCollider", startOffset, rate);
    }

    private void TriggerCollider()
    {
        
        if(Vector2.Distance(Player.transform.position, transform.position) < 10.0f)
        {
            if(BoxCollider2DReference.enabled)
            {
                AudioSrc.Stop();
            }
            else
            {
                AudioSrc.PlayOneShot(FlameSound);
            }  
        }
        
        BoxCollider2DReference.enabled = !BoxCollider2DReference.enabled;
        SpriteRenderedReference.enabled = !SpriteRenderedReference.enabled;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        float distance = other.gameObject.transform.position.x - BoxCollider2DReference.bounds.center.x;

        if(other.gameObject.tag == "Player")
        {
           PlayerMovement Player = other.gameObject.GetComponent<PlayerMovement>();
           Player.Knock(new Vector2(Mathf.Sign(distance), 1f)); 
        }
    }
}