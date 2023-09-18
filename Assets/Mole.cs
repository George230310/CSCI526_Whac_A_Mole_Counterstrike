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
    
    // materials for switching mole color after getting hit
    [SerializeField] private Material defaultMat;
    [SerializeField] private Material hitMat;
    private Renderer _myRenderer;

    private void Start()
    {
        initialPosition = transform.position;
        TriggerRiseAfterRandomDelay();
        _myRenderer = GetComponent<Renderer>();
        _myRenderer.material = defaultMat;
    }

    private void Update()
    {
        if (isRising && !isAtPeak)
        {
            float currentHeight = transform.position.y - initialPosition.y;
            if (currentHeight < riseHeight)
            {
                transform.Translate(Vector3.up * (speed * Time.deltaTime));
            }
            else
            {
                isRising = false;
                isAtPeak = true;
                CancelInvoke(nameof(Fall));
                Invoke(nameof(Fall), 3.0f);
            }
        }
        else if (!isRising && !isAtPeak && transform.position.y > initialPosition.y)
        {
            transform.Translate(Vector3.down * (fallSpeed * Time.deltaTime));
        }
    }

    private void StartRising()
    {
        _myRenderer.material = defaultMat;
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
        CancelInvoke(nameof(Fall));
        CancelInvoke(nameof(StartRising));
        TriggerRiseAfterRandomDelay();
    }

    private void TriggerRiseAfterRandomDelay()
    {
        float randomRiseDelay = Random.Range(1f, 5f);
        Invoke(nameof(StartRising), randomRiseDelay);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hammer"))
        {
            _myRenderer.material = hitMat;
            Debug.Log("Mole hit!");
            isHit = true;
            FallImmediately();
        }
    }
}