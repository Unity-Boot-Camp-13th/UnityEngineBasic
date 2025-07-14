using System;

public class UserInventoryItem
{
    public long SerialNumber { get; }
    public int ItemId { get; }
    private static readonly Random s_random = new Random();

    public static UserInventoryItem Acquire(int itemId)
    {
        long serialNumber = long.Parse($"{DateTime.Now.ToString("yyyymmdd")}{s_random.Next(10000):D4}");

        return new UserInventoryItem(serialNumber, itemId);
    }

    private UserInventoryItem(long serialNumber, int itemId)
    {
        SerialNumber = serialNumber;
        ItemId = itemId;
    }
}
