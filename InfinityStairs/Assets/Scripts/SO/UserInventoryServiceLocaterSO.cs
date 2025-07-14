using UnityEngine;

[CreateAssetMenu(fileName = "UserInventoryServiceLocaterSO", menuName = "Services/UserInventoryServiceLocaterSO")]
public class UserInventoryServiceLocaterSO : ScriptableObject
{
    public IUserInventoryService Service { get; private set; }

    public void Init()
    {
        Service = new UserInventoryService();
    }
}
