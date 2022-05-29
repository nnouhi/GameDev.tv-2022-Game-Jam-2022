using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerCollider : MonoBehaviour
{
    [SerializeField] float DoubleJumpTimer = 0.0f;
    [SerializeField] float JumpPowerTimer = 0.0f;
    [SerializeField] float SlowMotionTime = 5.0f;
    [SerializeField, Range(0.2f, 0.8f)] float SlowMotionScale = 0.5f;

    [SerializeField] private GameObject DoubleJumpImage;
    [SerializeField] private GameObject LongJumpImage;
    [SerializeField] private GameObject SlowMotionImage;

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
                other.GetComponent<PowerUp>()?.Collect();
                DisplayDoubleJump();
                break;
            }

            case "JumpBoost":
            {
                script.SetJumpPower(script.GetJumpPower() * 2.0f);
                Invoke("ResetJumpBoost", JumpPowerTimer);
                other.GetComponent<PowerUp>()?.Collect();
                DisplayLongJump();
                break;
            }

            case "SlowMotion":
            {
                Time.timeScale = SlowMotionScale;
                Invoke("ResetTimeScale", SlowMotionTime);
                other.GetComponent<PowerUp>()?.Collect();
                DisplaySlowMotion();
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

    private void DisplayDoubleJump()
    {
        DoubleJumpImage.SetActive(true);
        Invoke("HideDoubleJump", DoubleJumpTimer);
    }

    private void HideDoubleJump()
    {
        DoubleJumpImage.SetActive(false);
    }

    private void DisplayLongJump()
    {
        LongJumpImage.SetActive(true);
        Invoke("HideLongJump", JumpPowerTimer);
    }

    private void HideLongJump()
    {
        LongJumpImage.SetActive(false);
    }

    private void DisplaySlowMotion()
    {
        SlowMotionImage.SetActive(true);
        Invoke("HideSlowMotion", SlowMotionTime);
    }

    private void HideSlowMotion()
    {
        SlowMotionImage.SetActive(false);
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
