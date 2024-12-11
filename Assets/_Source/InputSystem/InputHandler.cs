using System;
using UnityEngine;

namespace InputSystem
{
    public abstract class InputHandler : MonoBehaviour
    {
        public abstract event Action OnFire;
    }
}