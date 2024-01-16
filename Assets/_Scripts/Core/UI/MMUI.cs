using UnityEngine;

public class MMUI : SingletonMonoBehaviour<MMUI>
{
    [Header("DEPENDENCIES")]
    public MMEnterUI mmEnterPanel;
    public PreMMUI premmPanel;
    public MMStaminaUI staminaPanel;
    public MMAccuracyUI accuracyPanel;
    public MMPlayUI mmPlayPanel;
    public MMEndUI mmEndPanel;
    public MMInputReciever inputReciever;
    public MMScoreUI scoreUI;

    protected override void Awake()
    {
        base.Awake();
    }

    #region BUTTON CALLBACKS
    public void OnMatchMakingButtonClicked()
    {
        //MM enter panel
        mmEnterPanel.SwitchMatchButton(false);

        //Pre-MM panel
        premmPanel.SwitchStartMatchButton(true);
        premmPanel.SwitchTrainingButton(true);
        premmPanel.SwitchStaminaPanel(true);
        premmPanel.SwitchAccuracyPanel(true);
        premmPanel.SwitchYouText(true);
        premmPanel.SwitchEnemyText(true);

        //Stamina panel
        staminaPanel.SetPlayerStaminaText(premmPanel.mmData.PlayerStamina);
        staminaPanel.SetEnemyStaminaText(CalculateEnemyStamina());

        //Accuracy panel
        accuracyPanel.SetPlayerAccuracyText(premmPanel.mmData.PlayerAccuracy);
        accuracyPanel.SetEnemyAccuracyText(CalculateEnemyAcccuracy());

        //Name panel
        premmPanel.SetOpponentName(GetOpponentName());
    }

    public void OnKeepTrainingButtonClicked()
    {
        //MM enter panel
        mmEnterPanel.SwitchMatchButton(true);

        //Pre-MM panel
        premmPanel.SwitchStartMatchButton(false);
        premmPanel.SwitchTrainingButton(false);
        premmPanel.SwitchStaminaPanel(false);
        premmPanel.SwitchAccuracyPanel(false);
        premmPanel.SwitchYouText(false);
        premmPanel.SwitchEnemyText(false);
    }

    public void OnMatchTimeButtonClicked()
    {
        //Pre-MM panel
        premmPanel.SwitchAccuracyPanel(false);
        premmPanel.SwitchStaminaPanel(false);
        premmPanel.SwitchStartMatchButton(false);
        premmPanel.SwitchTrainingButton(false);
        premmPanel.SwitchEnemyText(false);
        premmPanel.SwitchYouText(false);

        //MM play panel
        mmPlayPanel.SwitchMMPlayText(true);
        mmPlayPanel.SwitchHand(true);

        //Opponent name set
        scoreUI.SetOpponentName(GetOpponentName());

        Instantiate(inputReciever, Vector3.zero, Quaternion.identity);
    }

    public void OnBackToTrainingButtonClicked()
    {
        //MM play panel
        mmPlayPanel.SwitchScorePanel(false);
        mmPlayPanel.SwitchTimerPanel(false);
        mmPlayPanel.SwitchMMPlayText(false);

        //MM end panel
        mmEndPanel.SwitchMMEndPanelObjects(false);

        //MM enter panel
        mmEnterPanel.SwitchMatchButton(true);
    }
    #endregion

    #region GAME CALLBACKS
    public void OnGameInit()
    {
        // MM enter panel
        mmEnterPanel.SwitchMatchButton(false);

        //Pre-MM panel
        premmPanel.SwitchStartMatchButton(false);
        premmPanel.SwitchTrainingButton(false);
        premmPanel.SwitchStaminaPanel(false);
        premmPanel.SwitchAccuracyPanel(false);
        premmPanel.SwitchYouText(false);
        premmPanel.SwitchEnemyText(false);

        //MM play panel
        mmPlayPanel.SwitchScorePanel(false);
        mmPlayPanel.SwitchTimerPanel(false);
        mmPlayPanel.SwitchMMPlayText(false);
        mmPlayPanel.SwitchHand(false);

        //MM end panel
        mmEndPanel.SwitchMMEndPanelObjects(false);
    }

    public void OnGameStart()
    {
        //MM enter panel
        mmEnterPanel.SwitchMatchButton(true);
    }
    #endregion

    #region MM CALLBACKS

    public void OnMatchStart()
    {
        mmPlayPanel.SwitchScorePanel(true);
        mmPlayPanel.SwitchTimerPanel(true);
        mmPlayPanel.SwitchMMPlayText(false);
        mmPlayPanel.SwitchHand(false);
        mmPlayPanel.timerPanel.StartTimer();
    }

    public void OnMatchEnd()
    {
        //MM panel
        mmEndPanel.SwitchMMEndPanelObjects(true);
    }
    #endregion

    private float CalculateEnemyStamina()
    {
        return AIManager.Instance.CurrentAIData.Stamina;
    }

    private int CalculateEnemyAcccuracy()
    {
        return (int)AIManager.Instance.CurrentAIData.Accuracy;
    }

    private string GetOpponentName()
    {
        return AIManager.Instance.CurrentAIData.Name;
    }

    public void WhoWinTheGame()
    {
        var playerPoint = PlayerController.Instance.GetPoint();
        var aiPoint = AIManager.Instance.CurrentAI.GetPoint();

        if (playerPoint > aiPoint)
        {
            mmEndPanel.SetWinText("YOU BEAT "+ AIManager.Instance.CurrentAIData.Name);
            PlayerController.Instance.WinPrice(AIManager.Instance.CurrentAIData.WinPrice);
            mmEndPanel.SetWinPriceText(AIManager.Instance.CurrentAIData.WinPrice);
            AIManager.Instance.AILoseTheMatch();
        }
        else
        {
            mmEndPanel.SetWinText(AIManager.Instance.CurrentAIData.Name +" BEAT YOU");
        }
    }
}
