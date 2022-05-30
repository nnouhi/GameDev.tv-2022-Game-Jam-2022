using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject followingObject;
    [SerializeField] private float range = 4.0f;
    [SerializeField] private float offset = 4.0f;
    [SerializeField] private AudioClip theme;
    [SerializeField] private AudioClip intro;
    [SerializeField] private AudioClip ending;
    [SerializeField] private MainMenu menu;
    private AudioSource musicPlayer;
    private bool gameStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
        musicPlayer.clip = intro;
        musicPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!menu.InMenu() && !gameStarted)
        {
            gameStarted = true;
            musicPlayer.Stop();
            musicPlayer.clip = theme;
            musicPlayer.Play();
        }

        if (followingObject)
        {
            float objectY = followingObject.transform.position.y;
            if (Mathf.Abs(objectY - this.transform.position.y) > range)
            {
                this.transform.position = new Vector3(this.transform.position.x, objectY + offset, this.transform.position.z);
            }
        }
    }

    public void EndGame()
    {
        musicPlayer.Stop();
        musicPlayer.clip = ending;
        musicPlayer.Play();
    }
}
