using UnityEngine;
using UnityEngine.Tilemaps;


public class TileManager : MonoBehaviour
{
    // 상호작용 맵에 대한 설정
    [SerializeField] private Tilemap interactables;

    // 타일 (상호작용 이미지 가리기)
    [SerializeField] private Tile hidden;
    // 타일 (상호작용)
    [SerializeField] private Tile interacted;
    // 타일 (심어진 위치)
    [SerializeField] private Tile grown;

    public Tilemap Interactables { get { return interactables; } }

    private void Awake()
    {
        foreach (var pos in interactables.cellBounds.allPositionsWithin)
        {
            // TileBase 는 타일 클래스의 기초 클래스 
            TileBase exist = interactables.GetTile(pos);

            if (exist != null)
            {
                interactables.SetTile(pos, hidden);
            }
        }
    }

    public bool isInteractable(Vector3Int pos)
    {
        var tile = interactables.GetTile(pos);

        if (tile != null)
        {
            // 현재
            if (tile.name == "Hidden")
            {
                return true;
            }
        }
        return false;
    }

    public void SetInteract(Vector3Int position)
    {
        interactables.SetTile(position, interacted);
    }

    public void SetGrown(Vector3Int position)
    {
        interactables.SetTile(position, grown);
    }

    public string GetTile(Vector3Int pos)
    {
        if (interactables != null)
        {
            var tile = interactables.GetTile(pos);

            if (tile != null)
            {
                return tile.name;
            }
        }
        return "";
    }
}