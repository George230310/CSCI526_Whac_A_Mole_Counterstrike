using UnityEngine;
using UnityEngine.UI;

public class PlayerMole : Mole
{
    private float timeAtPeak = 0f;
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
        
        //score
        if (isAtPeak && !isHit)
        {
            timeAtPeak += Time.deltaTime;
            if (timeAtPeak >= 1f) // 如果鼹鼠已在顶峰持续一秒钟
            {
                ScoreManager.instance.AddScore(1); // 分数增加1点
                timeAtPeak -= 1f; // 减去一秒，以追踪下一秒
            }
        }
        else
        {
            timeAtPeak = 0f; // 如果鼹鼠不在顶峰或被击中，则重置计数器
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