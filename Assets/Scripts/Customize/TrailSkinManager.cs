using System.Collections.Generic;
using UnityEngine;

public class TrailSkinManager : MonoBehaviour
{
    public TrailRenderer trail;
    public GameObject player;
    public List<SkinInfo> skins = new List<SkinInfo>();
    private int currentSkin = 0;

    void Awake()
    {
        CustomizeData data = SaveSystem.LoadCustomize();
        SkinInfo skin = skins.Find(item => findTrail(item, data));
        currentSkin = skins.IndexOf(skin);
        SelectSkin(currentSkin);
    }
    private bool findTrail(SkinInfo item, CustomizeData data)
    {
        bool find = false;
        if(item.skin != null)
        {
            trail.gameObject.SetActive(true);
            find = item.skin.name == data.trail;
        }
        if(item.effect != null)
        {
            find = item.effect.name == data.trail;
        }
        return find;
    }

    public void nextSkin()
    {
        int index = currentSkin += 1;
        if (index < skins.Count)
        {
            SelectSkin(index);
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
            SelectSkin(index);
        }
        else
        {
            currentSkin = 0;
        }
    }

    private void SelectSkin(int index)
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
        if (skin.effect != null)
        {
            trail.gameObject.SetActive(false);
            GameObject effect = GameObject.FindGameObjectWithTag("Trail");
            if (effect != null)
            {
                Destroy(effect);
            }
            GameObject newTrail = Instantiate(skin.effect, player.transform.position, Quaternion.identity);
            newTrail.transform.parent = player.transform;
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
