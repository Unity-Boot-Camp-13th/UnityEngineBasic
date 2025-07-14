using UnityEngine;

public class ServiceInitializer : MonoBehaviour
{
    [SerializeField] UserInventoryServiceLocaterSO _userInventoryServiceLocater;

    private void Awake()
    {
        _userInventoryServiceLocater.Init();
    }
}