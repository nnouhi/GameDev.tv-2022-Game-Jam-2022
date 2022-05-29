using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private AudioClip ShootingSound;
    [SerializeField] private GameObject Player;

    public ProjectileBehaviour ProjectileReference;
    public Transform LaunchOffset;
    
    private AudioSource AudioSrc;
    private Animator ProjectileSpawnerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        AudioSrc = GetComponent<AudioSource>();
        ProjectileSpawnerAnimator = GetComponent<Animator>();
        InvokeRepeating("SpawnProjectile", 0.0f, 3.0f);
    }

    private void SpawnProjectile()
    {
        if(PlaySound())
        {
            AudioSrc.PlayOneShot(ShootingSound);
        }
        ProjectileSpawnerAnimator.SetTrigger("ShootTrigger");
        Instantiate(ProjectileReference, LaunchOffset.position, transform.rotation);
    }

    private bool PlaySound()
    {
        return Vector2.Distance(Player.transform.position, transform.position) < 10.0f ;
    }
}
