using UnityEngine;
using UnityEngine.UI;

public class PlayerMole : MonoBehaviour
{
    public float speed = 2.0f;
    public float fallSpeed = 2.0f;
    public float riseHeight = 1.0f;
    
    private Vector3 initialPosition;
    private bool isRising = false;
    private bool isAtPeak = false;
    private bool isHit = false;
    
    private float _upDuration = 1.0f;
    
    // materials for switching mole color after getting hit
    [SerializeField] private Material defaultMat;
    [SerializeField] private Material hitMat;
    private Renderer _myRenderer;

    private void Start()
    {
        initialPosition = transform.position;
        _myRenderer = GetComponent<Renderer>();
        _myRenderer.material = defaultMat;
    }

    private void Update()
    {
        if (Input.GetAxis("Vertical") > 0.0f)
        {
            StartRising();
        }
        
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
                Invoke(nameof(Fall), _upDuration);
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
    }

    private void FallImmediately()
    {
        isAtPeak = false;
        isRising = false;
        CancelInvoke(nameof(Fall));
        CancelInvoke(nameof(StartRising));
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