using UnityEngine;

[CreateAssetMenu(fileName = "New Bot name collection", menuName = "Bot/Bot Name Collection", order = 1)]
public class BotNameCollection : ScriptableObject
{
    [SerializeField] private string[] names;

    #region [- Behaviours -]
    public string GetRandomName()
    {
        return names[Random.Range(0, names.Length)];
    } 
    #endregion
}
