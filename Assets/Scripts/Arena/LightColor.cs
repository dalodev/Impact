using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightColor : MonoBehaviour
{

    public HardLight2D light2D;
    public Color lightColor;
    public ParticleSystem particleSys;

    // Start is called before the first frame update
    void Start()
    {
        Material material = particleSys.GetComponent<ParticleSystemRenderer>().material;
        Color color = material.color;
        float alpha = light2D.Color.a;
        lightColor = new Color(color.r, color.g, color.b, lightColor.a);
        light2D.Color = lightColor;
    }

    
}
