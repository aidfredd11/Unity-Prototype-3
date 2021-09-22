using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float currentSpeed;
    public float maxSpeed = 1000;
    public float acceleration = 1;

    public float leftBound;

    private PlayerController playerControllerScript;
    private MoveLeft bgMoveLeft;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        if (gameObject.CompareTag("Obstacle"))
        {
            // speed of bg at this moment in time
            bgMoveLeft = GameObject.Find("Background").GetComponent<MoveLeft>();
            float speed = bgMoveLeft.currentSpeed;
         
            currentSpeed = Mathf.MoveTowards(speed, maxSpeed, acceleration * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == false)
        {

            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.deltaTime);
            transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);

        }
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
