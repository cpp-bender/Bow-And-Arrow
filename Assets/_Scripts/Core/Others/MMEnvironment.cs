using UnityEngine;
using DG.Tweening;


public class MMEnvironment : MonoBehaviour
{
    [Header("DEPENDENCIES")]
    public GameObject movingObjects;
    public MMCharacter player;
    public MMCharacter ai;

    public void MatchEnterButtonClicked()
    {
        player.SetRandomAnimation();
        player.SetOutfit();

        ai.SetRandomAnimation();
        ai.SetOutfit();

        MoveDown();
    }

    public void KeepTrainingButtonClicked()
    {
        MoveUp();
    }

    private void MoveDown()
    {
        movingObjects.transform.DOLocalMoveY(-8.75f, .5f).Play();
    }

    private void MoveUp()
    {
        movingObjects.transform.DOLocalMoveY(8.75f, .5f).Play();
    }
}
