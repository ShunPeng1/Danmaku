namespace _Scripts.CoreGame.InteractionSystems.Interfaces
{
    public interface IDanmakuGameSession
    {
        void Awake();
        void Start();

        void Update(float deltaTime);
        bool CanTransitionToNextSession();
        void TransitionToNextSession();
    }
}