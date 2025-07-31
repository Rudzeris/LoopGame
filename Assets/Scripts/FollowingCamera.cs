using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField] private GameObject playerCharacter;
    [SerializeField] private float returnSpeed;
    [SerializeField] private float height;
    [SerializeField] private float rearDistance;
    [SerializeField] private float cameraXOffset;

    private Vector3 currentVector;

    void Start()
    {
        transform.position = new Vector3(playerCharacter.transform.position.x - cameraXOffset,
            playerCharacter.transform.position.y + height, playerCharacter.transform.position.z - rearDistance);
    }

    // Update is called once per frame
    void Update()
    {
        CameraMove();
    }

    private void CameraMove()
    {
        currentVector = new Vector3(playerCharacter.transform.position.x - cameraXOffset,
            playerCharacter.transform.position.y + height, playerCharacter.transform.position.z - rearDistance);
        transform.position = Vector3.Lerp(transform.position, currentVector, returnSpeed * Time.deltaTime);
    }
}
