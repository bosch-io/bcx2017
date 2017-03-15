package org.eclipse.hono;

import java.util.Map;

import org.apache.qpid.proton.amqp.messaging.Data;
import org.apache.qpid.proton.amqp.messaging.Section;
import org.apache.qpid.proton.message.Message;
import org.eclipse.hono.util.MessageHelper;

/**
 * Base class for advanced handling of telemetry-data
 */
public class EventHandler {

	public void handleMessage(final Message msg) {
		final Section body = msg.getBody();

		if (!(body instanceof Data)) {
			return;
		}

		String deviceId = MessageHelper.getDeviceId(msg);
		String contentType = msg.getContentType();
		String content = ((Data) msg.getBody()).getValue().toString();
		Map applicationProperties = msg.getApplicationProperties().getValue();

		// the only thing we are doing with the message is printing it to
		// System.out
		printFullMessage(deviceId, contentType, content, applicationProperties);
	}

	protected void handleDevice(final String deviceId, final String contentType, final String content,
			Map applicationProperties) {
		System.out.println("Received message for device [" + deviceId + "]");
	}
	
	protected void printFullMessage(final String deviceId, final String contentType, final String content,
			Map applicationProperties)
	{
		StringBuilder sb = new StringBuilder("received message [device: ").
                append(deviceId).append(", content-type: ").append(contentType).append(" ]: ").append(content);

        if (applicationProperties != null) {
            sb.append(" with application properties: ").append(applicationProperties);
        }
        
        System.out.println(sb.toString());
	}
}