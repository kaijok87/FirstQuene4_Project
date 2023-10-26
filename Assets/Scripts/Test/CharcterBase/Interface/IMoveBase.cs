using UnityEngine;
public interface IMoveBase
{
    /// <summary>
    /// �浹�� �������� ������ �Լ�
    /// </summary>
    //void PropIsCollision(bool isCollision);

    void SetMoveDistanceSubtractiveOperation(float targetRadiusValue = 0.0f);
    void Teleportation(Vector3 endPos);
    void OnMove(Vector3 direction, float distance);
    
    void OnMovingStop();

    void InitDataSetting(float charcterHalfSize);
}
