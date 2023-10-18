using UnityEngine;
public interface IMoveBase 
{
    void SetMoveDistanceSubtractiveOperation(float targetRadiusValue = 0.0f);
    void Teleportation(Vector3 endPos);
    void OnMove(Vector3 direction, float distance);

    void OnMovingStop();

    void InitDataSetting();
}
