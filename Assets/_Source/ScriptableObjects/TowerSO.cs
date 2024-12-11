using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewTower", menuName = "Tower/New")]
    public class TowerSO : ScriptableObject
    {
        [field: SerializeField] public int Size;
        [field: SerializeField] public List<GameObject> Parts;
    }
}
