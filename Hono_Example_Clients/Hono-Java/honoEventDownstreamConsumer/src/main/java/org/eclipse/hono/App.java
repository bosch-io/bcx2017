package org.eclipse.hono;

import java.util.concurrent.CountDownLatch;

import org.eclipse.hono.client.HonoClient;
import org.eclipse.hono.client.MessageConsumer;
import org.eclipse.hono.connection.ConnectionFactoryImpl;

import io.vertx.core.Future;
import io.vertx.core.Vertx;
import io.vertx.proton.ProtonClientOptions;

public class App {
    public static final String QPID_ROUTER_HOST = "hono.bosch-iot-suite.com";
    public static final short  QPID_ROUTER_PORT = 15671;

    public static final String TENANT_ID = "bcx";

    private final Vertx vertx = Vertx.vertx();
    private final HonoClient honoClient;

    private final CountDownLatch latch;

    // filter xdks: private TelemetryHandler telemetryHandler = new FilteringTelemetryHandler("xdk\\..+?");
    private EventHandler telemetryHandler = new EventHandler();
    
    public App(String[] args) {
	Credentials honoConsumerCreds = new Credentials();
        parseCmdline(honoConsumerCreds, args);

        honoClient = new HonoClient(vertx,
                ConnectionFactoryImpl.ConnectionFactoryBuilder.newBuilder()
                        .vertx(vertx)
                        .host(QPID_ROUTER_HOST)
                        .port(QPID_ROUTER_PORT)
                        .user(honoConsumerCreds.getUser())
                        .password(honoConsumerCreds.getPassword())
                        .trustStorePath("certs/trusted-certs.pem")
                        .disableHostnameVerification()
                        .build());
        latch = new CountDownLatch(1);
    }

    private void parseCmdline(Credentials honoConsumerCreds, String[] args) {
        for (int i = 0; i < args.length-1; i++) {
            if ("--user".equals(args[i])) {
                honoConsumerCreds.setUser(args[++i]);
            }
            else if ("--password".equals(args[i])) {
                honoConsumerCreds.setPassword(args[++i]);
            }
        }
        if (honoConsumerCreds.getUser() == null || honoConsumerCreds.getPassword() == null) {
            System.err.println("Usage: <app> --user <user> --password <password>");
            System.exit(-1);
        }
    }

    public static void main(String[] args) throws Exception {
        System.out.println("Starting downstream consumer...");
        App app = new App(args);
        app.consumeEventData();
        System.out.println("Finishing downstream consumer.");
    }

    protected void consumeEventData() throws Exception {
        final Future<MessageConsumer> consumerFuture = Future.future();

        consumerFuture.setHandler(result -> {
            if (!result.succeeded()) {
                System.err.println("honoClient could not create event consumer : " + result.cause());
            }
            latch.countDown();
        });

        final Future<HonoClient> connectionTracker = Future.future();

        honoClient.connect(new ProtonClientOptions(), connectionTracker.completer());

        connectionTracker.compose(honoClient -> {
                    honoClient. createEventConsumer(TENANT_ID,
                            msg -> telemetryHandler.handleMessage(msg), consumerFuture.completer());
                },
                consumerFuture);

        latch.await();

        if (consumerFuture.succeeded())
            System.in.read();
        vertx.close();
    }

    private class Credentials {
        String user;
        String password;

        public String getUser() {
            return user;
        }

        public void setUser(String user) {
            this.user = user;
        }

        public String getPassword() {
            return password;
        }

        public void setPassword(String password) {
            this.password = password;
        }
    }
}
