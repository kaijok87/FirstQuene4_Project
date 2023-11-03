using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ı����� �Ⱦ� �̱���
/// </summary>
public class NomalSingleton<T> : MonoBehaviour where T : Component
{
    /// <summary>
    /// ���� �����϶� �����ϴ� ��찡 �ִٰ� �Ͽ� �ۼ� 
    /// ġ�����ΰǰ�? üũ�� �ʿ� 
    /// </summary>
    static bool isShutDown = false;

    /// <summary>
    /// �̱��� ��ü 
    /// </summary>
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
                if (FindObjectOfType<T>(true) == null) //�������� �ִ��� üũ�ϰ�  
                {
                    //������ ����
                    GameObject gameObject = new GameObject($"NomalSingleton_{typeof(T).Name}");
                    instance = gameObject.AddComponent<T>();
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
        }
        else  //���� ���������Ұ�� �ش� ������Ʈ�� �ι��̻� �����Ѱ��̴� 
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
