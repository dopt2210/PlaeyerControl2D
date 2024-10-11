using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCtrl : MonoBehaviour
{
    public static PlayerCtrl instance {  get; private set; }    
    public static PlayerInput _inputPlayer { get; private set; }

    private InputAction _move;
    private InputAction _jump;
    private InputAction _dash;
    private InputAction _climb;
    private InputAction _load;

    public bool JumpDown { get; private set; }
    public bool LoadDown { get; private set; }
    public bool JumpReleased { get; private set; }
    public bool ClimbDown { get; private set; }
    public bool DashDown { get; private set; }
    public Vector2 Move { get; private set; }

    private void Awake()
    {
        PlayerCtrl.instance = this;

        _inputPlayer = GetComponent<PlayerInput>();

        _move = _inputPlayer.actions["Move"];
        _jump = _inputPlayer.actions["Jump"];
        _dash = _inputPlayer.actions["Dash"];
        _climb = _inputPlayer.actions["Climb"];
        _load = _inputPlayer.actions["Load"];

    }

    void Update()
    {
        this.GatherInput();

    }
    protected virtual void GatherInput()
    {

        JumpDown = _jump.WasPressedThisFrame();
        JumpReleased = _jump.WasReleasedThisFrame();
        LoadDown = _load.IsPressed();
        ClimbDown = _climb.IsPressed();
        DashDown = _dash.IsPressed();
        Move = _move.ReadValue<Vector2>();

    }


}
