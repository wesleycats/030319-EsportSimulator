[System.Serializable]
public class Accommodation
{
    public enum Type { Garage, Apartment, House, LuxuryApartment }
    public Type type;

    public bool bought;

    public Accommodation(Type aType, bool aBought)
    {
        type = aType;
        bought = aBought;
    }
}
