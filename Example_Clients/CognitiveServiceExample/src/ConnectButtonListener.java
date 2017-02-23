import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

/**
 * Created by DaveKing on 19.02.2017.
 */
public class ConnectButtonListener implements ActionListener {
    @Override
    public void actionPerformed(ActionEvent e) {
            Thread connection = new Thread(new CameraConnector());
            connection.start();
    }
}

