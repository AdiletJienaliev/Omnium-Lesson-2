using System;
using System.Collections;
using UnityEngine;

    public class PlayerAttack : MonoBehaviour
    {
        public ICreatorUnit unitCreator;
        public float secondPerAttack = 1;

        private void Awake()
        {
            StartCoroutine(Attack());
        }

        private IEnumerator Attack()
        {
            while (true)
            {
                yield return new WaitForSeconds(secondPerAttack);
                IUnit unit =  unitCreator.GetUnit();
                unit.SetDamage(10);
            }
        }
    }
