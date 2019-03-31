using UnityEngine;

[System.Serializable]
public struct ItemForm
{
    public string elementName;
    public Item.Type type;
    public Item.Quality quality;
    public int cost;
    public int gameKnowledge;
    public int teamPlay;
    public int mechanics;
    public Sprite sprite;
}
