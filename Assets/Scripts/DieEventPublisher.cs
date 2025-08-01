using TopDown.Movement;
using UnityEngine;
using UnityEngine.Events;

public class DieEventPublisher : MonoBehaviour
{
    public UnityEvent DieEvent;


    private void Start()
    {

    }

    private void Update()
    {
        CheckPlayerHeight();
    }
    private void CheckPlayerHeight()
    {
        if (transform.position.y <= -5)
        {
            DieEvent.Invoke();
        }
    }
}
