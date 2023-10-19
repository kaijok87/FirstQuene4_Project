using System;
using Unity.Collections;
using UnityEngine;

/// <summary>
/// ���� �ӵ� ������ �̳Ѱ�
/// ����� ������ int ���� �����Ѵ�.
/// </summary>
public enum GameSpeedType 
{
    Stop            = 0,            // ���� 0���
    OneFourth       = 25,           // 1/4���
    HalfSlow        = 50,           // 1/2���
    Nomal           = 100,          // 1  ���
    DoubleFast      = 200,          // 2  ���
    QuadrupleFast   = 400,          // 4  ���
    OctupleFast     = 800,          // 8  ���
}

public class GameManager : DontDistroySingleton<GameManager>
{
    /// <summary>
    /// ���Ӽӵ� ������ �̳� ��
    /// ���Ӽӵ� ���꺯�� ������ 
    /// </summary>
    [SerializeField]
    GameSpeedType gameSpeedType = GameSpeedType.Nomal;
    public GameSpeedType GameSpeedType 
    {
        get => gameSpeedType;
        set 
        {
            if (value != gameSpeedType) 
            {
                gameSpeedType = value;
                switch (gameSpeedType)
                {
                    case GameSpeedType.Stop:
                        Time.timeScale = 0.0f;
                        break;
                    case GameSpeedType.OneFourth:
                    case GameSpeedType.HalfSlow:
                    case GameSpeedType.Nomal:
                    case GameSpeedType.DoubleFast:
                    case GameSpeedType.QuadrupleFast:
                    case GameSpeedType.OctupleFast:
                    default:
                        Time.timeScale = 1.0f * ((float)gameSpeedType / 100.0f);
                        break;
                }
#if UNITY_EDITOR
                tempGameSpeedType = gameSpeedType;
#endif

            }
        }
    }

    /// <summary>
    /// BGM �Ҹ� ũ�� ��
    /// </summary>
    [SerializeField]
    float backGroundMusicVolumSize;

    /// <summary>
    /// ����Ʈ �Ҹ� ũ�� ��
    /// </summary>
    [SerializeField]
    float effectSoundVolumSize;


































#if UNITY_EDITOR
    
    GameSpeedType tempGameSpeedType = GameSpeedType.Nomal;
    private void OnValidate()
    {
        gameSpeedType = tempGameSpeedType;
    }
#endif
}
