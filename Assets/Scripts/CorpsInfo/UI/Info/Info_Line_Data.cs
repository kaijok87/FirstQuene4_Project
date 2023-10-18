
using UnityEngine;

public class Info_Line_Data : MonoBehaviour
{
    uint line_Index = uint.MaxValue;
    public uint line_Length 
    {
        get => line_Index;
        set
        {
            line_Index = value;
        }
    }

}
