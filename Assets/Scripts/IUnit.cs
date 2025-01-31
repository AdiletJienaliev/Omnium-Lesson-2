    using UnityEngine;

    public interface IUnit
    {
        public int MaxHealth { get; set; }
        public int listIndex { get; set; }
        
        public void Initialize(Transform target);
        public Vector3 GetPosition();
        public void SetDamage(int value);
    }
