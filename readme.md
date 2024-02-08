# TACSV

Team Artemis Can Sat Viewer

## TACSV Details
### Functionality
- Home Page
    - Shows info like altitude, connection status, battery, database stats, maybe a cool 3d model
- Graphs Page
    - Graphs any data selected on the y axis against time of being recorded on the x axis.
    - Data is pulled straight from db
    - Graphs can be saved to be used in reports
- Data Page
    - Just a data viewer with a customisable query editor so we can debug our data
- Settings Page
    - Connection Settings
    - Theme?
    - Autorefresh Settings
- Amin
    - Amin
- Console
    - One tab allows commands (detailed below)
    - Another shows live stream of data from the ground unit
### Commands

- [ ] ping
- [X] help
- [X] cmdInfo
- [ ] battery
- [ ] send
- [ ] kill
- [ ] changefreq
- [ ] getgroundfreq
- [ ] getsatellitefreq

## Protocol
### Definitions
- Satellite - references the microcontroller + components being used in the actual CanSat
- Ground - references the microcontroller + components being used to receive the data
- TACSV – references the viewing software that stores the data and graphs it.

### Functionality
- Check both the ground and the satellites radios can talk to each other from TACSV
- Change both radio's frequency from TACSV
- Have data be sent from the Satelite to TACSV
- Get battery of the satellite from TACSV

### Message Format
1 character at the beginning meaning the id and then the rest of the id
#### Idea 1
Message has 2 characters at beginning noting the message id.
Each message type has a different id.

For Example:
- ID: 0 Satellite -> Ground Data message
- ID: 0 Ground -> TACSV Data Message
- ID: 1 TACSV -> Ground Battery Request
- ID: 1 Ground -> Satellite Battery Request
- ID: 1 Satellite -> Ground Battery Data
- ID: 1 Ground -> TACSV Battery Data

#### Idea 2
Message has 2 characters at beginning noting the message id.
Each message type and message direction has a different id.
For Example:
- ID: 0 Satellite -> Ground Data message
- ID: 1 Ground -> TACSV Data Message
- ID: 2 TACSV -> Ground Battery Request
- ID: 3 Ground -> Satellite Battery Request
- ID: 4 Satellite -> Ground Battery Data
- ID: 5 Ground -> TACSV Battery Data

These id's are seperate as i feel like it will make debugging easier.

#### Idea 3
Message has 4 characters at the beginning noting what device the message has been sent from and which device it will be sent to

01 - Satellite -> Ground
02 - Ground -> Satellite
03 - Ground -> TACSV
04 - TACSV -> Ground

Next 2 characters are the message id. 
This means we can have less id's while visually differentiating the devices sending the message.
### Need to Check
- When being sent data, will it have time? If yes how will you get time?