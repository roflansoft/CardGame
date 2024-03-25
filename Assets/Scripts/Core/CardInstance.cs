using ScriptableObjects;

namespace Core
{
    public class CardInstance
    {
        public readonly CardAsset CardData;
        
        public int LayoutId { get; private set; }
        
        public int CardPosition { get; set; }

        public CardInstance(CardAsset cardData)
        {
            CardData = cardData;
            LayoutId = (int)Layout.None;
        }

        public void MoveToLayout(int layoutId)
        {
            var lastLayout = LayoutId;
            LayoutId = layoutId;
            CardGame.Instance.RecalculateLayout(layoutId);
            CardGame.Instance.RecalculateLayout(lastLayout);
        }
    }
}
