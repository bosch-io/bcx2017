package org.eclipse.hono.examples;

import org.apache.qpid.proton.amqp.messaging.Data;
import org.apache.qpid.proton.amqp.messaging.Section;
import org.apache.qpid.proton.message.Message;
import org.eclipse.hono.EventHandler;
import org.eclipse.hono.util.MessageHelper;

public class VerbosingDeviceIdsHandler extends EventHandler {

	@Override
	public void handleMessage(final Message msg) {
		final Section body = msg.getBody();

		if (!(body instanceof Data)) {
			return;
		}

		String deviceId = MessageHelper.getDeviceId(msg);

		System.out.println("Event-Data received from  [" + deviceId + "]");	
	}
}