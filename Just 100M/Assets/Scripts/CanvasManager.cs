using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public PlayerController playerController;
    public ScoreManager scoreManager;
    public CoinCollector coinCollector;

    private int score;

    public GameObject deadMenuMonster;
    public GameObject deadMenuPlayer;
    public TextMeshProUGUI monsterment;
    public TextMeshProUGUI playerment;

    public AudioClip magicSound;

    
    private void Update()
    {
        score = coinCollector.score;

        if (playerController.isDead ||
            scoreManager.timeOver)
        {
            if (score >= 80)
            {
                deadMenuPlayer.SetActive(true);
                playerment.text = $"오늘의 행운 점수는\r\n{score} 점이야 ~ ♡";
            }
            else if (score < 80)
            {
                deadMenuMonster.SetActive(true);
                monsterment.text = $"오늘의 행운 점수는\r\n{score} 점이야 (부끄..)";
            }

        }
    }

    public void clickMainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
