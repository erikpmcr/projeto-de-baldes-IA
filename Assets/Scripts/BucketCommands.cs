using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketCommands : MonoBehaviour
{ 
    public void fill(Bucket bk)
    {
        bk.bucketCurrent = bk.bucketMax;
    }
    public void empty(Bucket bk)
    {
        bk.bucketCurrent = 0;
    }
    public void transfer(Bucket bkx, Bucket bky)
    {
        if (bky.bucketMax < bky.bucketCurrent + bkx.bucketCurrent)
        {
            int tempI = bky.bucketCurrent - bkx.bucketCurrent;
            bky.bucketCurrent += tempI;
            bkx.bucketCurrent -= tempI;
        }
    }

}
