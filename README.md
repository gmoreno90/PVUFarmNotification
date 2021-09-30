# BotPVU Farm Notification
The proposal of this bot is not need to open the website of Plant Vs undead in order to check the status of my farm. The email sent require email configuration using an SMTP server

### How its works?
- Download the last release zip file.
- Extract on any folder tha tyou want.
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
  "SmtpPassword": "YourEmailPassword"
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