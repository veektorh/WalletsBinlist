# Wallet Binlist Project

This repo contains 4 projects 
- IdentityServer
- BinlistApi Wrapper 
- RabbitMqConsumer 
- DemoApp 

<br/><br/>

## Prerequisite

> A rabbit mq server must be running using the following configuration
  ```
  - Hostname : localhost
	- Port : 5672
	- UserName : guest
	- Password : guest
  
  ```
<br/><br/>

### 1) IdentityServer

- IdentityServer4
- Asp net core 3.1

This project consists of one client (BinlistApi Wrapper) protected using client credential authentication flow

<br/><br/>

### 2) RabbitMqConsumer

- .Net core 3.1 Console App

Console app that listens to rabbitmq messages published from the BinlistApi wrapper application whenever a new bin information is requested

<br/><br/>


### 3) BinlistApi Wrapper

- Asp net core 2.2 web api

Api wrapper for binlist.net api. this endpoint is Authorized and can only been accessed using a token gotten from identity server

Consists of the RabbitMq setup, declares queue and publish messages for every bin lookup 

    | URI                                                     | HTTP Method | Description                                      |
    | ------------------------------------------------------- | ----------- | -----------------------------------------        |
    | binlist/{bin}                                           | `GET`       | Fetch card data for the requested bin            |

<br/><br/>

### 4) DemoApp
- Asp net core 2.2 web api
- Entity Framework
- Sqlite Database


  - ### DemoApp
    A net core project that connects to the binlistapi wrapper api, after requesting token through identityServer. This api consists of two endpoints.
    
    - > Uses Nlog for logging
    - > Uses Microsoft In memory cache for caching
    
    | URI                                                     | HTTP Method | Description                                      |
    | ------------------------------------------------------- | ----------- | -----------------------------------------        |
    | binlist/{bin}                                           | `GET`       | Fetch card data for the requested bin            |
    | binlist/count                                           | `GET`       | Returns total number of hits for every card bin  |

  
  - ### DemoAppTest
    Contains unit test for the DemoApp project. It uses MsTest Framework







### HTTP Response Codes

Each response will be returned with one of the following HTTP status codes:

- `200` `OK` The request was successful
- `400` `Bad Request` There was a problem with the request (malformed)
- `404` `Not Found` An attempt was made to access a resource that does not exist in the API
- `500` `Server Error` An error on the server occurred

