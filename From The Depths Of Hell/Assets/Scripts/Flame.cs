using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    [SerializeField] private float startOffset = 0.0f;
    [SerializeField] private float rate = 0.0f;

    private BoxCollider2D BoxCollider2DReference;
    private SpriteRenderer SpriteRenderedReference;
    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2DReference = GetComponent<BoxCollider2D>();
        SpriteRenderedReference = GetComponent<SpriteRenderer>();

        InvokeRepeating("TriggerCollider", startOffset, rate);
    }

    private void TriggerCollider()
    {
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
