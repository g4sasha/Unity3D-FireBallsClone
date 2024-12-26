using Core;
using UnityEngine;

namespace GameManagement
{
    public class LoseState : GameState
    {
        public override void Enter()
        {
            Debug.Log("Поражение...");
        }

        public override void Exit() { }
    }
}
