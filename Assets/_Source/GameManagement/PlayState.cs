using Core;
using UnityEngine;

namespace GameManagement
{
    public class PlayState : GameState
    {
        public override void Enter()
        {
            Debug.Log("Игра запущена!");
            Time.timeScale = 1f;
        }

        public override void Exit() { }
    }
}
