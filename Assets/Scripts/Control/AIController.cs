using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 3f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float waypointDwellTime = 5f;


        Fighter fighter;
        Health health;
        GameObject player;
        Mover mover;

        Vector3 guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;        
        float waypointWaitTime = Mathf.Infinity;

        int currentWaypointIndex = 0; //patrol node counter

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            player = GameObject.FindGameObjectWithTag("Player");

            guardPosition = this.transform.position;
        }

        private void Update()
        {
            if (health.IsDead()) return;

            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0f;
                AttackBehavior();  //attack player
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehavior();  //lost track of player, wait for a moment
            }
            else
            {
                PatrolBehavior();  //couldnt find player, head back to starting point
            }

            UpdateTimers();
        }

        private void AttackBehavior()
        {
            fighter.Attack(player);
        }

        private void SuspicionBehavior()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void PatrolBehavior()
        {
            Vector3 nextPosition = guardPosition;

            if (patrolPath != null)
            {
                if (AtWayPoint())
                {
                    waypointWaitTime = 0f;
                    CycleWayPoint();
                }
                nextPosition = GetCurrentWaypoint();
            }

            if (waypointWaitTime > waypointDwellTime)
            {

                mover.StartMoveAction(nextPosition);
            }
        }      

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            waypointWaitTime += Time.deltaTime;
        }

        private bool AtWayPoint()
        {
            float distanceToWaypoint = Vector3.Distance(this.transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void CycleWayPoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, this.transform.position);
            return distanceToPlayer < chaseDistance;
        }

        //called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;

            Gizmos.DrawWireSphere(this.transform.position, chaseDistance);
        }
    }
}