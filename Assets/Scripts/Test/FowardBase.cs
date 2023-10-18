using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FowardBase : MonoBehaviour
{
    Transform parent;
    [SerializeField]
    float distance;
    private void Awake()
    {
        parent = transform.parent;
    }
    private void Update()
    {
        transform.localPosition = parent.forward * distance;
        transform.rotation = Quaternion.LookRotation(parent.forward);
    }
}
