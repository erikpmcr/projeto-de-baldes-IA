using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BucketStateNode : Node<Vector2>
{
    public BucketStateNode bp;
    public List<BucketStateNode> bc = new List<BucketStateNode>();
    public Action inCommand;
    public bool canFill1;
    public bool canFill2;
    public bool canEmpty1;
    public bool canEmpty2;
    public bool canTransfer1t2;
    public bool canTransfer2t1;

    
}
