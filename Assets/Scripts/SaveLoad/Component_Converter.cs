using UnityEngine;
using UnityEditor;


public abstract class Component_Converter : ScriptableObject
{
    public abstract string ToJson(GameObject gameObject);

    public abstract void FromJson(string data, in GameObject gameObject);
    
}