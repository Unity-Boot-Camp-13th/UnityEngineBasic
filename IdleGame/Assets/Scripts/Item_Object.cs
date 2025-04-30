using System.Collections;
using UnityEngine;

public class Item_Object : MonoBehaviour
{
    public float angle = 45.0f;

    IEnumerator Simulate(Vector3 pos)
    {
        var targetDistance = Vector3.Distance(transform.position, pos);

        var velocity = targetDistance / Mathf.Sin(angle * Mathf.Deg2Rad / 9.8f);

        // Mathf.Sin : 삼각함수 중에서 Sin 값을 반환하는 기능

        // 삼각형 기준으로 가로 세로를 각각 w, h라고 할 때 Sin 은 h / a(빗변) 을 계산하는 식
        // 유니티에서 각이 45도일 경우 빗변의 길이가 1인 삼각형이 만들어집니다. (유니티 자체 로직)

        // Mathf.Sin(45 * Mathf.Deg2Rad) ==> 빗변의 길이가 1이고 각도가 45도인 삼각형의 높이(h)를 리턴합니다.

        // Mathf.Cos(45 * Mathf.Deg2Rad) ==> 빗변의 길이가 1이고 각도가 45도인 삼각형의 밑변(w)을 리턴합니다.

        // Deg2Rad 는 도(Degree) --> 라디안 (Radian)으로 변경해주는 코드
        // 사용 이유 : 유니티에서 sin, cos 함수를 계산할 때 각도 단위를 라디안(radian)으로 사용하기 때문

        float sx = Mathf.Sqrt(velocity) * Mathf.Cos(angle * Mathf.Deg2Rad);
        float sy = Mathf.Sqrt(velocity) * Mathf.Sin(angle * Mathf.Deg2Rad);
    }
}