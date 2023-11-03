
using System;
using UnityEngine;
public enum DamageType 
{ }

/// <summary>
/// 유닛 각각의 개체를 관리할 클래스 
/// 맴버로 구조체를 사용하기때문에 구조체를 수정할수있는 기능을 제공해줘야 한다.
/// 구조체는 기본적으로 불변( ReadOnly 와 비슷한개념 ) 임으로 콜바이 벨류 로인해 값이 복사된다. 
/// 구조체의 Set Get 프로퍼티를사용하면 전체내용이 복사된다 ref 예약어는 작동하지않는다 return 예약어가 ref 를 처리하지않는다. 
/// </summary>
[Serializable]
public class UnitData
{
    /// <summary>
    /// 유닛 개체를 관리할 인덱스값 
    /// </summary>
    [SerializeField]
    int unitIndex;
    public int UnitIndex => unitIndex;

    /// <summary>
    /// 유닛의 정적으로 생성된 데이터 저장할 구조체 
    /// </summary>
    [SerializeField]
    UnitDataBase unitDataBase;

    /// <summary>
    /// 유닛의 동적으로 생성된 데이터 저장할 구조체 
    /// </summary>
    [SerializeField]
    UnitStateData unitStateData;

    /// <summary>
    /// 장비하고있는 장비클래스 
    /// </summary>
    [SerializeField]
    UnitEquipBase unitEquipBase;

  
    public UnitData() 
    {
        unitEquipBase = new UnitEquipBase();        //기본틀 잡는다 
    }

    /// <summary>
    /// 유닛의 이름을 반환하는 함수
    /// </summary>
    /// <returns>유닛의 이름</returns>
    public string GetUnitName() 
    {
        return unitStateData.UnitName;
    }

    public void SetFlockingPos(in Vector3 initPos) 
    {
        unitStateData.ColonyMemberPosition = initPos;   
    }

    /// <summary>
    /// 장비 변경요청 
    /// 처리 할것들 
    /// </summary>
    /// <param name="equipData">변경할 장비 </param>
    public void ChangeEquip(in EquipItem equipData) 
    {

        switch (equipData.Type) //장비 타입으로 각각 처리 
        {
            case EquipType.None:
                //장비 해제 
                break;
            case EquipType.Weapone:
                //unitEquipBase.WeaponLeft;
                //unitEquipBase.WeaponRight;
                //무기장착
                // 1. 오른손 먼저 장착 오른속있을경우 왼손
                // 2. 방패면 왼손으로 장착
                // 3. 방패착용상태에서 무기장착시 방패 장착 해제 
                break;
            case EquipType.Sheild:
                break;
            case EquipType.Armor:
                break;
            case EquipType.Helm:
                break;
            case EquipType.Glove:
                break;
            case EquipType.Boot:
                break;
            case EquipType.Accessories:
                break;
            default:
                break;
        }
    }


    /// <summary>
    /// 데미지를 받을 때 처리할 함수 
    /// </summary>
    public void TakeDamage() 
    {

    }
    public float AttackDamage(DamageType type) 
    {
        float damage = 0.0f;
        return damage;
    }

    private void MethodTest() 
    {
    }
    public void OnDataChange(int unitIndex, in UnitDataBase unitDataBase, in UnitStateData unitStateData)
    {
        this.unitIndex = unitIndex;
        this.unitDataBase = unitDataBase;
        this.unitStateData = unitStateData;
    }
  
}