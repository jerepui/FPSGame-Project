using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // PlayerController class manages all the behaviour regarding player controls and movement.  

    /*These values are in a Serialize Field, so the values can be changed in the editor without having to edit the values in code
      each time you want to test a different value.*/

    //This allows us to choose a camera in the editor to act as the player camera that is attached to the player object.
    [SerializeField] Transform playerCamera = null;

    //mouseSensitivity affects the turn speed of the camera when the mouse is moved.
    [SerializeField] float mouseSensitivity = 3.0f;

    //runSpeed affects the movement speed of the player.
    [SerializeField] float runSpeed = 30.0f;
    [SerializeField] float gravity = -13.0f;

    /*The moveSmoothTime and mouseSmoothTime values make the movement of the 
      camera and player end smoothly, instead of ending exactly when the movement is stopped by the player. The values are set in a 
      small range, so the smoothing doesn't make the movement slippery. */
    [SerializeField][Range(0.0f, 0.5f)] float moveSmoothTime = 0.2f;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;

    //Boolean value that defines if the mouse cursor is locked in place during play and if it is invisible.
    [SerializeField] bool lockCursor = true;

    //cameraPitch is the vertical rotation of the camera up and down.
    float cameraPitch = 0.0f;

    //The velocity of moving on the Y axis. Here used for falling because of gravity.
    float velocityY = 0.0f;

    //The variable where the player controller object is stored
    CharacterController controller = null;

    //Initializing variables for vector2.smoothDamp function.
    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;
    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
       // We lock the mouse cursor in place and make it hidden.

        controller = GetComponent<CharacterController>();
        if(lockCursor) {

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        //We call UpdateMouseLook() and UpdateMovement() functions each frame to update the movement of the camera and the player each frame. 
        UpdateMouseLook();
        UpdateMovement();
    }

    void UpdateMouseLook() {

        //We store the values of the vertical and horizontal movement of the mouse into a Vector2

        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        //Smoothens the change in the camera movement so the changes are gradual and more natural looking.
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        //Rotates the player object with the camera based on the horizontal mouse movement and the mouse sensitivity.
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);

        /*Vertical mouse movement value is positive moving up and negative moving down, but camera's rotation values are inverted, 
         * where rotating up has a negative value, vice versa. So we subtract the vertical mouse movement multiplied by mouse sensitivty from the cameraPitch */
        cameraPitch -= currentMouseDelta.y * mouseSensitivity;

        //Restricts camera from rotating vertically further than straight up and down.
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        //Rotates the camera up and down based on the vertical mouse movement.
        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

    }

    void UpdateMovement() {

        //We store the values of horizontal and vertical movement of the player in a Vector2. Horizontal movement values determined by A and D keys and Vertical by W and S keys
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Diagonal movement has a higher input value than any single direction, causing the player to move faster when moving diagonally. We normalize the previous vector so the value for each direction is max 1.
        targetDir.Normalize();

        //Smoothens the change in movement so player doesn't reach the target direction instantly, but steadily making the movement and stopping more natural.
        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        //Resets player's velocity to 0 when the object is on the ground.
        if(controller.isGrounded) {

            velocityY = 0.0f;
        }

        //Increases the velocity of the character based on gravity gradually.
        velocityY += gravity * Time.deltaTime;

        //We store the vertical and horizontal movement along with the effect of gravity in a vector3. Movement is affected by the runSpeed value.
        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * runSpeed + Vector3.up * velocityY;

        //Makes the player object move by the vector3 values gradually
        controller.Move(velocity * Time.deltaTime);
    }
}
