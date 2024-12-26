using Core;
using UnityEngine;

namespace GameManagement
{
    public class LoseState : GameState
    {
        public override void Enter()
        {
            Debug.Log("Поражение...");
            Time.timeScale = 0f;
        }

        public override void Exit() { }
    }
}
