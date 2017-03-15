package org.eclipse.hono.examples;

import java.util.Map;
import java.util.regex.Pattern;

import org.eclipse.hono.EventHandler;

/**
 * Filtering telemetry-data over the deviceId
 */
public class FilteringHandler extends EventHandler {

	private final Pattern matchDeviceId;

	public FilteringHandler(String deviceIdRegex) {
		this.matchDeviceId = Pattern.compile(deviceIdRegex);
		System.out.println("Filtering devices with " + matchDeviceId);
	}

	@Override
	protected void handleDevice(String deviceId, String contentType, String content, Map applicationProperties) {
		if (matchDeviceId.matcher(deviceId).matches()) {
			handleFilteredDevice(deviceId, contentType, content, applicationProperties);
		}
	}

	protected void handleFilteredDevice(String deviceId, String contentType, String content,
			Map applicationProperties) {
		printFullMessage(deviceId, contentType, content, applicationProperties);
	}
}