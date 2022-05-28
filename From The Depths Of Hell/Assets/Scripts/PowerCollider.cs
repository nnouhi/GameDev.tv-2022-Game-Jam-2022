using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerCollider : MonoBehaviour
{
    [SerializeField] float DoubleJumpTimer = 0.0f;
    [SerializeField] float JumpPowerTimer = 0.0f;
    [SerializeField] float SlowMotionTime = 5.0f;
    [SerializeField, Range(0.2f, 0.8f)] float SlowMotionScale = 0.5f;
    public PlayerMovement script;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        switch(other.gameObject.tag)
        {
            case "DoubleJumpBoost":
            {
                script.SetDoubleJump(true);
                script.SetDoubleJumpCounter(DoubleJumpTimer);
                Destroy(other.gameObject); 
                break;
            }

            case "JumpBoost":
            {
                script.SetJumpPower(script.GetJumpPower() * 2.0f);
                Invoke("ResetJumpBoost", JumpPowerTimer);
                Destroy(other.gameObject); 
                break;
            }

            case "SlowMotion":
            {
                Time.timeScale = SlowMotionScale;
                Invoke("ResetTimeScale", SlowMotionTime);
                Destroy(other.gameObject);
                break;
            }

            case "Ladder":
            {
                script.SetOnLadder(true);
                break;
            }

            default:
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            script.SetOnLadder(false);
        }
    }

    private void ResetJumpBoost()
    {
        script.SetJumpPower(script.GetJumpPower() / 2.0f);
    }

    private void ResetTimeScale()
    {
        Time.timeScale = 1.0f;
    }
}
