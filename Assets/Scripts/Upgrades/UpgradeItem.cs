using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade/Upgrade Item")]
public class UpgradeItem : ScriptableObject
{
    public float id;
    public string itemName;
    public Sprite sprite;
    public float cost;
    public Color backgroundColor;

}
