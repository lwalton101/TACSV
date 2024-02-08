# TACSV

Team Artemis Can Sat Viewer

## TACSV Details
### Functionality
- Home Page
    - Shows info like altitude, connection status, battery, database stats(??), maybe a cool 3d model, can awake status 
- Settings Page
    - Connection Settings
    - Theme?
    - Autorefresh Settings
- Amin(need to hide this better)
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

### Message Structure
    - 1.) Component ID (Example: S)
    - 2.) Reading Index, What reading is coming back from the accelerometer.
    - 3.) Semicolon Break
    - 4.) The actual reading(Hexadecimal)
    - 5.) Semicolon Break

    Examples:
        - S0;FFFFFF;

### Packets needed
    - 1.) CCS 811 -> CO2, TVOC
    - 2.) BME280 -> Temp, Pressure, Humidity
    - 3.) BMP280 -> Temp, Pressure, Humidity
    - 4.) LTR390 -> UV, ALS
    - 5.) LSM303AGR -> X, Y, Z, Acc
    - 6.) Time since initalised(millis)
    - 7.) Battery levels

## Questions
- Can we just use time on the ground? Also what did we agree on time i should really write things down.
