import java.io.BufferedReader;
import java.io.IOException;
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

        System.out.println("Waiting for events");

        while (UserInterface.isConnected()) {
            try {
                checkForEvents();
                Thread.sleep(1000);
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    private void checkForEvents() {

        BufferedReader inputReader = null;

        try {
            serverSocket = new ServerSocket(LISTENING_PORT);
            serverSocket.setReuseAddress(true);

        } catch (IOException e) {
            System.err.println("Could not listen on port " + LISTENING_PORT);

        }

        Socket clientSocket = null;


        try {
            clientSocket = serverSocket.accept();
        } catch (Exception e) {
            e.printStackTrace();

        }

        try {

            inputReader = new BufferedReader(
                    new InputStreamReader(clientSocket.getInputStream()));

            String inputLine;

            while ((inputLine = inputReader.readLine()) != null) {
                if (inputLine.startsWith("<?xml")) {
                    UserInterface.eventField.setText(getEventFromMessage(inputLine, UserInterface.eventField.getText()));
                }
            }

        } catch (Exception ex) {
            ex.printStackTrace();
        }
        finally {
            try {
                inputReader.close();
                clientSocket.close();
                serverSocket.close();
            }
            catch (Exception e)
            {
                e.printStackTrace();
            }

        }
    }

    private String getEventFromMessage(String inputLine, String oldEvents) {

        String event = "";

        if (inputLine.toLowerCase().contains("globalchange")) {
            event = "Camera shaken";
        }
        if (inputLine.toLowerCase().contains("detect_any_object")) {
            event = "Object detected";
        }
        if (inputLine.toLowerCase().contains("audio")) {
            event = "Audio alarm";
        }

        return oldEvents + "\r\n" + event + "\r\n";
    }
}
