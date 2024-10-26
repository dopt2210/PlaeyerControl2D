using UnityEngine;

[CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
public class InputCtrlPlayer: InputCtrl
{
    public override Vector2 _MoveInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    public override bool _JumpInput()
    {
        return Input.GetButtonDown("Jump");
    }

    public override bool _JumpHeldInput()
    {
        return Input.GetButton("Jump");
    }

    public override bool _ClimbInput()
    {
        return Input.GetKey(KeyCode.C);
    }

    public override bool _DashInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }
}
