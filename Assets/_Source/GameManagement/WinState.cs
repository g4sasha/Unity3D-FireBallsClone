using Core;
using UnityEngine;

namespace GameManagement
{
    public class WinState : GameState
    {
        public override void Enter()
        {
            Debug.Log("Победа!");
            Time.timeScale = 0f;
        }

        public override void Exit() { }
    }
}
