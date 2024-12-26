using Core;
using UnityEngine;

namespace GameManagement
{
    public class WinState : GameState
    {
        public override void Enter()
        {
            Debug.Log("Победа!");
        }

        public override void Exit() { }
    }
}
