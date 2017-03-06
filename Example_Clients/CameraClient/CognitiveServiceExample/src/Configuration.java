import java.io.*;

/**
 * Configuration class load the default camera IP, subscription key and temporary picture path from the cfg file
 */

public class Configuration {

    public static void ReadConfig(String filename) {

        try {
            //  Read every line in the file.

            BufferedReader br = new BufferedReader(new FileReader(filename));
            String line;
            while ((line = br.readLine()) != null) {
                //  Split line using before equals as key word and after equals as value
                String[] parts = line.split("=");
                if (((parts != null) && ((parts.length == 2) && parts[0].startsWith("SubscriptionKeyForEmotion")))) {
                    UserInterface.SUBSCRIPTION_KEY_FOR_EMOTION = parts[1];
                }

               if (((parts != null) && ((parts.length == 2) && parts[0].startsWith("DefaultCameraIP")))) {
                    UserInterface.DEFAULT_IP = parts[1];
                }

                if (((parts != null)  && ((parts.length == 2) && parts[0].startsWith("PicturePath")))) {
                    UserInterface.PICTURE_PATH = parts[1];
                }

            }
        }
        catch (Exception ex)
        {
            ex.printStackTrace();
        }

    }
}


