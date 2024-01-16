using System.Collections;
using UnityEngine;

public class EnvironmentManager : SingletonMonoBehaviour<EnvironmentManager>
{
    [Header("DEPENDENCIES")]
    public EnvironmentController[] allEnvironments;
    public EnvironmentQueueData environmentQueueData;

    [Header("DEBUG")]
    public EnvironmentController currentEnv;

    private int currentEnvIndex;
    private int previousAngleIndex;
    private int currentEnvAngleIndex;

    public int CurrentEnvAngleIndex { get => currentEnvAngleIndex; }

    protected override void Awake()
    {
        base.Awake();

        SetInitialEnvironment();
    }

    public void SwitchToMMEnvironment()
    {
        AdjustCurrentEnvironment();

        currentEnv.SetPlayerTransform(0);

        currentEnv.SetCameraTransform(0);

        currentEnv.SetAITransform(0);

        RenderSettings.skybox = currentEnv.environmentData.SkyBoxMat;

        currentEnvAngleIndex = 0;
        previousAngleIndex = 0;

        void AdjustCurrentEnvironment()
        {
            for (int i = 0; i < allEnvironments.Length; i++)
            {
                allEnvironments[i].gameObject.SetActive(false);
            }

            currentEnvIndex = AIManager.Instance.AIIndexData.aiIndex;

            currentEnvIndex = currentEnvIndex % environmentQueueData.EnvironmentQueues.Length;

            currentEnv = allEnvironments[environmentQueueData.EnvironmentQueues[currentEnvIndex]];

            currentEnv.gameObject.SetActive(true);
        }
    }

    public void SwitchToTrainingEnvironment()
    {
        AdjustCurrentEnvironment();

        currentEnv.SetPlayerTransform(1);

        currentEnv.SetCameraTransform(1);

        RenderSettings.skybox = currentEnv.environmentData.SkyBoxMat;

        currentEnvAngleIndex = 1;
        previousAngleIndex = 1;

        void AdjustCurrentEnvironment()
        {
            for (int i = 0; i < allEnvironments.Length; i++)
            {
                allEnvironments[i].gameObject.SetActive(false);
            }

            currentEnvIndex = AIManager.Instance.AIIndexData.aiIndex;

            currentEnvIndex = currentEnvIndex % environmentQueueData.EnvironmentQueues.Length;

            currentEnv = allEnvironments[environmentQueueData.EnvironmentQueues[currentEnvIndex]];

            currentEnv.gameObject.SetActive(true);
        }
    }

    public IEnumerator ChangeEnvironmentAngles()
    {
        if (currentEnv.environmentData.PlayerTransforms.Length == 1)
        {
            Debug.Log("Add more than one element!");
            yield return null;
        }

        do
        {
            currentEnvAngleIndex = Random.Range(1, currentEnv.environmentData.PlayerTransforms.Length);

        } while (currentEnvAngleIndex == previousAngleIndex);

        previousAngleIndex = currentEnvAngleIndex;

        FingerInput.Instance.canRecieveInput = false;

        FingerInput.Instance.isDown = false;

        ThrowInputUI.Instance.ResetSlider();

        yield return new WaitForSeconds(1f);

        currentEnv.SetCameraTransform(currentEnvAngleIndex);

        currentEnv.SetPlayerTransform(currentEnvAngleIndex);

        currentEnv.SetAITransform(currentEnvAngleIndex);

        FingerInput.Instance.canRecieveInput = true;
    }

    private void SetInitialEnvironment()
    {
        for (int i = 0; i < allEnvironments.Length; i++)
        {
            allEnvironments[i].gameObject.SetActive(false);
        }

        currentEnvAngleIndex = 1;
        previousAngleIndex = 1;

        currentEnvIndex= AIManager.Instance.AIIndexData.aiIndex % allEnvironments.Length;

        currentEnv = allEnvironments[environmentQueueData.EnvironmentQueues[currentEnvIndex]];

        currentEnv.gameObject.SetActive(true);

        RenderSettings.skybox = currentEnv.environmentData.SkyBoxMat;
    }
}
