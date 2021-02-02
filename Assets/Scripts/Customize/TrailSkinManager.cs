using System.Collections.Generic;
using UnityEngine;

public class TrailSkinManager : MonoBehaviour
{
    public TrailRenderer trail;
    public GameObject player;
    public List<SkinInfo> skins = new List<SkinInfo>();
    private int currentSkin = 0;

    public void nextSkin()
    {
        int index = currentSkin += 1;
        if (index < skins.Count)
        {
            SkinInfo skin = skins[index];
            if (skin.skin != null)
            {
                GameObject effect = GameObject.FindGameObjectWithTag("Trail");
                if (effect != null)
                {
                    Destroy(effect);
                }
                trail.gameObject.SetActive(true);
                trail.material = skin.skin;
            }
            if(skin.effect != null)
            {
                trail.gameObject.SetActive(false);
                GameObject effect = GameObject.FindGameObjectWithTag("Trail");
                if(effect != null)
                {
                    Destroy(effect);
                }
                GameObject newTrail = Instantiate(skin.effect, player.transform.position, Quaternion.identity);
                newTrail.transform.parent = player.transform;
            }
        }
        else
        {
            currentSkin = skins.Count - 1;
        }
    }

    public void previousSkin()
    {
        int index = currentSkin -= 1;
        if (index >= 0)
        {
            SkinInfo skin = skins[index];
            if (skin.skin != null)
            {
                GameObject effect = GameObject.FindGameObjectWithTag("Trail");
                if (effect != null)
                {
                    Destroy(effect);
                }
                trail.gameObject.SetActive(true);
                trail.material = skin.skin;
            }
            if(skin.effect != null)
            {
                trail.gameObject.SetActive(false);
                GameObject effect = GameObject.FindGameObjectWithTag("Trail");
                if(effect != null)
                {
                    Destroy(effect);
                }
                GameObject newTrail = Instantiate(skin.effect, player.transform.position, Quaternion.identity);
                newTrail.transform.parent = player.transform;
            }
            
        }
        else
        {
            currentSkin = 0;
        }
    }

    public SkinInfo getselectedItem()
    {
        return skins[currentSkin];
    }


    public string getTrailName()
    {
        Material material = skins[currentSkin].skin;
        if (material != null)
        {
            return skins[currentSkin].skin.name;
        }
        else
        {
            return skins[currentSkin].effect.name;
        }
    }
}
