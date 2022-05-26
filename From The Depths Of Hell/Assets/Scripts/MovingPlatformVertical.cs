using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformVertical : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] private float speed = 2;
    [SerializeField] private float range = 2;
    [SerializeField, Range(-1, 1)] private int startDirection = 1;

    private Vector2 startPos;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position; // Save start position
        direction = new Vector2(0, startDirection); // Move direction is on the y axis
    }

    // Update is called once per frame
    void Update()
    {
        // If reached bounds
        if (ReachedBounds())
        {
            // Switch direction
            direction *= -1;
        }

        this.transform.Translate(Time.deltaTime * speed * direction);
    }

    private bool ReachedBounds()
    {
        return this.transform.position.y > startPos.y + range && direction.y > 0
            || this.transform.position.y < startPos.y - range && direction.y < 0;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        // If player is on platform
        if (other.gameObject == player)
        {
            // Move player along with platfom
            player.transform.parent = this.transform;
        }
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        // When player gets of platform stop moving him
        if (other.gameObject == player)
        {
            player.transform.parent = null;
        }
    }
}
