package org.eclipse.hono.examples;

import java.util.HashSet;
import java.util.Set;

import org.apache.qpid.proton.amqp.messaging.Data;
import org.apache.qpid.proton.amqp.messaging.Section;
import org.apache.qpid.proton.message.Message;
import org.eclipse.hono.EventHandler;
import org.eclipse.hono.util.MessageHelper;

/**
 * Only show new devices and count them
 */
public class WelcomingDeviceMessageHandler extends EventHandler {

	private final Set<String> foundDeviceIds = new HashSet<String>();

	@Override
	public void handleMessage(final Message msg) {
		final Section body = msg.getBody();

		if (!(body instanceof Data)) {
			return;
		}

		String deviceId = MessageHelper.getDeviceId(msg);

		// the only thing we are doing with the message is printing it to System.out
		welcomeDevice(deviceId);
	}

	private void welcomeDevice(String deviceId) {
		if (!foundDeviceIds.contains(deviceId)) {
			this.foundDeviceIds.add(deviceId);
			System.out.println("Welcoming new device [" + deviceId + "]. Device-Count now is " + foundDeviceIds.size());			
		}
	}
}