using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UserInventoryServiceLocaterSO _userInventoryServiceLocater;

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _userInventoryServiceLocater.Service.AcquireRandomItem();
        }
    }
}