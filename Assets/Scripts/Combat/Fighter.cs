using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{

    public class Fighter : MonoBehaviour, IAction
    {

        Transform target;
        [SerializeField] float weaponRange = 2f;

        private void Update()
        {

            if (target == null) return;

            if (target != null && !GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehavior();

            }
        }

        private void AttackBehavior()
        {
            GetComponent<Animator>().SetTrigger("attack");
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
            Debug.Log("Player attacking: "+target.name);            
        }

        public void Cancel()
        {
            target = null;
        }

        //Animation Event
        void Hit()
        {
            
        }

    }
}
