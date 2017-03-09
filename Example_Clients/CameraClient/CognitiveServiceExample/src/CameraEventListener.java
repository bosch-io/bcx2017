import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.ServerSocket;
import java.net.Socket;

/**
 * CameraEventListener retrieve all subscribed by the OnvifClientSoapRequests class and display on the event textfield
 */
public class CameraEventListener extends Thread {

    final static int LISTENING_PORT = 34567;

    ServerSocket serverSocket = null;

    public void run() {


        try{
            serverSocket = new ServerSocket(LISTENING_PORT);
            serverSocket.setReuseAddress(true);
            System.out.println("Waiting for events");
        }
        catch (Exception e1){
            e1.printStackTrace();
        }

        while (UserInterface.isConnected()) {
            try {
                checkForEvents();
                Thread.sleep(1000);
            } catch (Exception e2) {
                e2.printStackTrace();
            }

        }

        try {
            serverSocket.close();
        } catch (Exception e3) {
            e3.printStackTrace();
        }
    }


    private void checkForEvents() {

        BufferedReader inputReader;

        Socket clientSocket;

        try {

            clientSocket = serverSocket.accept();

            inputReader = new BufferedReader(
                    new InputStreamReader(clientSocket.getInputStream()));

            String inputLine;

            while ((inputLine = inputReader.readLine()) != null) {
                if (inputLine.startsWith("<?xml")) {
                    UserInterface.eventField.setText(getEventFromMessage(inputLine, UserInterface.eventField.getText()));
                }
                else {
                    System.out.println(inputLine);
                }
            }

            inputReader.close();
            clientSocket.close();

        } catch (Exception ex) {
            ex.printStackTrace();

        }
    }

    private String getEventFromMessage(String inputLine, String oldEvents) {

        String event = "";

        if (inputLine.toLowerCase().contains("globalscenechange")) {
            event = "Camera shaken";
        }
        if (inputLine.toLowerCase().contains("detect_any_object")) {
            event = "Object detected";
        }
        if (inputLine.toLowerCase().contains("imagetoodark")) {
            event = "Camera lens covered";
        }
        if (inputLine.toLowerCase().contains("imagetoobright")) {
            event = "Camera blended";
        }
        if (inputLine.toLowerCase().contains("audio")) {
            event = "Audio alarm";
        }

        System.out.println(inputLine);

        return oldEvents + "\r\n" + event + "\r\n";
    }
}
