using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bucket : MonoBehaviour
{
    public int bucketCurrent;
    public int bucketMax;
    public Image current;
    public Sprite[] imgs;
    // Start is called before the first frame update
    void Start()
    {
        current = this.gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        bucketCurrent = Mathf.Clamp(bucketCurrent, 0, bucketMax);
        if (bucketCurrent < imgs.Length)
        {
            current.sprite = imgs[bucketCurrent];
        }
    }
}
