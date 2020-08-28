namespace Interactions
{
    public class InteractableItemOnEnable : InteractableItem
    {
        private void OnEnable()
        {
            if (gameObject.layer == (int)LayerValue.Interactable)
            {
                OnInteract();
                OnFinishedInteraction();
            }
        }
    }
}