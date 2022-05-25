using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblingPlatform : MonoBehaviour
{
    [SerializeField] private float resetTime = 5.0f;
    [SerializeField] private float crumbleTime = 1.0f;
    private Animator animator;

    private float resetTimer = -1.0f;
    private float crumbleTimer = -1.0f;
    private bool crumbled;
    private bool startCrumbling;

    // Start is called before the first frame update
    void Start()
    {
        crumbled = false;
        startCrumbling = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimers();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        StartCrumbling();
    }

    private void UpdateTimers()
    {
        if (crumbled)
        {
            if (resetTimer < 0.0f) // Reset platform to normal
            {
                Reset();
            }
            resetTimer -= Time.deltaTime;
        }

        if (startCrumbling)
        {
            if (crumbleTimer < 0.0f) // Platform destroyed
            {
                Crumble();
            }
            crumbleTimer -= Time.deltaTime;
        }
    }

    private void StartCrumbling()
    {
        if (!startCrumbling)
        {
            crumbleTimer = crumbleTime;
            startCrumbling = true;

            animator.SetTrigger("Crumble");
        }
    }

    private void Crumble()
    {
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        startCrumbling = false;
        crumbled = true;
        resetTimer = resetTime;
    }

    private void Reset()
    {
        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        crumbled = false;

        animator.SetTrigger("Reset");
    }
}