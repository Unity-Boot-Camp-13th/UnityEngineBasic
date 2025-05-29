using UnityEngine;

[CreateAssetMenu(menuName = "Tower/TowerData")]
public class TowerData : ScriptableObject
{
    public Sprite icon;
    public string tower_name;
    public float damage;
    public float range;
    public float attack_speed;
}