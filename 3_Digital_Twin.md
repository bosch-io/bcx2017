# What if I want to look at the whole device, not just messages? 

We're using the metaphor of a *Digital Twin* to express that for IoT assets, there's both a physical device, and a digital representation of its capabilities and aspects in the backend. We created a cloud service called [Bosch IoT Things](https://things.apps.bosch-iot-cloud.com/) that enables applications, cloud services, and devices to manage the data of their IoT assets in a simple, convenient, robust, and secure way. Solutions can store and update the data, properties, and relationships of your domain's assets and get notified of all relevant changes.

By the way - we proposed a new open source project called [Eclipse Ditto](https://projects.eclipse.org/proposals/eclipse-ditto) where we plan to open-source some of our digital twin technology. The proposal page has a good intro to this topic overall.

In order to work with Bosch IoT Things, you can either use a Java client, or the HTTP API. Let's look at the API:

## Getting started with the HTTP API

An IoT developer can create, read, update and delete Things via the Bosch IoT Things HTTP API. 

To use the HTTP API you will need:

 * The header for authenticating your solution: ``x-cr-api-token: {apiToken}``
 * The header for authenticating the current user via Basic Auth: ``Authorization: Basic base64({username}:{password})``

The root resource of the Bosch IoT Things HTTP API is located at ``https://things.apps.bosch-iot-cloud.com/cr/1``.
All requests and responses are ``JSON``-based so please use ``application/json`` as the ``Content-Type`` for your 
requests.

In below examples you have to replace _USER_ and _PASS_ with the concrete values.
### Search things
Let's see which Things your user is allowed to see. By default this request will not return more than 25 things.

> GET /search/things

```
curl -G -u "USER:PASS" --header "x-cr-api-token: PUT_TOKEN_HERE" https://things.apps.bosch-iot-cloud.com/cr/1/search/things
```

You can further filter or limit the returned results, see the 
[HTTP API documentation](https://things.apps.bosch-iot-cloud.com/documentation/rest/#) for more information.

### Retrieve a Thing
The response from the request above returned a JSON-document containing at most 25 things.
Each thing also contains its id in the returned JSON, e.g. _"thingId":"bcx:rrc.655997720"_.
We have created a dummy-thing you can play around with.
You can now query for the details of this dummy-thing.

> GET /things/{thingId}

```
curl -G -u "USER:PASS" --header "x-cr-api-token: PUT_TOKEN_HERE" https://things.apps.bosch-iot-cloud.com/cr/1/things/bcx:ThingsDummyDevice-0000
```

You can read only parts of a Thing by specifying the path inside the Thing via the URL path e.g. to read an 
attribute ``thingName`` use the following path:

> GET /things/{thingId}/attributes/thingName

```
curl -G -u "USER:PASS" --header "x-cr-api-token: PUT_TOKEN_HERE" https://things.apps.bosch-iot-cloud.com/cr/1/things/bcx:ThingsDummyDevice-0000/attributes/thingName
```

### Modify a Thing

You can either update the whole Thing at once (attention, this overwrites all data of a Thing) or only parts of it 
e.g. its attributes or a single property value.

To update a single property value for the attribute ``dummyAttribute`` of a Thing use the following request:
> PUT /things/{thingId}/attributes/dummyAttribue


```
curl -X PUT -u "USER:PASS" -H "Content-Type: application/json" --header "x-cr-api-token: PUT_TOKEN_HERE" -d '"TotallyNewDummyValue"' https://things.apps.bosch-iot-cloud.com/cr/1/things/bcx:ThingsDummyDevice-0000/attributes/dummyAttribute
```

### Further operations

For a complete list of available operations please refer to the 
[HTTP API documentation](https://things.apps.bosch-iot-cloud.com/documentation/rest/#).

In case you are familiar with [Postman](https://www.getpostman.com/) we also have prepared a 
[Postman Collection](Documentation/Postman)
for you to quickly get started (tip: use the environment ``env_BCX2017-prod-environment.json`` to have the correct 
endpoint configured automatically).

## Java API

If you prefer a Java based approach you can also use the 
[Things Integration Client](https://cr.apps.bosch-iot-cloud.com/dokuwiki/doku.php?id=005_dev_guide:005_java_api:005_java_api) 
to access your Things and to subscribe for changes that are made to your Things.
To get started go to the 
[Hello World Example](https://things.apps.bosch-iot-cloud.com/dokuwiki/doku.php?id=005_dev_guide:tutorial:000_hello_world)
of using the Java Client.

## Further information
* Main entry point for 
[Bosch IoT Things documentation](https://things.apps.bosch-iot-cloud.com/dokuwiki/doku.php?id=start)
* Complete documentation of [Things HTTP API](https://cr.apps.bosch-iot-cloud.com/documentation/rest/#)
* Documentation of the 
[Things Integration Client](https://cr.apps.bosch-iot-cloud.com/dokuwiki/doku.php?id=005_dev_guide:005_java_api:005_java_api)
* [Code examples](https://github.com/bsinno/iot-things-examples) of using the Things Integration Client (Java)

