using TopDown.Generator;
using UnityEngine;

public class ZoneChecker : MonoBehaviour
{
    private float raycastDistance = 5f;
    private ZoneSwitcher zoneSwitcher;

    private void Start()
    {
        zoneSwitcher = GameObject.FindGameObjectWithTag("Generator").GetComponent<ZoneSwitcher>();
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
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * raycastDistance);
    }
}
