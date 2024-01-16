using UnityEngine;
using System.Collections;

public class BornToDie : MonoBehaviour
{
    [SerializeField] float deathDuration;

    private void Start()
    {
        StartCoroutine(OnDestroy());
    }

    private IEnumerator OnDestroy()
    {
        yield return new WaitForSeconds(deathDuration);

        if(gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}
