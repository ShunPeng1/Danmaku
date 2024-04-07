using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Priority_Queue;
using Shun_Utilities;
using UnityEngine;


namespace _Scripts.Simulation
{
    public class SimulationManager : SingletonMonoBehaviour<SimulationManager>
    {
        private SimplePriorityQueue<SimulationPackage> _simulationQueue = new();


        private bool _isExecuting;

        public void AddSimulationPackage(SimulationPackage simulationPackage)
        {
            if (simulationPackage == null || _simulationQueue.Contains(simulationPackage))
            {
                return;
            }
            _simulationQueue.Enqueue(simulationPackage, simulationPackage.Priority);
        }

        public void RemoveCoroutineSimulationObject(SimulationPackage simulationPackage)
        {
            if (simulationPackage == null || !_simulationQueue.Contains(simulationPackage))
            {
                return;
            }
            _simulationQueue.Remove(simulationPackage);
        }

        public void ClearAll()
        {
            _simulationQueue.Clear();
        }

        private void FixedUpdate()
        {
            StartCoroutine(ExecuteAll());
        }
        

        IEnumerator ExecuteAll()
        {
            if (_isExecuting)
            {
                yield break;
            }

            _isExecuting = true;
            while (_simulationQueue.Count > 0)
            {
                var simulationObject = _simulationQueue.Dequeue();
                
                if (simulationObject.IsParallel)
                    yield return StartCoroutine(ExecuteCoroutineSimulationParallel(simulationObject));
                else 
                    yield return StartCoroutine(ExecuteCoroutineSimulationConcurrent(simulationObject));
            }

            _isExecuting = false;
        }
        IEnumerator ExecuteCoroutineSimulationConcurrent(SimulationPackage simulationPackage)
        {
            foreach (var enumerator in simulationPackage.ExecuteEvents)
            {
                if (enumerator == null) continue;
                yield return StartCoroutine(enumerator.Invoke());
            }

            yield return null;
            
        }
        IEnumerator ExecuteCoroutineSimulationParallel(SimulationPackage simulationPackage)
        {
            foreach (var enumerator in simulationPackage.ExecuteEvents.Where(enumerator => enumerator != null))
            {
                StartCoroutine(enumerator.Invoke());
            }

            // Wait for all running coroutines to complete
            yield return new WaitUntil(() => simulationPackage.IsComplete);
        }
        
    }
}

