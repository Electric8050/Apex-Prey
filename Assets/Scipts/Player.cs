using UnityEngine;

public class Player : MonoBehaviour
{
    public float horizontalInput;
    public float forwardInput;
    public float speed;
    public Rigidbody rb;
    public bool isOnGround = true;
    public float walkSpeed = 10f;
    public float sprintSpeed = 15f;
    private float currentSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // update currentSpeed before moving so sprint takes effect immediately
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }

        // use currentSpeed for movement
        transform.Translate(Vector3.right * Time.deltaTime * currentSpeed * horizontalInput);
        transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed * forwardInput);

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            rb.AddForce(Vector3.up * 700);
            isOnGround = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

}
