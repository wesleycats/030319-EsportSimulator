using UnityEngine;

[System.Serializable]
public class Accommodation
{
    public enum Type { None, Garage, Apartment, House, LuxuryApartment }

    public Type type;
	public int cost;
	public int rent;
	public int[] trainingRates;
	public Sprite sprite;

    public Accommodation(Type aType, int aCost, int aRent, int[] aTrainingRates, Sprite aSprite)
    {
        type = aType;
		cost = aCost;
		rent = aRent;
		trainingRates = aTrainingRates;
		sprite = aSprite;
    }
}
