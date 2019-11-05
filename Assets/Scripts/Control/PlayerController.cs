using RPG.Combat;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

        Ray lastRay;  //raytracing debug

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
                if (target == null) continue;

                if (Input.GetMouseButtonDown(0))
                {
                   // lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    GetComponent<Fighter>().Attack(target);
                }
               // Debug.DrawRay(lastRay.origin, lastRay.direction * 100, Color.blue);
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
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("we have hit");
                  //  lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    GetComponent<Mover>().MoveTo(hit.point);

                }
                //Debug.DrawRay(lastRay.origin, lastRay.direction * 100, Color.blue);
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            //debug raycasting
            /*
            lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(lastRay.origin, lastRay.direction * 100, Color.blue);
            return lastRay;
            */
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}