using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text wave_text;
    public Text gold_text;

    #region Singleton
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    public void UpdateGoldUI (int gold)
    {
        gold_text.text = gold.ToString();
    }
}