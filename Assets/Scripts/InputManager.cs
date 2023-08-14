using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public event Action OnMouseButtonClicked;
    public event Action OnEscapeButtonClicked;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnMouseButtonClicked?.Invoke();

        if (Input.GetKeyDown(KeyCode.Escape))
            OnEscapeButtonClicked?.Invoke();
    }
}
