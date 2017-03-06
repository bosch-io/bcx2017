package main

// On macOS, you might need to set these:
// export DYLD_LIBRARY_PATH=/usr/local/lib
// #cgo LDFLAGS: -L/usr/local/lib

import (
	"fmt"
	"log"
	"os"

	"qpid.apache.org/amqp"
	"qpid.apache.org/electron"
	"flag"
	"github.com/pborman/uuid"
)

func init() {
}

var clientId string
const bufferSize uint = 100

func startReceiver(source string, connection electron.Connection) <- chan amqp.Message {
	out := make(chan amqp.Message, bufferSize)

	r, err := connection.Receiver(electron.Source(source),
		electron.Capacity(1), electron.RcvSettle(electron.RcvFirst), electron.SndSettle(electron.SndSettled))
	if err != nil {
		log.Fatalf("Unable to add receiver: %v", err)
		close(out)
		return out
	}

	go func() {
		log.Printf("[%s] Waiting for incoming messages\n", source)
		for {
			if rm, err := r.Receive(); err == nil {
				rm.Accept()
				out <- rm.Message
			} else if err == electron.Closed {
				close(out)
				return
			} else {
				log.Printf("[%s] Receive error: %v\n", source, err)
				close(out)
				return
			}
		}
	}()

	return out
}

func printMessages(telemetry <-chan amqp.Message, events <-chan amqp.Message) {
	for {
		select {
		case msg, more := <-telemetry:
			if !more {
				return
			}
			deviceId := msg.Annotations()["device_id"]
			payloadString := fmt.Sprintf("%+v", msg.Body())

			log.Printf("TELEMETRY [%s] %s", deviceId, payloadString)

		case msg, more := <-events:
			if !more {
				return
			}
			deviceId := msg.Annotations()["device_id"]
			payloadString := fmt.Sprintf("%+v", msg.Body())

			log.Printf("EVENT     [%s] %s", deviceId, payloadString)
		}
	}
}

func main() {
	flagHost := flag.String("host", "localhost", "Hono host name (default: localhost)")
	flagPort := flag.Uint("port", 15672, "Hono host name (default: 15672)")
	flagUsername := flag.String("user", "", "Hono user name (default: empty)")
	flagPassword := flag.String("pass", "", "Hono password (default: empty)")
	flagTenant := flag.String("tenant", "DEFAULT_TENANT", "Hono tenant (default: DEFAULT_TENANT)")

	flag.Parse()

	hostWithPort := fmt.Sprintf("%s:%v", *flagHost, *flagPort)
	clientId = fmt.Sprintf("hono-receiver[%v]", uuid.NewRandom().String())
	container := electron.NewContainer(clientId)

	log.Printf("Connecting to %s", hostWithPort)

	connection, err := container.Dial("tcp", hostWithPort,
		electron.User(*flagUsername),
		electron.Password([]byte(*flagPassword)),
		electron.SASLAllowInsecure(true))
	if err != nil {
		log.Fatalf("Unable to connect to Hono server: %v", err)
	}

	telemetry := startReceiver("telemetry/" + *flagTenant, connection)
	events := startReceiver("event/" + *flagTenant, connection)

	printMessages(telemetry, events)

	log.Println("Closing connection")

	connection.Close(nil)

}
