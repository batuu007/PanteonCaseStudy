using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallPainting : ChangeRegion
{
    private float screenWidth;
    private int numberOfRed;

    public float currentPercentage = 0;
    public float percentageToWin = 85;

    public CinemachineControl cinemachine;
    public GameObject paintingWall;
    public GameObject percentageUI;
    public GameObject gameOverUI;
    private GameObject percentageUiText;

    [SerializeField] private BoolType isGameOver;
    private void Start()
    {
        percentageUiText = percentageUI.transform.GetChild(0).gameObject;
        screenWidth = Screen.width;
    }
    private void Update()
    {
        AreaSelector();
        CheckIfGameOver();
    }
    private void RefreshPercentageUI()
    {
        percentageUiText.GetComponent<Text>().text = currentPercentage.ToString();
    }
    private void PaintWall()
    {
        if (currentPercentage >= percentageToWin)
        {
            FinishTheGame();
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Texture2D currentTexture;
            Vector3 mouseButtonPos;

            Vector3 relativeBottonPos = GetRelativeMousePositionToObjectTexture(out currentTexture, out mouseButtonPos);

            PaintSelectedPixels(currentTexture, relativeBottonPos);
        }
    }
    private void CheckIfGameOver()
    {
        if (isGameOver.value)
        {
            currentPlayerPhase = PlayerPhase.gameOver;
        }
    }
    private void AreaSelector()
    {
        switch (currentPlayerPhase)
        {
            case PlayerPhase.platforming:
                PlayerMovement.instance.SwerveMovement();
                PlayerMovement.instance.ForwardMovement();
                break;
            case PlayerPhase.painting:
                PaintWall();
                RefreshPercentageUI();
                break;
            default:
                break;
        }
    }
    private void FinishTheGame()
    {
        currentPlayerPhase = PlayerPhase.gameOver;
        gameOverUI.SetActive(true);
        percentageUI.SetActive(false);
        paintingWall.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishLine"))
        {
            currentPlayerPhase = PlayerPhase.painting;
            paintingWall.SetActive(true);
            percentageUI.SetActive(true);
            cinemachine.SwitchCam();
            PlayerMovement.instance.mouseSpeed = 0;
            PlayerMovement.instance.moveSpeed = 0;
        }
    }
    private Vector3 GetRelativeMousePositionToObjectTexture(out Texture2D texture, out Vector3 mouseClickPos)
    {
        //Texture to texture2D
        Texture mainTexture = paintingWall.GetComponent<Renderer>().material.GetTexture("_MainTex");

        texture = new Texture2D(mainTexture.width, mainTexture.height, TextureFormat.RGBA32, false);

        RenderTexture currentRT = RenderTexture.active;

        RenderTexture renderTexture = new RenderTexture(mainTexture.width, mainTexture.height, 32);
        Graphics.Blit(mainTexture, renderTexture);

        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();

        Color[] pixels = texture.GetPixels();

        RenderTexture.active = currentRT;


        //Converting mouseposition to pixel from the texture
        mouseClickPos = Input.mousePosition;

        //Get the screen size
        Vector3 screenSize = new Vector3(Screen.width, Screen.height, 0);
        Vector3 textureSize = new Vector3(texture.width, texture.height, 0);
        Vector3 textureScreenPosition = Camera.main.WorldToScreenPoint(paintingWall.transform.position);
        Vector3 textureStartPosition = textureScreenPosition - textureSize / 2;
        Vector3 relativeClickPosition = mouseClickPos - textureStartPosition;

        return relativeClickPosition;
    }
    private void PaintSelectedPixels(Texture2D currentTexture, Vector3 relativeClickPos)
    {
        Texture2D texture = currentTexture;
        paintingWall.GetComponent<Renderer>().material.SetTexture("_MainTex", texture);

        // RGBA32 texture format data layout exactly matches Color32 struct
        var data = texture.GetRawTextureData<Color32>();

        // fill texture data with a simple pattern
        Color32 red = new Color32(255, 0, 0, 255);
        int index = 0;
        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                if (x < relativeClickPos.x + 25 && x > relativeClickPos.x - 25 &&
                    y < relativeClickPos.y + 25 && y > relativeClickPos.y - 25)
                {
                    if (!CompareTwoColors(data[index], red))
                    {
                        data[index] = red;
                        numberOfRed += 1;
                    }
                }
                index++;
            }
        }
        // upload to the GPU
        texture.Apply();

        currentPercentage = (numberOfRed * 100) / (texture.height * texture.width);
    }
    private bool CompareTwoColors(Color32 color1, Color32 color2)
    {
        if (color1.r == color2.r &&
            color1.g == color2.g &&
            color1.b == color2.b &&
            color1.a == color2.a)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
