using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerCollider : MonoBehaviour
{
    [SerializeField] float DoubleJumpTimer = 0.0f;
    [SerializeField] float JumpPowerTimer = 0.0f;
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
                break;
            }

            case "JumpBoost":
            {
                script.SetJumpPower(script.GetJumpPower() * 2.0f);
                Invoke("ResetJumpBoost", JumpPowerTimer);
                break;
            }

            default:
                break;
        }
    
        Destroy(other.gameObject);
    }

    private void ResetJumpBoost()
    {
        script.SetJumpPower(script.GetJumpPower() / 2.0f);
    }
}
