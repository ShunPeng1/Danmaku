using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Simulation
{
    public class SimulationPackage
    {
        public float Priority { get; private set; }
        public List<Func<IEnumerator>> ExecuteEvents  { get; private set; }
        public bool IsParallel { get; private set; }
        public bool IsComplete { get; private set; }
        
        private SimulationPackage(float priority, bool isParallel, List<Func<IEnumerator>> executeEvents)
        {
            Priority = priority;
            IsParallel = isParallel;
            ExecuteEvents = executeEvents;
        

            if (!IsParallel) return;
            
            // If the package is parallel, we need to add a begin and end event to the package
            ExecuteEvents.Insert(0,SetBeginParallel);
            ExecuteEvents.Add(SetEndParallel);
        }

        private IEnumerator SetBeginParallel()
        {
            IsComplete = false;
            yield break;
        }
        
        private IEnumerator SetEndParallel()
        {
            IsComplete = true;
            yield break;
        }
        
        public class SimulationPackageBuilder
        {
            private readonly List<Func<IEnumerator>> _executeEvents = new();
            private readonly float _priority;
            private readonly bool _isParallel;

            public SimulationPackageBuilder(float priority, bool isParallel = false)
            {
                _priority = priority;
                _isParallel = isParallel;
            }
            
            public SimulationPackage Build()
            {
                return new SimulationPackage(_priority, _isParallel, _executeEvents);
            }
            
            public void AddToPackage(float waitTime)
            {
                IEnumerator CoroutineWrapper()
                {
                    yield return new WaitForSeconds(waitTime);
                }
            
                _executeEvents.Add(CoroutineWrapper);
            }
        
            public void AddToPackage(Action action)
            {
                _executeEvents.Add(ConvertToIEnumerator(action));
            }

            public void AddToPackage(Func<IEnumerator> coroutine)
            {
                _executeEvents.Add(coroutine);
            }
        
            public void AddToPackage(IEnumerator coroutine)
            {
                IEnumerator CoroutineWrapper() => coroutine;
                _executeEvents.Add(CoroutineWrapper);
            }

            public void AddToPackage(Tween tween)
            {
                _executeEvents.Add(ConvertToIEnumerator(tween));
            }

            
            private Func<IEnumerator> ConvertToIEnumerator(Action action)
            {
                return new Func<IEnumerator>(() =>
                {
                    IEnumerator CoroutineWrapper()
                    {
                        action();
                        yield return null; // Yielding once to make it a valid coroutine
                    }

                    return CoroutineWrapper();
                });
            }

            private Func<IEnumerator> ConvertToIEnumerator(Tween tween)
            {
                if (tween == null || !tween.IsActive()) return null;
            
                tween.Pause();
                return new Func<IEnumerator>(() =>
                {
                    IEnumerator CoroutineWrapper()
                    {
                        tween.SetAutoKill(false);
                        tween.Play();
                        while (!tween.IsComplete())
                        {
                            yield return null;
                        }
                        tween.Kill();
                    }

                    return CoroutineWrapper();
                });
            }
        }
        
        
    }
}