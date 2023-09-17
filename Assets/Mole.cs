using UnityEngine;

public class Mole : MonoBehaviour
{
    private Vector3 initialPosition;
    public float speed = 2.0f;
    public float fallSpeed = 2.0f;
    public float riseHeight = 1.0f;
    private bool isRising = false;
    private bool isAtPeak = false;
    private bool isHit = false;

    void Start()
    {
        initialPosition = transform.position;
        TriggerRiseAfterRandomDelay();
    }

    void Update()
    {
        if (isRising && !isAtPeak)
        {
            float currentHeight = transform.position.y - initialPosition.y;
            if (currentHeight < riseHeight)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
            else
            {
                isRising = false;
                isAtPeak = true;
                CancelInvoke("Fall");
                Invoke("Fall", 1.0f);
            }
        }
        else if (!isRising && !isAtPeak && transform.position.y > initialPosition.y)
        {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        }
    }

    void OnMouseDown()
    {
        isHit = true;
        FallImmediately();
    }

    private void StartRising()
    {
        isHit = false;
        isRising = true;
        isAtPeak = false;
    }

    private void Fall()
    {
        isAtPeak = false;
        isRising = false;

        TriggerRiseAfterRandomDelay();
    }

    private void FallImmediately()
    {
        isAtPeak = false;
        isRising = false;
        CancelInvoke("Fall");
        CancelInvoke("StartRising");
        TriggerRiseAfterRandomDelay();
    }

    private void TriggerRiseAfterRandomDelay()
    {
        float randomRiseDelay = Random.Range(1f, 5f);
        Invoke("StartRising", randomRiseDelay);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hammer"))
        {
            isHit = true;
            FallImmediately();
        }
    }
}