using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoardInput : MonoBehaviour
{

    private Camera _camera;

    private ClickActions ca;

    private void Awake()
    {
        ca = new ClickActions();
    }

    private void OnEnable()
    {
        ca.ClickAction.LeftClick.performed += LeftClick;
        ca.ClickAction.RightClick.performed += RightClick;
        ca.ClickAction.Enable();
    }
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    private void LeftClick(InputAction.CallbackContext obj)
    {
        Ray ray = new Ray(_camera.transform.position, Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            if (hit.transform.TryGetComponent<ItemTarget>(out ItemTarget it))
            {
                it.LeftClicked();
            }
        }
    }

    private void RightClick(InputAction.CallbackContext obj)
    {
        Ray ray = new Ray(_camera.transform.position, Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            if (hit.transform.TryGetComponent<ItemTarget>(out ItemTarget it))
            {
                it.RightClicked();
            }
        }
    }
}
