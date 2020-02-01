using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Borders : MonoBehaviour
{
    public float borderWidth;
    public List<GameObject> borders;
    public Camera cam;

    public SpriteRenderer road;
    public SpriteRenderer backgroundLeft;
    public SpriteRenderer backgroundRight;

    Vector2 getSpriteSize(SpriteRenderer sprite)
    {
        return new Vector2(sprite.sprite.bounds.size.x * sprite.transform.localScale.x, sprite.sprite.bounds.size.y * sprite.transform.localScale.y);
    }

    Vector2 getScreenSize()
    {
        float ratio = (float)Screen.width / (float)Screen.height;
        float screenSizeY = cam.orthographicSize * 2;
        float screenSizeX = screenSizeY * ratio;
        return new Vector2(screenSizeX, screenSizeY);
    }

    Vector2 getRoadSize()
    {
        var screeSize = getScreenSize();
        return new Vector2(getSpriteSize(road).x, screeSize.y / 2);
    }

    void applyBackground(SpriteRenderer background, bool left)
    {
        var screenSize = getScreenSize();
        var roadSize = getRoadSize();

        float sideSpace = (screenSize.x - roadSize.x) / 2;
        float originalRatio = background.sprite.bounds.size.y / background.sprite.bounds.size.x;
        float ratioX = sideSpace / background.sprite.bounds.size.x;
        float ratioY = ratioX;
        background.transform.localScale = new Vector3(ratioX, ratioY, 1);
        background.transform.position = new Vector3((left == true ? 1 : -1) * (-screenSize.x / 2 + sideSpace / 2), 0);
    }

    void applyBorders()
    {
        var roadSize = getRoadSize();

        borders[0].transform.position = new Vector3(0, roadSize.y + borderWidth / 2);
        borders[1].transform.position = new Vector3(0, -roadSize.y - borderWidth / 2);
        borders[2].transform.position = new Vector3(-roadSize.x / 2 - borderWidth / 2, 0);
        borders[3].transform.position = new Vector3(roadSize.x / 2 + borderWidth / 2, 0);
    }

    private void Update()
    {
        applyBorders();
        applyBackground(backgroundLeft, true);
        applyBackground(backgroundRight, false);
    }
}
