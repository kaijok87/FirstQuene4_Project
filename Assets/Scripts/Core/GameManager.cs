using System;
using Unity.Collections;
using UnityEngine;

/// <summary>
/// 게임 속도 조절용 이넘값
/// 백분율 단위의 int 값을 저장한다.
/// </summary>
public enum GameSpeedType 
{
    Stop            = 0,            // 정지 0배속
    OneFourth       = 25,           // 1/4배속
    HalfSlow        = 50,           // 1/2배속
    Nomal           = 100,          // 1  배속
    DoubleFast      = 200,          // 2  배속
    QuadrupleFast   = 400,          // 4  배속
    OctupleFast     = 800,          // 8  배속
}

public class GameManager : DontDistroySingleton<GameManager>
{
    /// <summary>
    /// 게임속도 조절용 이넘 값
    /// 게임속도 연산변수 조절용 
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
    /// BGM 소리 크기 값
    /// </summary>
    [SerializeField]
    float backGroundMusicVolumSize;

    /// <summary>
    /// 이팩트 소리 크기 값
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
