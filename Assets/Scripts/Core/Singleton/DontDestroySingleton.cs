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
            if (isShutDown)  //���� �������ΰ�? üũ�ϰ� 
            {
                Debug.LogWarning($"{typeof(T).Name}�� �̹� ���� ���̴�.");
                return null;    //�������̸� ��
            }
            else if (instance != null)  //���������� 
            {
                return instance;    //�״�� ��ȯ
            }
            else    // ���̾����� 
            {
                if (FindObjectOfType<T>(true)  == null) //�������� �ִ��� üũ�ϰ�  
                {
                    //������ ����
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
        if (instance == null) //������Ʈ ������ �ش簪�� ���°�� ó�� �����Ǵ� ���̴�
        {
            instance = this as T;                       // ����ȯ�ؼ� ���
            DontDestroyOnLoad(instance.gameObject);     //�ı������ɾ�д�
        }
        else if(instance != this) //���� ���������Ұ�� �ش� ������Ʈ�� �ι��̻� �����Ѱ��̴� 
        {
            Destroy(this.gameObject); // ���߿� ������ this �� �ı���Ų��
        }
    }

    /// <summary>
    /// ���ø����̼� �������϶� ȣ��Ǵ� �Լ�
    /// </summary>
    private void OnApplicationQuit()
    {
        isShutDown = true;
    }
}
