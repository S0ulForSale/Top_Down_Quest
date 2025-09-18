using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Utils;

namespace Managers
{
    public class InputManager : MonoSingleton<InputManager>
    {
        public readonly UnityEvent shoot = new UnityEvent();
        public readonly UnityEvent dash = new UnityEvent();
        public Vector2 axis { get; private set; }
        public bool firing { get; private set; }
        public Vector2 nonZeroAxis { get; private set; } = new(1, 0);
        
        public void SetAxis(Vector2 axisInput)
        { 
            axis = axisInput;
            if (axis != Vector2.zero)
                nonZeroAxis = axisInput;
        }

        public void SetShooting(bool shooting)
        {
            firing = shooting;
        }

        public void Shoot()
        {
            shoot.Invoke();
        }

        public void Dash()
        {
            dash.Invoke();
        }
    }
}