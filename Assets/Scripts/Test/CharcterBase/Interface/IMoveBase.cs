using UnityEngine;
public interface IMoveBase
{
    /// <summary>
    /// 충돌한 반지름값 셋팅할 함수
    /// </summary>
    //void PropIsCollision(bool isCollision);

    void SetMoveDistanceSubtractiveOperation(float targetRadiusValue = 0.0f);
    void Teleportation(Vector3 endPos);
    void OnMove(Vector3 direction, float distance);
    
    void OnMovingStop();

    void InitDataSetting(float charcterHalfSize);
}
