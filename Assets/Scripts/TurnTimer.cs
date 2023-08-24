using UnityEngine;
using UnityEngine.UI;

public class TurnTimer : MonoBehaviour
{
    public float turnDuration = 10f;
    public Text countdownText;
    private Game game;
    private bool isCounting;

    private void Start()
    {
        ResetTimer();
    }

    private void Update()
    {
        if (isCounting)
        {
            turnDuration -= Time.deltaTime;
            UpdateTimerDisplay();

            if (turnDuration <= 0f)
            {
                game.NextTurn();
                DestroyMovePlates();
            }
        }
    }

    public void StartCountdown(Text countdownText)
    {
        this.countdownText = countdownText;
        ResetTimer();
        turnDuration = 10f;
        isCounting = true;
    }
    
    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    public void Stop()
    {
        isCounting = false;
    }

    private void ResetTimer()
    {
        turnDuration = 10f;
        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        countdownText.text = Mathf.Ceil(turnDuration).ToString();
    }

    public void SetGame(Game game)
    {
        this.game = game;
    }
}