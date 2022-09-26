using UnityEngine;

public class GarageController : MonoBehaviour
{

    [SerializeField] private Camera camera;

    [SerializeField] private GarageCanvas garageCanvas;

    private float lerpSpeed = 10f;
    private Vector3 currentAngle, targetAngle;

    [SerializeField] private CarSpawner carSpawner;

    private string[] carNames = {"f", "me", "m", "me", "m", "me", "m", "me", "m"};
 
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

        int clamped = Clamp0360(currentAngle.y);
        
        garageCanvas.setCarNameText(carNames[getCurrentCarIndex()]);
    }

    private int getCurrentCarIndex()
    {
        return Mathf.RoundToInt(Clamp0360(camera.transform.eulerAngles.y) / 45);
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
