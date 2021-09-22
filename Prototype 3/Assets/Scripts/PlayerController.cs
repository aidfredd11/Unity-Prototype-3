using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    public float jumpForce;
    public float gravityModifier = 1;

    public bool gameOver = false;
    private bool isGrounded = true;
    private bool waitForStart = true;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSound;
    public AudioClip crashSound;

    private AudioSource playerAudio;
    private Animator playerAnimator;

    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && waitForStart)
        {
            waitForStart = false;
            FindObjectOfType<GameManager>().StartGame();
        }

        else if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true && !gameOver && !waitForStart)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;

            // update the score
            score++;
            FindObjectOfType<GameManager>().UpdateScore(score);
            
            playerAnimator.SetTrigger("Jump_trig");
            dirtParticle.Stop();

            playerAudio.PlayOneShot(jumpSound, 1.0f);
        } 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            FindObjectOfType<GameManager>().EndGame(); // End the game

            explosionParticle.Play();
            dirtParticle.Stop();

            playerAudio.PlayOneShot(crashSound, 1.0f);

            playerAnimator.SetBool("Death_b", true);
            playerAnimator.SetInteger("DeathType_int", 1);

        } 
    }
}
