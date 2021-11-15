# BotPVU Farm Notification
The proposal of this bot is not need to open the website of Plant Vs undead in order to check the status of my farm. The email sent require email configuration using an SMTP server

### How its works?
- Download the last release zip file.
- Extract on any folder that you want.
- Modify the file appsettings.json using the following steps
- Double click BotPVU.exe file

### Configuration
For configuration only need to open the appsettings.json file and fill the following information
```json
{
  "LoginTokenPVU": "XXXXXXXXXXXXXXX",
  "SendEmailNotification": true,
  "NotificationEmails": ["mail1@example.com"],
  "SmtpServer": "smtp.office365.com",
  "SmtpPort": 587,
  "SmtpServerSSL": true,
  "SmtpUserName": "example@mail.com",
  "SmtpPassword": "YourEmailPassword",
  "AutoFarming": false,
  "AutoFarmingDelay": 3000,
  "UserAgent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.61 Safari/537.36",
  "BackendEndpoint": "https://backend-farm.plantvsundead.com",
  "MyPlants": [ "1234", "1235" ]
}
```
### How get the Bearer Token?
- Open the website https://marketplace.plantvsundead.com/
- Go to Tab FARM
- Press F12 and go to tab Console
- Write: localStorage.getItem("token"); and press enter
- Copy the token into appsettings.json - LoginTokenPVU

### License
MIT
**Free Software, Hell Yeah!**

### Donations
This tool was developed and is being maintained by members of the PVU community, if you wish to make any contribution as a form of gratitude in order to help us keep this tool running, you can do it at the following address of the Binance Smart Chain (PVU, BNB, BUSD, ...) :
0x530103A4f6D864294A09D24f397CA1ea3cD895a2