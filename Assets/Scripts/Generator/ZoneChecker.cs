using System;
using System.Collections;
using TopDown.Generator;
using TopDown.Movement;
using UnityEngine;

public class ZoneChecker : MonoBehaviour
{
    private float raycastDistance = 1.4f;
    [SerializeField] private float respawnTime = 0.2f;
    private ZoneSwitcher zoneSwitcher;
    private GameObject spawnPlatform;
    private Coroutine spawn;

    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.PLAYER_STAY_CRITICAL_ZONE, OnPlayerStayCriticalZone);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.PLAYER_STAY_CRITICAL_ZONE, OnPlayerStayCriticalZone);
    }

    private void OnPlayerStayCriticalZone()
    {
        spawnPlatform.SetActive(true);
        spawnPlatform.transform.position = zoneSwitcher.GetCurrentZone().transform.position + new Vector3 (0,5,0);

        StartTeleportCoroutine();
    }

    private IEnumerator TeleportPlayerToSpawn()
    {
        yield return new WaitForSeconds(respawnTime);
        transform.position = spawnPlatform.transform.position + Vector3.up * 2;
        gameObject.GetComponent<CharacterController>().enabled = true;
        spawn = null;
    }

    private void StartTeleportCoroutine()
    {
        if(spawn == null)
        {
            spawn = StartCoroutine(TeleportPlayerToSpawn());
        }
    }

    private void Start()
    {
        zoneSwitcher = GameObject.FindGameObjectWithTag("Generator").GetComponent<ZoneSwitcher>();
        spawnPlatform = GameObject.FindGameObjectWithTag("Spawn");
    }


    void Update()
    {
        RaycastHit hit;
        var raycast = (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance));
        if(raycast)
        {
            var objectBelowPlayer = hit.collider.gameObject;
            var zone = objectBelowPlayer.transform.root.GetComponent<Zone>();
            if(zone != null)
            {
                zoneSwitcher.SwithZone(zone.index);
                Debug.Log("Hit! " + zone.index);
            }

            if(!objectBelowPlayer.CompareTag("Spawn"))
            {
                spawnPlatform.SetActive(false);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * raycastDistance);
    }
}
