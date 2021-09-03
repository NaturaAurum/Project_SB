using System;
using System.Collections;
using System.Collections.Generic;
using SB.GameLogic.Command;
using UnityEngine;

namespace Stella.GameLogic.Inputs
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField]
        private KeyCode jumpKeyCode = KeyCode.Space;

        private Vector3 prevDirection = Vector3.zero;
        private Vector3 currDirection = Vector3.zero;

        // gc 막기 위한 axis const string 변수
        // update에서 "" 묶어서 string 만들어서 쓰면 gc 발생함
        // 로컬 임시 변수로 만들어져서 할당되고 버려지기 때문
        private const string VERTICAL_AXIS = "Vertical";
        private const string HORIZONTAL_AXIS = "Horizontal";
        
        private void Update()
        {
            if (Input.GetKeyDown(jumpKeyCode))
            {
                CommandDispatcher.Dispatch(new JumpCommand());
            }

            currDirection = Vector3.zero;

            // var vertical = Input.GetAxis(VERTICAL_AXIS);
            var horizontal = Input.GetAxis(HORIZONTAL_AXIS);

            currDirection.x = horizontal;
            // currDirection.y = vertical;

            if (prevDirection != currDirection)
            {
                CommandDispatcher.Dispatch(new MoveCommand(currDirection));
            }
            
            prevDirection = currDirection;
        }
    }
}
