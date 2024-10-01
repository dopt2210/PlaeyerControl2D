using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public InputField _input;
    void Update()
    {
        GatherInput();

    }
    void GatherInput()
    {
        _input = new InputField
        {
            JumpDown = Input.GetButtonDown("Jump"),
            JumpHeld = Input.GetButton("Jump"),
            ClimbDown = Input.GetKey(KeyCode.R),
            DashDown = Input.GetKeyDown(KeyCode.E),
            Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")),
        };
    }

    public struct InputField
    {
        public bool JumpDown;
        public bool JumpRelease;
        public bool JumpHeld;
        public bool ClimbDown;
        public bool DashDown;
        public Vector2 Move;
    }
}
