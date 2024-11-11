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
    private InputAction _interact;
    private InputAction _menuOpen;
    private InputAction _menuClose;

    public bool JumpDown { get; private set; }
    public bool LoadDown { get; private set; }
    public bool JumpReleased { get; private set; }
    public bool ClimbDown { get; private set; }
    public bool DashDown { get; private set; }
    public bool InteractDown { get; private set; }
    public bool MenuOpen { get; private set; }
    public bool MenuClose { get; private set; }

    public Vector2 Move { get; private set; }
    public int MoveX { get; private set; }
    public int MoveY { get; private set; }

    private void Awake()
    {
        if(instance != null) { Destroy(gameObject); return; }
        instance = this;

        _inputPlayer = GetComponent<PlayerInput>();

        _move = _inputPlayer.actions["Move"];
        _jump = _inputPlayer.actions["Jump"];
        _dash = _inputPlayer.actions["Dash"];
        _climb = _inputPlayer.actions["Climb"];
        _load = _inputPlayer.actions["Load"];
        _interact = _inputPlayer.actions["Interact"];
        _menuOpen = _inputPlayer.actions["MenuOpen"];
        _menuClose = _inputPlayer.actions["MenuClose"];

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
        InteractDown = _interact.WasPressedThisFrame();
        MenuOpen = _menuOpen.IsPressed();
        MenuClose = _menuClose.IsPressed();

        Move = _move.ReadValue<Vector2>();
        MoveX = (int)(Move * Vector2.right).normalized.x;
        MoveY = (int)(Move * Vector2.up).normalized.y;
    }

    public static void DeactivatePlayerCtrl()
    {
        _inputPlayer.currentActionMap.Disable();
    }

    public static void ActivatePlayerCtrl()
    {
        _inputPlayer.currentActionMap.Enable();
    }

}
