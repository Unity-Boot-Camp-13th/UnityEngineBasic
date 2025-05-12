using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bulletFactory;

    public GameObject firePosition;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var bullet = Instantiate(bulletFactory);

            bullet.transform.position = firePosition.transform.position;
        }
    }
}
