using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Portal : MonoBehaviour
{

    enum DestinationIdentifier
    {
        A, B, C, D, E
    }

    [SerializeField] int sceneToLoad = -1;
    [SerializeField] Transform spawnPoint;
    [SerializeField] DestinationIdentifier destination;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(Transition());
        }
    }

    private IEnumerator Transition()
    {
        if(sceneToLoad < 0)
        {
            Debug.LogError("sceneToLoad NOT set!");
            yield break;
        }

        DontDestroyOnLoad(gameObject);
        yield return SceneManager.LoadSceneAsync(sceneToLoad);
        //Debug.Log("Scene Loaded by: " + SceneManager.GetActiveScene().name);
        Portal otherPortal = GetOtherPortal();
        UpdatePlayer(otherPortal);
        Destroy(gameObject);
    }

    private Portal GetOtherPortal()
    {
        foreach (Portal portal in FindObjectsOfType<Portal>())
        {
            Debug.Log("otherPortal located");
            if (portal == this) continue;
            if (portal.destination != destination) continue;
            return portal;
        }

        return null;
    }

    private void UpdatePlayer(Portal otherPortal)
    {
        //had to do 'navmeshagent fix' because player was not spawing in correct place when going 2B->1B

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        //player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);  //then delete player.transform.position = otherPortal.spawnPoint.position;
        player.GetComponent<NavMeshAgent>().enabled = false;

        player.transform.position = otherPortal.spawnPoint.position;
        player.transform.rotation = otherPortal.spawnPoint.rotation;

        player.GetComponent<NavMeshAgent>().enabled = true;
    }
}
