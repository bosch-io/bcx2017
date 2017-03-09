package org.eclipse.hono;

import io.vertx.core.Future;
import io.vertx.core.Vertx;
import io.vertx.proton.ProtonClientOptions;
import org.apache.qpid.proton.amqp.messaging.AmqpValue;
import org.apache.qpid.proton.amqp.messaging.Data;
import org.apache.qpid.proton.amqp.messaging.Section;
import org.apache.qpid.proton.message.Message;
import org.eclipse.hono.client.HonoClient;
import org.eclipse.hono.client.MessageConsumer;
import org.eclipse.hono.connection.ConnectionFactoryImpl;
import org.eclipse.hono.util.MessageHelper;

import java.util.concurrent.CountDownLatch;

public class App {
    public static final String QPID_ROUTER_HOST = "192.168.99.100";
    public static final short  QPID_ROUTER_PORT = 15671;

    public static final String TENANT_ID = "DEFAULT_TENANT";

    private final Vertx vertx = Vertx.vertx();
    private final HonoClient honoClient;

    private final CountDownLatch latch;

    public App() {
        honoClient = new HonoClient(vertx,
                ConnectionFactoryImpl.ConnectionFactoryBuilder.newBuilder()
                        .vertx(vertx)
                        .host(QPID_ROUTER_HOST)
                        .port(QPID_ROUTER_PORT)
                        .user("user1@HONO")
                        .password("pw")
                        .trustStorePath("certs/trusted-certs.pem")
                        .disableHostnameVerification()
                        .build());
        latch = new CountDownLatch(1);
    }

    public static void main(String[] args) throws Exception {
        System.out.println("Starting downstream consumer...");
        App app = new App();
        app.consumeTelemetryData();
        System.out.println("Finishing downstream consumer.");
    }

    private void consumeTelemetryData() throws Exception {
        final Future<MessageConsumer> consumerFuture = Future.future();

        consumerFuture.setHandler(result -> {
            if (!result.succeeded()) {
                System.err.println("honoClient could not create telemetry consumer : " + result.cause());
            }
            latch.countDown();
        });

        final Future<HonoClient> connectionTracker = Future.future();

        honoClient.connect(new ProtonClientOptions(), connectionTracker.completer());

        connectionTracker.compose(honoClient -> {
                    honoClient.createTelemetryConsumer(TENANT_ID,
                            msg -> handleTelemetryMessage(msg), consumerFuture.completer());
                },
                consumerFuture);

        latch.await();

        if (consumerFuture.succeeded())
            System.in.read();
        vertx.close();
    }

    private void handleTelemetryMessage(final Message msg) {
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
