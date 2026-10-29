using UnityEngine;

namespace Runtime.Gameplay
{
    public static class GameplayInput
    {
        public static float VerticalInput =>
            (Input.GetKey(KeyCode.W) ? 1f : 0f) - (Input.GetKey(KeyCode.S) ? 1f : 0f);
        public static float HorizontalInput =>
            (Input.GetKey(KeyCode.D) ? 1f : 0f) - (Input.GetKey(KeyCode.A) ? 1f : 0f);
        public static bool SprintHeld => Input.GetKey(KeyCode.LeftShift);

        public static bool InteractDown => Input.GetKeyDown(KeyCode.E);
        public static bool MenuDown => Input.GetKeyDown(KeyCode.Escape);

        public static float MouseX => Input.GetAxis("Mouse X");
        public static float MouseY => Input.GetAxis("Mouse Y");

        public static bool AltHeld => Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt);
        public static bool AltUp => Input.GetKeyUp(KeyCode.LeftAlt) || Input.GetKeyUp(KeyCode.RightAlt);
    }
}
