using System.Collections;
using UnityEngine;


public class FingerInput : SingletonMonoBehaviour<FingerInput>
{
    public bool canRecieveInput = false;
    public bool isDown = false;

    protected override void Awake()
    {
        base.Awake();
    }

    public void SwitchInputMode(bool on)
    {
        canRecieveInput = on;
    }

    public void PointerDown()
    {
        isDown = true;
    }

    public bool IsFingerHold()
    {
        if (Input.GetMouseButton(0) && isDown && canRecieveInput)
        {
            return true;
        }

        return false;
    }

    public bool IsFingerDown()
    {
        if (Input.GetMouseButtonDown(0) && isDown && canRecieveInput)
        {
            return true;
        }

        return false;
    }

    public bool IsFingerUp()
    {
        if (Input.GetMouseButtonUp(0) && isDown && canRecieveInput)
        {
            isDown = false;
            return true;
        }

        return false;
    }

    public void OnGameStart()
    {
        StartCoroutine(GameStartRoutine());
    }

    private IEnumerator GameStartRoutine()
    {
        yield return new WaitForSeconds(.1f);

        canRecieveInput = true;
    }
}
