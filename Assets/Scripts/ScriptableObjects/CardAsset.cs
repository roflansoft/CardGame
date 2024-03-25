using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New CardAsset", menuName = "Card Asset", order = 51)]
    public class CardAsset : ScriptableObject
    {
        [SerializeField] public string cardName;

        [SerializeField] public Color color;

        [SerializeField] public Sprite picture;
    }
}
