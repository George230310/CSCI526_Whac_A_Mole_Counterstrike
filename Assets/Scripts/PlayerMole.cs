using UnityEngine;
using UnityEngine.UI;

public class PlayerMole : Mole
{
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
}