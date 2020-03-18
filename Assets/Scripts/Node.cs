using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Node<T>
{
    public Node<T> p;
    public List<Node<T>> c = new List<Node<T>>();
    public T data;
    
}
