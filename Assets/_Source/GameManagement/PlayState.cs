using Core;
using UnityEngine;

namespace GameManagement
{
    public class PlayState : GameState
    {
        public override void Enter()
        {
            Debug.Log("Игра запущена!");
        }

        public override void Exit() { }
    }
}
