using System;
using UnityEngine;

namespace InputSystem
{
    public class DesktopInput : InputHandler
    {
        public override event Action OnFire;

        private void Update()
        {
            HandleFire();
        }

        private void HandleFire()
        {
            if (Input.GetButton("Fire1"))
            {
                OnFire?.Invoke();
            }
        }
    }
}
