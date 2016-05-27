using UnityEngine;
using System.Collections;

public class Base : MonoBehaviour {
    private Rect rectangle;
    private Vector2 flagSpawnPoint;

    public Base (Rect rectangle)
    {
        this.rectangle = rectangle;
        //Sets the flag's spawnpoint in the exact center of the rectangle
        this.flagSpawnPoint = new Vector2(this.rectangle.x + this.rectangle.width / 2, this.rectangle.y + this.rectangle.height / 2);
    }

    public Rect GetRectangle()
    {
        return this.rectangle;
    }

    public void SetRectangle(Rect rectangle)
    {
        this.rectangle = rectangle;
        this.UpdateFlagSpawnPoint();
    }

    public void SetRectangle(Vector2 position, Vector2 size)
    {
        this.rectangle = new Rect(position, size);
        this.UpdateFlagSpawnPoint();
    }

    public void SetRectangle(float x, float y, float width, float height)
    {
        this.rectangle = new Rect(x, y, width, height);
        this.UpdateFlagSpawnPoint();
    }

    public Vector2 GetFlagSpawnPoint()
    {
        return this.flagSpawnPoint;
    }

    /// <summary>
    /// Recalculates the flag's spawnpoint
    /// </summary>
    private void UpdateFlagSpawnPoint()
    {
        this.flagSpawnPoint = new Vector2(this.rectangle.x + this.rectangle.width / 2, this.rectangle.y + this.rectangle.height / 2);
    }
}
