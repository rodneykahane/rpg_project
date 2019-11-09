using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        

        private void Update()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            DistanceToPlayer(player);
        }

        private void DistanceToPlayer(GameObject player)
        {
            if (Vector3.Distance(this.transform.position, player.transform.position) < chaseDistance)
            {
                Debug.Log(name + " is chasing the Player!");
            }
        }
    }
}