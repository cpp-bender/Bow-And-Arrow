using UnityEngine;

public class MMCharacter : MonoBehaviour
{
    [Header("DEPENDENCIES")]
    public Animator animator;
    public OutfitDrawer outfit;

    public void SetRandomAnimation()
    {
        animator.SetInteger("IdleIndex", Random.Range(0, 3));
        animator.SetTrigger("Idle");
    }

    public void SetOutfit()
    {
        outfit.SyncCombine();
    }
}
