using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    public ParticleSystem dirtParticle;
    public ParticleSystem explosionParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public float jumpForce = 500.0f;
    public float gravityModifier = 1.5f;
    public bool isOnGround = true;
    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the GameObject's Rigibody component to playerRb and playerAnim
        playerRb = GetComponent<Rigidbody>();   
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        // Modifying the Game's gravity with custom values
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        // Make the player jump when Spacebar is pressed
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("Jump_trig");
            isOnGround = false;
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 0.7f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Invoking gameover when player hits an obstacle
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        } else if(collision.gameObject.CompareTag("Obstacle"))
        {
            // Play death animation upon hitting obstacle
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            // Play explosion effect, crash audio and stop dirt effect
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            Debug.Log("Game Over!");
            gameOver = true;
        }
    }
}
