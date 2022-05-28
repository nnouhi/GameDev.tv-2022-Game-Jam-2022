using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float RespawnTime = 10.0f;

    private new BoxCollider2D collider;
    private new SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        renderer = GetComponent<SpriteRenderer>();
        Spawn();
    }

    public void Collect()
    {
        collider.enabled = false;
        renderer.enabled = false;
        Invoke("Spawn", RespawnTime);
    }

    private void Spawn()
    {
        collider.enabled = true;
        renderer.enabled = true;
    }
}
