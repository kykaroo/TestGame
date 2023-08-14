using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private float speed;
    public bool needMove;
    private void Update()
    {
        if (!needMove) return;
        
        MoveBackground();
    }

    private void MoveBackground()
    {
        transform.Translate(new(-(Time.deltaTime * speed),0,0));
    }

    internal void ResetBackground()
    {
        transform.position = Vector3.zero;
    }
}