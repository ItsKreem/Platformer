using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    public AudioSource JumpAudio;
    protected override void HandleInput()
    {
        _inputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetButton("Jump"))
        {
            DoJump();
            _isJumping = true;
            if (JumpAudio != null)
            {
                GameObject.Instantiate(JumpAudio, transform.position, Quaternion.identity);
            }
        }
        else
        {
            _isJumping = false;
        }
    }
}
