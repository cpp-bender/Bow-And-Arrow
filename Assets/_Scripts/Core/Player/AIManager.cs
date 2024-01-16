using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;


[DefaultExecutionOrder(-10)]
public class AIManager : SingletonMonoBehaviour<AIManager>
{
    [Header("DEPENDENCIES")]
    [SerializeField] List<AIData> aiDatas;
    [SerializeField] AIController aiPrefab;
    [SerializeField] EnvironmentManager environment;

    [Header("OVERWRITE")]
    [SerializeField] IntVariable outfitData;

    private AIController currentAI;
    private AIData currentAIData;
    private AIIndexData aIIndexData;

    public AIData CurrentAIData { get => currentAIData; }
    public AIController CurrentAI { get => currentAI; }
    public AIIndexData AIIndexData { get => aIIndexData; }

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        currentAIData = aiDatas[AIIndexData.aiIndex];

        HandleAIOutfit();
    }

    private void HandleAIOutfit()
    {
        outfitData.ApplyChange(currentAIData.CharacterClothIndex);
    }

    public void MatchInit()
    {
        currentAI = Instantiate(aiPrefab, Vector3.zero, aiPrefab.transform.rotation);

        currentAI.outfitDrawer.SyncCombine();

        currentAI.SetAIData(currentAIData);

        currentAI.transform.position = environment.currentEnv.environmentData.AiTransforms[environment.CurrentEnvAngleIndex].position;
        currentAI.transform.rotation = Quaternion.Euler(environment.currentEnv.environmentData.AiTransforms[environment.CurrentEnvAngleIndex].rotation);

        currentAI.transform.SetParent(transform);

        currentAI.canShoot = false;
    }

    public void MatchStart()
    {
        currentAI.canShoot = true;
    }

    public void MatchEnd()
    {
        currentAI.canShoot = false;
    }

    public void BackToTrainingButtonClick()
    {
        HandleAIOutfit();

        DestroyImmediate(currentAI.gameObject);
    }

    public void AILoseTheMatch()
    {
        if (AIIndexData.aiIndex < aiDatas.Count - 1)
        {
            AIIndexData.aiIndex++;
        }

        currentAIData = aiDatas[AIIndexData.aiIndex];
        currentAI.SetAIData(currentAIData);
    }

    private void SaveIndex()
    {
        SaveManager.Save(aIIndexData, SaveFiles.AIIndexSaveFile);
    }

    private void LoadIndex()
    {
        aIIndexData = SaveManager.Load(new AIIndexData(), SaveFiles.AIIndexSaveFile);
    }

    private void OnEnable()
    {
        LoadIndex();
    }

    private void OnDestroy()
    {
        SaveIndex();
    }

}
