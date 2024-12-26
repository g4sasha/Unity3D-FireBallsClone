namespace Core
{
    public abstract class GameState
    {
        public virtual void Enter() { }

        public virtual void Exit() { }
    }
}
