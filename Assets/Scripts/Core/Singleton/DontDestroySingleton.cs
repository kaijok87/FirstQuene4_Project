using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DontDestroySingleton<T> : MonoBehaviour where T : Component
{
    static bool isShutDown = false;
    static T instance;
    public static T Instance 
    {
        get 
        {
            if (isShutDown)  //어플 종료중인가? 체크하고 
            {
                Debug.LogWarning($"{typeof(T).Name}은 이미 삭제 중이다.");
                return null;    //종료중이면 널
            }
            else if (instance != null)  //값이있으면 
            {
                return instance;    //그대로 반환
            }
            else    // 값이없으면 
            {
                if (FindObjectOfType<T>(true)  == null) //기존씬에 있는지 체크하고  
                {
                    //없으면 생성
                    GameObject gameObject = new GameObject($"DontDestroySingleton_{typeof(T).Name}");
                    instance = gameObject.AddComponent<T>();
                    DontDestroyOnLoad(gameObject);
                }
                return instance;
            }
        }
    }



    protected virtual void Awake()
    {
        if (instance == null) //오브젝트 생성시 해당값이 없는경우 처음 생성되는 것이니
        {
            instance = this as T;                       // 형변환해서 담고
            DontDestroyOnLoad(instance.gameObject);     //파괴방지걸어둔다
        }
        else if(instance != this) //뭔가 값이존재할경우 해당 오브젝트를 두번이상 생성한것이니 
        {
            Destroy(this.gameObject); // 나중에 생성된 this 는 파괴시킨다
        }
    }

    /// <summary>
    /// 어플리케이션 종료중일때 호출되는 함수
    /// </summary>
    private void OnApplicationQuit()
    {
        isShutDown = true;
    }
}
