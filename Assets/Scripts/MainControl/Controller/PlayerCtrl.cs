using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] private InputCtrlPlayer _inputPlayer;
    public InputField _input;

    void Update()
    {
        GatherInput();

    }
    private void GatherInput()
    {
        _input = new InputField
        {
            JumpDown = _inputPlayer._JumpInput(),
            JumpHeld = _inputPlayer._JumpHeldInput(),
            ClimbDown = _inputPlayer._ClimbInput(),
            DashDown = _inputPlayer._DashInput(),
            Move = _inputPlayer._MoveInput(),
        };
    }

    public struct InputField
    {
        public bool JumpDown;
        public bool JumpHeld;
        public bool ClimbDown;
        public bool DashDown;
        public Vector2 Move;
    }
}
