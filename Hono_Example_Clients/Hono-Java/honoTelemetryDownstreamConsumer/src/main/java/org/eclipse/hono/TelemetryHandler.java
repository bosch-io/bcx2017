package org.eclipse.hono;

//import org.eclipse.hono.client.HonoClient;
//import org.eclipse.hono.client.MessageConsumer;
import org.eclipse.hono.util.MessageHelper;
import org.apache.qpid.proton.amqp.messaging.AmqpValue;
import org.apache.qpid.proton.amqp.messaging.Data;
import org.apache.qpid.proton.amqp.messaging.Section;
import org.apache.qpid.proton.message.Message;

public class TelemetryHandler {
    public void handleTelemetryMessage(final Message msg) {
        final Section body = msg.getBody();
        String content = null;
        if (!(body instanceof Data))
            return;

        content = ((Data) msg.getBody()).getValue().toString();

        final String deviceId = MessageHelper.getDeviceId(msg);

        StringBuilder sb = new StringBuilder("received message [device: ").
                append(deviceId).append(", content-type: ").append(msg.getContentType()).append(" ]: ").append(content);

        if (msg.getApplicationProperties() != null) {
            sb.append(" with application properties: ").append(msg.getApplicationProperties().getValue());
        }

        System.out.println(sb.toString());
    }
}
