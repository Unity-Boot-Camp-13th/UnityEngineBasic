using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
	// 연결할 매니저
	public ItemManager ItemManager;
    #region Singleton
    public static GameManager Instance;
    public TileManager TileManager;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion
}