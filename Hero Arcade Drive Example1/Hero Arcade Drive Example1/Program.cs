using System;
using System.Threading;
using Microsoft.SPOT;
using System.Text;


using CTRE.Phoenix;
using CTRE.Phoenix.Controller;
using CTRE.Phoenix.MotorControl;
using CTRE.Phoenix.MotorControl.CAN;

namespace Hero_Arcade_Drive_Example1
{
    public class Program
    {
        /* create PWM controllers */
        static PWMSpeedController victor_right = new PWMSpeedController(CTRE.HERO.IO.Port3.PWM_Pin8);
        static PWMSpeedController victor_left = new PWMSpeedController(CTRE.HERO.IO.Port3.PWM_Pin9);
        static StringBuilder stringBuilder = new StringBuilder();


        static CTRE.Phoenix.Controller.GameController m_gamepad = null;

        public static void Main()
        {

            /* loop forever */
            while (true)
            {
                /* drive robot using gamepad */
                Drive();
                /* print whatever is in our string builder */
                Debug.Print(stringBuilder.ToString());
                stringBuilder.Clear();
                /* feed watchdog to keep Talon's enabled */    
 //               CTRE.Phoenix.Watchdog.Feed();
                /* run this task every 20ms */
                Thread.Sleep(20);
            }
        }
        /**
         * If value is within 10% of center, clear it.
         * @param value [out] floating point value to deadband.
         */
        static void Deadband(ref float value)
        {
            if (value < -0.10)
                        {
                /* outside of deadband */
            }
            else if (value > +0.10)
            {
                /* outside of deadband */
            }
            else
            {
                /* within 10% so zero it */
                value = 0;
            }
        }
        static void Drive()
        {
            if (null == m_gamepad)
                m_gamepad = new GameController(UsbHostDevice.GetInstance());
            /*tests for Game Pad*/
            if (m_gamepad.GetConnectionStatus() == CTRE.Phoenix.UsbDeviceConnection.Connected)
            { /* print the axis value */
                stringBuilder.Append("true ");
                /* allow motor control */
                CTRE.Phoenix.Watchdog.Feed(); }

            float x = -1 * m_gamepad.GetAxis(5);
            float y = m_gamepad.GetAxis(1);
  

            Deadband(ref x);
            Deadband(ref y);

            float leftThrot = x;
            float rightThrot = y;
         
            victor_left.Set(leftThrot);
            victor_right.Set(rightThrot);

            stringBuilder.Append(y + "  ");
            stringBuilder.Append(x);
            stringBuilder.Append("hi");
       

        }
    }
}