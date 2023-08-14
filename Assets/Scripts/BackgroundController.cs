using System;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject moon;
    public float moonspeedX = 0.99f;
    public float moonspeedY = 0.01f;
    [SerializeField] private float foreGroundSpeedX = 0.001f;
    
    public bool needMove;

    private void Update()
    {
        if (!needMove) return;

        var moveDelta = Time.deltaTime * speed;
        var moveDeltaGround = new Vector3(-moveDelta, 0, 0);
        var moveDeltaMoon = new Vector3(moveDelta * moonspeedX, -(moveDelta * moonspeedY), 0);

        MoveBackground(moveDeltaGround, moveDeltaMoon);
    }

    private void MoveBackground(Vector3 moveDeltaGround, Vector3 moveDeltaMoon)
    {
        transform.Translate(moveDeltaGround);
        moon.transform.Translate(moveDeltaMoon);
    }

    internal void ResetBackground()
    {
        transform.position = Vector3.zero;
    }
}