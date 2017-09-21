namespace GameEngine.Input
{
    /*public class AccelerometterInput
    {
        Accelerometer accelSensor;
        public Vector3 accelReading = new Vector3();
        bool accelActive = false;

        public AccelerometterInput()
        {
            accelSensor = new Accelerometer();

            // Add the accelerometer event handler to the accelerometer sensor.
            accelSensor.ReadingChanged +=
                new EventHandler<AccelerometerReadingEventArgs>(AccelerometerReadingChanged);
            try
            {
                accelSensor.Start();
                accelActive = true;
            }
            catch (AccelerometerFailedException)
            {
                // the accelerometer couldn't be started.  No fun!
                accelActive = false;
            }
            catch (UnauthorizedAccessException)
            {
                // This exception is thrown in the emulator-which doesn't support an accelerometer.
                accelActive = false;
            }
        }
        public void Stop()
        {
            // Stop the accelerometer if it's active.
            if (accelActive)
            {
                try
                {
                    accelSensor.Stop();
                }
                catch 
                {
                    // the accelerometer couldn't be stopped now.
                }
            }
        }
        public void AccelerometerReadingChanged(object sender, AccelerometerReadingEventArgs e)
        {
            accelReading.X = (float)e.X;
            accelReading.Y = (float)e.Y;
            accelReading.Z = (float)e.Z;
        }
    }*/
}
