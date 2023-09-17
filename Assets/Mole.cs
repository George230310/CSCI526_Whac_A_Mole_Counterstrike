using UnityEngine;

public class Mole : MonoBehaviour
{
    private Vector3 initialPosition; // Initial position of the mole
    public float speed = 1.0f;       // Speed of the mole's rising movement
    public float fallSpeed = 2.0f;   // Speed of the mole's falling movement
    public float riseHeight = 1.0f;  // The height the mole rises to from its initial position
    private bool isRising = true;    // Indicates if the mole is rising
    private bool isAtPeak = false;   // Indicates if the mole has reached its peak height
    private bool isHit = false;      // Indicates if the mole has been hit

    void Start()
    {
        initialPosition = transform.position;  // Save the initial position
        Rise();   // Start the mole rising action
    }

    void Update()
    {
        // If the mole is rising and hasn't reached the riseHeight
        if (isRising && transform.position.y < initialPosition.y + riseHeight)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime); // Move the mole upwards
            // Check if the mole has reached its peak
            if (transform.position.y >= initialPosition.y + riseHeight)
            {
                isRising = false;  // Stop the mole from rising
                isAtPeak = true;   // Mark that the mole has reached its peak
                Invoke("Fall", 1.0f);  // Wait for 1 second before the mole starts to fall
            }
        }
        // If the mole is not rising, hasn't been hit and is above its initial position
        else if (!isRising && !isAtPeak && transform.position.y > initialPosition.y)
        {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime); // Move the mole downwards using fallSpeed
        }
    }

    // When the mole is clicked
    void OnMouseDown()
    {
        isHit = true;  // Mark that the mole has been hit
        Fall();        // Make the mole fall
    }

    // Function to start the mole's rising action
    public void Rise()
    {
        isRising = true;
    }

    // Function to make the mole fall
    public void Fall()
    {
        if (isAtPeak)
        {
            isAtPeak = false;  // Reset the peak flag
        }
        
        isRising = false;
        // If the mole has been hit, cancel any pending Invokes
        if (isHit)
        {
            CancelInvoke();
        }
        
        // Schedule a rise at a random time within the next 5 seconds
        float randomRiseTime = Random.Range(0f, 5f);
        Invoke("Rise", randomRiseTime);
    }
    
    // Detect collisions with other objects (like a hammer)
    public void OnTriggerEnter(Collider other)
    {
        // If the object that hit the mole has a tag "Hammer"
        if (other.gameObject.CompareTag("Hammer"))
        {
            isHit = true;  // Mark that the mole has been hit
            Fall();        // Make the mole fall
        }
    }
}
