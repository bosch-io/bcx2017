using System;
using System.IO;

namespace MicrosoftCognitiveServiceExample
{
    public class Configuration
    {   

        public static void ReadConfig(String Filename)
        {
            // Read every line in the file.
            using (StreamReader reader = new StreamReader(Filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Split line using before equals as key word and after equals as value
                    string[] parts = line.Split('=');

                    if (parts != null && parts.Length == 2 && parts[0].StartsWith("SubscriptionKeyForEmotion"))
                    {
                        UserInterface.SUBSCRIPTION_KEY_FOR_EMOTION = parts[1];                       
                    }
                    if (parts != null && parts.Length == 2 && parts[0].StartsWith("DefaultCameraIP"))
                    {
                        UserInterface.DEFAULT_IP = parts[1];
                    }
                    if (parts != null && parts.Length == 2 && parts[0].StartsWith("PicturePath"))
                    {
                        UserInterface.PICTURE_PATH = parts[1];
                    }
                }
            }
        }
    }
}
