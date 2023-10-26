using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSearchController : MonoBehaviour
{
    [SerializeField]
    float searchRange = 1.0f;
    [SerializeField]
    LayerMask searchLayer;

    [SerializeField]
    LayerMask propLayer;

    /// <summary>
    /// 주변 검색해서 가까운 타겟의 위치 좌표값을 반환하는 함수
    /// </summary>
    /// <returns>타겟의 좌표값</returns>
    public Vector3 GetSearchTarget()
    {
        Collider[] checkingTarget = Physics.OverlapSphere(transform.position, searchRange, searchLayer); //주변 검색해서 유닛을 찾는다.
        Vector3 resultVector = transform.position; //기본반환값은 자기자신위치
        float targetRange = float.MaxValue;
        foreach (Collider target in checkingTarget)
        {
            if (targetRange > (target.transform.position - transform.position).sqrMagnitude)    //거리 비교하고 
            {
                //가까우면 위치 갱신
                if (!IsStraightProp(target.transform.position)) //진선에 장애물이 없으면 
                {
                    resultVector = target.transform.position;   //위치 갱신
                }
            }
        }
        return resultVector;
    }

    /// <summary>
    /// 진선에 장애물이 있는지 체크하는 함수
    /// </summary>
    /// <returns>장애물 있으면 true 없으면 false</returns>
    public bool IsStraightProp(Vector3 targetPos)
    {
        Vector3 dir = targetPos - transform.position;
        return Physics.Raycast(transform.position,dir,searchRange, propLayer);
    }
}
