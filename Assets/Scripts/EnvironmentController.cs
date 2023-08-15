using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject moon;
    [SerializeField] private GameObject backGround;
    [SerializeField] private GameObject foreGround;
    
    private Vector3 environmentStartPosition;
    private Vector3 moonStartPosition;
    private Vector3 backGroundStartPosition;
    private Vector3 foreGroundStartPosition;
    
    public float moonSpeedX = 0.99f;
    public float moonSpeedY = 0.01f;
    public float foreGroundSpeedX = 0.99f;
    public float backGroundSpeedX = 0.99f;
    
    public bool needMove;

    private void Awake()
    {
        environmentStartPosition = transform.position;
        moonStartPosition = moon.transform.position;
        backGroundStartPosition = backGround.transform.position;
        foreGroundStartPosition = foreGround.transform.position;
    }

    private void Update()
    {
        if (!needMove) return;

        var moveDelta = Time.deltaTime * speed;
        var environmentMoveDelta = new Vector3(-moveDelta, 0, 0);
        var moonMoveDelta = new Vector3(moveDelta * (1 - moonSpeedX), -(moveDelta * moonSpeedY), 0);
        var foreGroundMoveDelta = new Vector3(-(moveDelta * foreGroundSpeedX), 0, 0);
        var backGroundMoveDelta = new Vector3(moveDelta * (1 - backGroundSpeedX), 0, 0);

        MoveBackground(environmentMoveDelta, moonMoveDelta, foreGroundMoveDelta, backGroundMoveDelta);
    }

    private void MoveBackground(Vector3 environmentMoveDelta, Vector3 moonMoveDelta, Vector3 foreGroundMoveDelta, Vector3 backGroundMoveDelta)
    {
        transform.Translate(environmentMoveDelta);
        moon.transform.Translate(moonMoveDelta);
        foreGround.transform.Translate(foreGroundMoveDelta);
        backGround.transform.Translate(backGroundMoveDelta);
    }

    internal void ResetBackground()
    {
        transform.position = environmentStartPosition;
        moon.transform.position = moonStartPosition;
        backGround.transform.position = backGroundStartPosition;
        foreGround.transform.position = foreGroundStartPosition;
    }
}