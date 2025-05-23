using UnityEngine;

public class AnimalFSM : MonoBehaviour
{
    public enum State { Patrol, Lay }
    public State state = State.Patrol;

    public Transform[] patrolPoints;
    public GameObject item; // 동물이 생성할 아이템
    int idx = 0;
    bool isLaid = false;

    public Transform player;
    public float range = 3f;
    public float speed = 2f;

    private void Update()
    {
        float d = Vector2.Distance(transform.position, player.position);
        if (state == State.Patrol)
        {
            Patrol();

            if (d <= range && isLaid == false)
                state = State.Lay;
        }
        else
        {
            Lay();
            
            state = State.Patrol;
            NextPoint();
        }
    }

    void Patrol()
    {
        Vector2 current = transform.position;
        Vector2 target = patrolPoints[idx].position;
        Vector2 nextPos = Vector2.MoveTowards(current, target, speed * Time.deltaTime);
        transform.position = new Vector3(nextPos.x, nextPos.y, transform.position.z);

        if (Vector2.Distance(current, target) < 0.05f)
            NextPoint();
    }

    void Lay()
    {
        Instantiate(item, transform);
        isLaid = true;
    }

    void NextPoint()
    {
        idx = (idx + 1) % patrolPoints.Length;
    }
}
