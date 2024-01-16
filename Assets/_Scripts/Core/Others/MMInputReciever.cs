using UnityEngine;

public class MMInputReciever : MonoBehaviour
{
    [Header("DEPENDENCIES")]
    public GameEvent Event;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Event.Raise();

            Destroy(gameObject);
        }
    }
}
