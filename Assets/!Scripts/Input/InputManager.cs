using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// This script acts as a single point for all other scripts to get
// the current input from. It uses Unity's new Input System and
// functions should be mapped to their corresponding controls
// using a PlayerInput component with Unity Events.

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    private bool submitPressed = false;
    private bool menuPressed = false;
    private bool skipPressed = false;

    private static InputManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Input Manager in the scene.");
        }
        instance = this;
    }

    public static InputManager GetInstance() 
    {
        return instance;
    }

    public void SubmitPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            submitPressed = true;
        }
        else if (context.canceled)
        {
            submitPressed = false;
        } 
    }

    public void MenuPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            menuPressed = true;
        }
        else if (context.canceled)
        {
            menuPressed = false;
        }
    }

    public void SkipPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            skipPressed = true;
        }
        else if (context.canceled)
        {
            skipPressed = false;
        }
    }

    // for any of the below 'Get' methods, if we're getting it then we're also using it,
    // which means we should set it to false so that it can't be used again until actually
    // pressed again.

    public bool GetSubmitPressed() 
    {
        bool result = submitPressed;
        submitPressed = false;
        return result;
    }

    public void RegisterSubmitPressed() 
    {
        submitPressed = false;
    }

    public bool GetMenuPressed()
    {
        bool result = menuPressed;
        menuPressed = false;
        return result;
    }

    public void RegisterMenuPressed()
    {
        menuPressed = false;
    }

    public bool GetSkipPressed()
    {
        bool result = skipPressed;
        skipPressed = false;
        return result;
    }

    public void RegisterSkipPressed()
    {
        skipPressed = false;
    }
}
