using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTweenManager : MonoBehaviour
{
    public LeanTweenType showType;
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.scale(gameObject, Vector3.one, 0.4f).setEase(showType);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
