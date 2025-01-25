using UnityEngine;

public abstract class BE_Brain : MonoBehaviour
{
    public BubbleEntity BubbleEntity { get; private set; }

    protected virtual void Awake()
    {
        BubbleEntity = GetComponentInParent<BubbleEntity>();
    }
}
