using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCtrl : MonoBehaviour
{
    public static PlayerCtrl instance;

    public static PlayerInput _inputPlayer;
    private InputAction _move;
    private InputAction _jump;
    private InputAction _dash;
    private InputAction _climb;

    public static bool JumpDown;
    public static bool JumpHeld;
    public static bool JumpReleased;
    public static bool ClimbDown;
    public static bool DashDown;
    public static Vector2 Move;

    private void Awake()
    {
        PlayerCtrl.instance = this;

        _inputPlayer = GetComponent<PlayerInput>();

        _move = _inputPlayer.actions["Move"];
        _jump = _inputPlayer.actions["Jump"];
        _dash = _inputPlayer.actions["Dash"];
        _climb = _inputPlayer.actions["Climb"];

    }

    void Update()
    {
        this.GatherInput();

    }
    protected virtual void GatherInput()
    {

        JumpDown = _jump.WasPressedThisFrame();
        JumpHeld = _jump.IsPressed();
        JumpReleased = _jump.WasReleasedThisFrame();
        ClimbDown = _climb.IsPressed();
        DashDown = _dash.IsPressed();
        Move = _move.ReadValue<Vector2>();

    }


}
