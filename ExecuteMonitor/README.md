# ExecuteMonitor

The monitor checks if an execute still active. If the 
application is stops, then start the application again.

- [ ] The Monitor.json file works with arguments.
- [X] Sent a mail when the application has stopped.
- [ ] Update the last activity.

#### Monitor.json example
```javascript
[
  {
    "MonitorName": "WeatherDemon",
    "ExecutableName": "%OneDrive%\\Bin\\WeatherDemon.exe",
    "MailTo": "example",
    "MailCc": "example",
    "MailBcc": "example",
    "Checks": [
      {
        "CheckFileName": "%OneDrive%\\Data\\DailyWeather\\DayWeather.json",
        "MaximumTime": "00:40:00",
        "LastActivity": null
      }
    ]
  }
]
```

