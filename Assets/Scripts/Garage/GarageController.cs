using UnityEngine;

public class GarageController : MonoBehaviour
{

    [SerializeField] private Camera camera;

    [SerializeField] private GarageCanvas garageCanvas;

    private float lerpSpeed = 10f;
    private Vector3 currentAngle, targetAngle;

    [SerializeField] private CarSpawner carSpawner;

    private string[] carNames = {"f", "me", "m"};
 
    public void Start()
    {
        currentAngle = camera.transform.eulerAngles;
        targetAngle = currentAngle;

        carNames[0] = "Jeep";
        carNames[1] = "Racecar";
        carNames[2] = "Protoype";
    }

    public void rotateRight()
    {
        targetAngle[1] += 45f;
    }
    
    public void rotateLeft()
    {
        targetAngle[1] -= 45f;
    }

    private void Update()
    {
        currentAngle = new Vector3(
            currentAngle.x,
            Mathf.LerpAngle(currentAngle.y, targetAngle.y, lerpSpeed * Time.deltaTime),
            currentAngle.z);
 
        camera.transform.eulerAngles = currentAngle;
        
        garageCanvas.setCarNameText(carNames[getCurrentCarIndex()]);

        if (carSpawner.getCurrentCarIndex() == getCurrentCarIndex())
        {
            garageCanvas.updateSelectButton(true);
        }
        else
        {
            garageCanvas.updateSelectButton(false);
        }

        if (getCurrentCarIndex() == 0)
        {
            garageCanvas.enableLeftButton(false);
        }
        else
        {
            garageCanvas.enableLeftButton(true);
        }
        
        if (getCurrentCarIndex() == carNames.Length - 1)
        {
            garageCanvas.enableRightButton(false);
        }
        else
        {
            garageCanvas.enableRightButton(true);
        }
    }

    private int getCurrentCarIndex()
    {
        return Mathf.RoundToInt(Clamp0360(targetAngle.y) / 45);
    }
    
    public int Clamp0360(float eulerAngles)
    {
        float result = eulerAngles - Mathf.CeilToInt(eulerAngles / 360f) * 360f;
        if (result < 0)
        {
            result += 360f;
        }
        return Mathf.RoundToInt(result);
    }

    public void selectCar()
    {
        carSpawner.setCurrentCar(getCurrentCarIndex());
    }
}
