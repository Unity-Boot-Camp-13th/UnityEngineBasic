using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; private set; }

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

    public int wave_count = 0; // ���̺� ī��Ʈ
    public int gold = 100; // ���� ���

    public void Earn (int amount)
    {
        gold += amount;
        // UI �Ŵ����� ���� ȭ���� ��� �ؽ�Ʈ�� ���� ó��
        UIManager.Instance.UpdateGoldUI(gold);
    }

    public bool Cost(int amount)
    {
        // ������ �ڽ�Ʈ�� ���ٸ�, false
        if (gold < amount)
        {
            return false;
        }
        // �Ϲ����� ����� �ڽ�Ʈ�� �����ϰ� true
        gold -= amount;
        UIManager.Instance.UpdateGoldUI(gold);
        return true;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        // �ٽ� ������ �� �ִ� ����� ������ְų�, ���� ���
    }
}
