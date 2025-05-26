using UnityEngine;


public class Item : MonoBehaviour
{
    public ItemData data;

    [HideInInspector] public Rigidbody2D rbody;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }
}