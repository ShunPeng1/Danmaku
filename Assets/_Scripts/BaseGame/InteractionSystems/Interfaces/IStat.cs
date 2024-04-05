using System;

namespace _Scripts.CoreGame.InteractionSystems.Interfaces
{
    public interface IStat<T>
    {
        public T Get();
        public void Set(T value);
        public void Increase(T value);
        public void Decrease(T value);
        
        public void Subscribe(Action<T,T> action);
        public void Unsubscribe(Action<T,T> action);
    }
}