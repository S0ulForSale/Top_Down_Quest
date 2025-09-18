using System;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class TouchController : MonoBehaviour
    {
        [SerializeField] private Button shootButton;
        [SerializeField] private PressRelease firing;
        [SerializeField] private Button dashButton;
        [SerializeField] private FixedJoystick joystick;
        private InputManager input;


        private void Start()
        {
            input = InputManager.instance;
            shootButton.onClick.AddListener(input.Shoot);
            dashButton.onClick.AddListener(input.Dash);
            firing.press.AddListener(input.SetShooting);
        }

        private void Update()
        {
            input.SetAxis(joystick.Direction);
        }
    }
}