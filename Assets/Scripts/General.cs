using UnityEngine;

public class General : MonoBehaviour
{

    [SerializeField] private Camera carCamera, garageCamera;

    [SerializeField] private CanvasController _canvasController;
    

    private void Start()
    {
        carCamera.enabled = true;
        garageCamera.enabled = false;
        
    }

    public void exitGame()
    {
        Application.Quit();
    }
    
    public void switchFromGameToGarage()
    {
        carCamera.enabled = false;
        garageCamera.enabled = true;
        _canvasController.closePauseMenu();
        _canvasController.openGarageMenu();
    }
    
    public void switchFromGarageToGame()
    {
        carCamera.enabled = true;
        garageCamera.enabled = false;
        _canvasController.closeGarageMenu();
        _canvasController.openPauseMenu();
    }

    public void switchFromPauseToGame()
    {
        _canvasController.closePauseMenu();
        _canvasController.openTouchElemens();
    }
    
    public void switchFromGameToPause()
    {
        _canvasController.openPauseMenu();
        _canvasController.closeTouchElements();
    }
    
    public bool playingOnMobile()
    {
        return Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;
    }
}
