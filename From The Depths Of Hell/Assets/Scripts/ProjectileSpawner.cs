using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public ProjectileBehaviour ProjectileReference;
    public Transform LaunchOffset;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnProjectile", 0.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnProjectile()
    {
        Instantiate(ProjectileReference, LaunchOffset.position, transform.rotation);
    }
}
