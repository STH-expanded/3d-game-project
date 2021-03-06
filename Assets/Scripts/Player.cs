using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Rigidbody playerBody;

    private bool isGrounded = true;

    [SerializeField]
    private int playerSpeed = 1;
    private int maxSpeed = 6;
    
    private Vector3 accelerationVector = Vector3.one;
    private Vector3 inputVector;

    [SerializeField]
    private String horizontalAxis;

    [SerializeField]
    private String verticalAxis;

    private bool hasGameStarted = false;
    
    [SerializeField]
    public Vector3 startPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody component of Player
        playerBody = GetComponent<Rigidbody>();
        
        // Save the spawning position of Player
        startPosition = transform.position;
    }


    //Refresh at each frame
    void Update()
    {
        
        StartCoroutine(SecondsBeforeStartingGame(3));

        if (hasGameStarted)
        {
            // Get Inputs
            inputVector = new Vector3(Input.GetAxis(horizontalAxis) * playerSpeed, 0, Input.GetAxis(verticalAxis) * playerSpeed);

            // Face the cube to the looking direction
            transform.LookAt(transform.position + new Vector3(inputVector.x, 0, inputVector.z));
                        
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "mesh_map_placeholder") isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "mesh_map_placeholder") isGrounded = false;
    }

    // Refresh at 60 fps
    void FixedUpdate() {
        if (inputVector != Vector3.zero) {
            // if (Math.Abs(playerBody.velocity.x) >= maxSpeed) inputVector.x = 0;
            // if (Math.Abs(playerBody.velocity.z) >= maxSpeed) inputVector.z = 0;

            if(playerBody.velocity.magnitude < maxSpeed)
            {
                playerBody.AddForce(new Vector3(inputVector.x, 0, inputVector.z), ForceMode.VelocityChange);
            } 
        }       

        
    }
    
    IEnumerator SecondsBeforeStartingGame(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        hasGameStarted = true;
    }
}
