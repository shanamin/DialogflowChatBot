# Dialogflow ES Chat Bot

Chat bot that interact with Google Dialogflow ES using websocket communication.

## Description

Sample Project for using end to end real time communication with Google Dialogflow API.  The server will handle client messages, sending them to Dialogflow ES through a WebSocket connection. It will then retrieve the Dialogflow ES response through a REST API and relay the response back to the client over the WebSocket
connection.

## Getting Started

### Dependencies

* Visual Studio 2022
* DOTNET 8 SDK (Software Development kit) For Development
* Google Cloud with "Dialogflow API" Enabled Account

### Installing

* Clone the repository
* After done with Visual studio and SDK insta create a Google cloud Project and Enable Dialogflow API
* After enabling create a service account give permissions and generate credentials file by going to keys -> Add key -> Create new Key
* Go to https://dialogflow.cloud.google.com/ on right side of Create Intent upload "flight booking.json" from "flight booking.zip"

### Executing program

* Build the project
* Run Visual studio on HTTPS

## Authors

Contributors names and contact info

ex. Shan Amin  
ex. (https://www.linkedin.com/in/shan-amin1/)

## Version History

* 0.1
    * Initial Release
