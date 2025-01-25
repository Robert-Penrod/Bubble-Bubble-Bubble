using UnityEngine;

public class BubbleBuilder : MonoBehaviour
{
    [SerializeField] GameObject _bubblePrefab;

    private void Update()
    {
        Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //BubblePerimiterCast();
    }

    void BubblePerimiterCast(Vector2 origin, Vector2 point, float radius)
    {

    }
}
