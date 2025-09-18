using System;
using UnityEngine;

namespace Managers
{
    public class TestController : MonoBehaviour
    {
#if UNITY_EDITOR
        private InputManager input;
        private void Start()
        {
            input = InputManager.instance;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                input.Dash();
            if(Input.GetKeyDown(KeyCode.C))
                input.Shoot();
            input.SetShooting(Input.GetKey(KeyCode.C));
            input.SetAxis(new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")));
        }
#endif
    }
}