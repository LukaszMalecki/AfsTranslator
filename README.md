# AfsTranslator

## General information
- The app is intended to translate English user input into leetspeak.  
- To achieve this end the app uses [external funtranslations api.](https://funtranslations.com/api/leetspeak)  
- The app also includes Api accessable via /Translator/Translate?text=<input_text_to_be_translated>  
- The app saves requests and responses in a local database.
- In its appsettings and appsettings.Development the app has options to:
  - Set on/off resetting database when server starts
  - Set on/off stubbing/mocking the external Api service (instead of sending actual requests the user will get a predefined response)
  - Set the api key
- The app contains a database table with viable translations 

## Limitations
- The free translation service is limited to 10 calls an hour (The paid api key can be inserted in appsettings)
- Input text can be max 100 characters long.
- Input text to be translated can't have whitespace characters besides spacebar.
- Input text can include alphanumericals and common special characters.
- Only translation variants listed in the database can be called. (default has only leetspeak)
