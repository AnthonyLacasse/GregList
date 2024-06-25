public enum InteractibleType
{
    LIGHT,
    PLANT,
    DISPOSABLE,
    COLLECTIBLE,
    HIDESPOT,


    COUNT
}


public interface Interactable
{

    public void InRange(bool inRange);

    public void Use();

    public InteractibleType GetType();

}
