using Cinemachine;
using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    CinemachineVirtualCamera cmVCam;
    [SerializeField]
    PlayerAstralForm astral;
    [SerializeField]
    PlayerPhysicalForm physical;
    // Start is called before the first frame update
    private void OnEnable()
    {
        cmVCam = FindObjectOfType<CinemachineVirtualCamera>();
        if (FindObjectOfType<PlayerPhysicalForm>() != null)
        {
            physical = FindObjectOfType<PlayerPhysicalForm>();
            cmVCam.Follow = physical.transform;
        }
           

        if ((FindObjectOfType<PlayerAstralForm>() != null))
        {
            astral = FindObjectOfType<PlayerAstralForm>();
            cmVCam.Follow = astral.transform;
        }
           
    }
    // Update is called once per frame
    void Update()
    {

    }
}
