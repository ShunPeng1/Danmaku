using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Positions
{
    public class PlayerStandingPositionMap : MonoBehaviour
    {
        // South, West, North, East == 0, 1, 2, 3
        [SerializeField] private List<Transform> _middlePositions;
        [SerializeField] private List<Transform> _leftPositions;
        [SerializeField] private List<Transform> _rightPositions;
        
        public Transform GetPlayerPosition(int totalPlayer, int index, int offset = 0)
        {
            if (index < 0 || index >= totalPlayer)
            {
                Debug.LogError("Invalid index");
                return null;
            }
            
            index = (index - offset + totalPlayer) % totalPlayer;
            
            switch (totalPlayer)
            {
                case 4:
                    return _middlePositions[index];
                case 5: // South Middle, West Middle, North Left, North Right, East Middle
                    var positions5 = new Dictionary<int, Transform>
                    {
                        {0, _middlePositions[0]},
                        {1, _middlePositions[1]},
                        {2, _leftPositions[2]},
                        {3, _rightPositions[2]},
                        {4, _middlePositions[3]}
                    };
                    return positions5[index];

                case 6: // South Middle, West Left, West Right, North Left, North Right, East Middle
                    var positions6 = new Dictionary<int, Transform>
                    {
                        {0, _middlePositions[0]},
                        {1, _leftPositions[1]},
                        {2, _rightPositions[1]},
                        {3, _leftPositions[2]},
                        {4, _rightPositions[2]},
                        {5, _middlePositions[3]}
                    };
                    return positions6[index];
                case 7: // South Middle, West Left, West Right, North Left, North Right, East Left, East Right
                    var positions7 = new Dictionary<int, Transform>
                    {
                        {0, _middlePositions[0]},
                        {1, _leftPositions[1]},
                        {2, _rightPositions[1]},
                        {3, _leftPositions[2]},
                        {4, _rightPositions[2]},
                        {5, _leftPositions[3]},
                        {6, _rightPositions[3]}
                    };
                    return positions7[index];
                case 8: // South Left, South Right, West Left, West Right, North Left, North Right, East Left, East Right
                    var positions8 = new Dictionary<int, Transform>
                    {
                        {0, _leftPositions[0]},
                        {1, _rightPositions[0]},
                        {2, _leftPositions[1]},
                        {3, _rightPositions[1]},
                        {4, _leftPositions[2]},
                        {5, _rightPositions[2]},
                        {6, _leftPositions[3]},
                        {7, _rightPositions[3]}
                    };
                    return positions8[index];
            }
            
            Debug.LogError("Invalid total player");
            return null;
        }
    }
}