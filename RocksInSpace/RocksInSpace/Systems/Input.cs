using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RocksInSpace.Systems
{
    /// <summary>
    /// Contains all logic for processing player input via Keyboard, Mouse or Gamepad.
    /// Currently only supporting SinglePlayer.
    /// </summary>
    public static class Input
    {
        static KeyboardState oKB, cKB;
        static MouseState oMS, cMS;
        static GamePadState oGP, cGP;

        public static Vector2 mousePosition => cMS.Position.ToVector2();

        public static void Update()
        {
            oKB = cKB;
            cKB = Keyboard.GetState();

            oMS = cMS;
            cMS = Mouse.GetState();

            oGP = cGP;
            cGP = GamePad.GetState(0);
        }

        #region Keyboard

        /// <summary>
        /// Returns True if key is being held in current tick.
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public static bool GetKey(Keys key)
        {
            return cKB.IsKeyDown(key);
        }

        /// <summary>
        /// Returns True if key is pressed for single frame.
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public static bool GetKeyDown(Keys key)
        {
            return oKB.IsKeyUp(key) && cKB.IsKeyDown(key);
        }

        /// <summary>
        /// Returns True if key is released for single frame.
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public static bool GetKeyUp(Keys key)
        {
            return oKB.IsKeyDown(key) && cKB.IsKeyUp(key);
        }

        #endregion

        #region Mouse

        /// <summary>
        /// Returns True if button is being held in current tick.
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public static bool GetMouseButton(int button)
        {
            switch(button)
            {
                case 0: // Left Mouse Held
                    return cMS.LeftButton == ButtonState.Pressed;
                case 1: // Right Mouse Held
                    return cMS.RightButton == ButtonState.Pressed;
                case 2: // Middle Mouse Held
                    return cMS.MiddleButton == ButtonState.Pressed;
            }

            return false;
        }

        /// <summary>
        /// Returns True if button is pressed for single frame.
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public static bool GetMouseButtonDown(int button)
        {
            switch (button)
            {
                case 0: // Left Mouse Held
                    return (oMS.LeftButton == ButtonState.Released && cMS.LeftButton == ButtonState.Pressed);
                case 1: // Right Mouse Held
                    return (oMS.RightButton == ButtonState.Released && cMS.RightButton == ButtonState.Pressed);
                case 2: // Middle Mouse Held
                    return (oMS.MiddleButton == ButtonState.Released && cMS.MiddleButton == ButtonState.Pressed);
            }

            return false;
        }

        /// <summary>
        /// Returns True if button is released for single frame.
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public static bool GetMouseButtonUp(int button)
        {
            switch (button)
            {
                case 0: // Left Mouse Held
                    return (oMS.LeftButton == ButtonState.Pressed && cMS.LeftButton == ButtonState.Released);
                case 1: // Right Mouse Held
                    return (oMS.RightButton == ButtonState.Pressed && cMS.RightButton == ButtonState.Released);
                case 2: // Middle Mouse Held
                    return (oMS.MiddleButton == ButtonState.Pressed && cMS.MiddleButton == ButtonState.Released);
            }

            return false;
        }

        #endregion

        #region Gamepad

        /// <summary>
        /// Returns True if button is being held in current tick.
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public static bool GetGamepadButton(Buttons button)
        {
            return cGP.IsButtonDown(button);
        }

        /// <summary>
        /// Returns True if button is pressed for single frame.
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public static bool GetGamepadButtonDown(Buttons button)
        {
            return oGP.IsButtonUp(button) && cGP.IsButtonDown(button);
        }

        /// <summary>
        /// Returns True if button is released for single frame.
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public static bool GetGamepadButtonUp(Buttons button)
        {
            return oGP.IsButtonDown(button) && cGP.IsButtonUp(button);
        }

        #endregion
    }
}
