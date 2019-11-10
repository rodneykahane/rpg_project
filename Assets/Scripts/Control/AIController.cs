﻿using RPG.Combat;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        Fighter fighter;
        GameObject player;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {            
            if(InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                fighter.Attack(player);
            }
            else
            {
                fighter.Cancel();
            }
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, this.transform.position);
            return distanceToPlayer < chaseDistance;
        }
    }
}