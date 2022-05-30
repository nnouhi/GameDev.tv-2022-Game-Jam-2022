using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavenDoors : MonoBehaviour
{
    [SerializeField] private new GameObject camera;
    private bool interacted = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!interacted)
        {
            camera.GetComponent<CameraController>()?.EndGame();
            interacted = true;
        }
    }
}
