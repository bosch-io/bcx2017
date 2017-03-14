FROM salmander/go-qpid-proton

ENV HOST=localhost
ENV PORT=15672
ENV USER=user
ENV PASS=pass
ENV TENANT=DEFAULT_TENANT

RUN go get -t -v github.com/pborman/uuid

WORKDIR /go
COPY *.go /go/

RUN go build .
RUN mv go bin/hono-receiver

CMD /go/bin/hono-receiver --host $HOST --port $PORT --user $USER --pass $PASS --tenant $TENANT
