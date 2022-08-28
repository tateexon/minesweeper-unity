using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoardInput : MonoBehaviour
{

    private ClickActions ca;

    private void Awake()
    {
        ca = new ClickActions();
    }

    private void OnEnable()
    {
        ca.Player.LeftClick.performed += LeftClick;
        ca.Player.RightClick.performed += RightClick;
        ca.Player.Enable();
    }

    private void OnDisable()
    {
        ca.Player.LeftClick.performed -= LeftClick;
        ca.Player.RightClick.performed -= RightClick;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Clicked(bool left)
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            if (hit.transform.TryGetComponent<ItemTarget>(out ItemTarget it))
            {
                if (left)
                {
                    it.LeftClicked();
                }
                else
                {
                    it.RightClicked();
                }
            }
        }
    }

    private void LeftClick(InputAction.CallbackContext obj)
    {
        Clicked(true);
    }

    private void RightClick(InputAction.CallbackContext obj)
    {
        Clicked(false);
    }
}
