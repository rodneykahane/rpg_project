﻿using UnityEngine;

namespace RPG.Combat
{

    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;
        bool isDead = false;

        public void TakeDamage(float damage)
        {
            if (healthPoints > 0)
            {
                healthPoints = Mathf.Max(healthPoints - damage, 0);
                Debug.Log(name + "'s health is: " + healthPoints);
            }
            else if (healthPoints <= 0)
            {
                Die();
                
            }
        }

        private void Die()
        {
            if (isDead) return;
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");            
        }
    }
}