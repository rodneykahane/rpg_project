using RPG.Combat;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {  
        void Update()
        {
            if (InteractWithCombat()) return;

            if (InertactWithMovement()) return;

            Debug.Log("Nothing to do");
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                
                if (!GetComponent<Fighter>().CanAttack(target))
                {
                    continue;  //if false, go to next item in foreach loop
                }

                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target);
                }

                return true;
            }

            return false;
        }

        private bool InertactWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    //Debug.Log("we have hit");
                    GetComponent<Mover>().StartMoveAction(hit.point);

                }

                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {

            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}