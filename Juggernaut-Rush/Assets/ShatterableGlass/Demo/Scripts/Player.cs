using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Simple player script.
// Press E to use buttons.
// Hold Q to slow motion.
public class Player : MonoBehaviour
{
    public float MoveSpeed = 6.0F;
    public float JumpSpeed = 8.0F;
    public float FallSpeed = 20.0F;
    private Vector3 MoveDirection = Vector3.zero;

    public float Sensitivity = 2f;

    public bool Smooth = false;

    // Texture in the center of the screen.
    public Texture Crosshair;
    int CrosshairSize;

    // Array of UseAreas.
    public UseArea[] UseAreas;    

    // Angles of the camera.
    float Yaw = 0.0f;
    float Pitch = 0.0f;

    // Previos values of yaw and pitch angles. Used to smooth camera movement.
    float[] YawAvg = new float[32];
    float[] PitchAvg = new float[32];

    // State of E and Q button in previos frame.
    bool PrevUse = false;
    bool PrevSlowMotion = false;

    // Add new Value to array and average all values.
    float Avg(float[] AvgValues, float New) {
        if (!Smooth)
            return New;
        float Acc = New;
        for (int i = 31; i > 0; i--)
        {
            AvgValues[i] = AvgValues[i - 1];
            Acc += AvgValues[i-1];
        }
        AvgValues[0] = New;
        return Acc / 32; 
    }

    private void Start()
    {              
        // Calculate CrosshairSize only once.
        CrosshairSize = Screen.height / 30;

        // Lock cursor.
#if UNITY_2017_1_OR_NEWER
        Cursor.lockState = CursorLockMode.Locked;
#else
        Screen.lockCursor = true;
#endif
    }
    
    void OnGUI()
    {
        // Draw Crosshair.
        GUI.DrawTexture(new Rect(Screen.width / 2 - CrosshairSize, Screen.height / 2 - CrosshairSize, CrosshairSize * 2, CrosshairSize * 2), Crosshair);
    }

    void Update()
    {
        // Basic Player Movement.
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            MoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            MoveDirection = transform.TransformDirection(MoveDirection);
            MoveDirection *= MoveSpeed;
            if (Input.GetButton("Jump"))
                MoveDirection.y = JumpSpeed;

        }
        MoveDirection.y -= FallSpeed * Time.deltaTime;
        controller.Move(MoveDirection * Time.deltaTime);

        // Basic Camera control.
        Yaw += Sensitivity * Avg(YawAvg, Input.GetAxis("Mouse X"));
        Pitch -= Sensitivity * Avg(PitchAvg, Input.GetAxis("Mouse Y"));

        Camera.main.transform.eulerAngles = new Vector3(Pitch, Yaw, 0.0f);

        transform.eulerAngles = new Vector3(0f, Yaw,0f);

        // Check E Key.
        bool Use = Input.GetKey(KeyCode.E);
        // Use() every assigned Button. Button will do its thing if player is in UseArea.
        if (Use && !PrevUse)
            foreach (UseArea Btn in UseAreas)
                Btn.Use();        
        // Save state.
        PrevUse = Use;

        // Ceck Q Key.
        bool SlowMotion = Input.GetKey(KeyCode.Q);

        // If Q was pressed first frame.
        if (SlowMotion && !PrevSlowMotion)
        {
            Time.timeScale = 0.25f;
            Time.fixedDeltaTime /= 4f;
        }

        // If Q was unpressed first frame.
        if (!SlowMotion && PrevSlowMotion)
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime *= 4f;
        }

        // Save state.
        PrevSlowMotion = SlowMotion;
    }
}