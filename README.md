# .NET Framework WebAPI Base

* JWT Authentication
* Session
* Database Connection
* Mail Service
* Swagger
* API Route Filter
* Google Recaptcha
* HCaptcha
* Custom Data Annotations

## Services

* Logs - List / Add
* User - List / Login / Register / Add / Edit / Delete
* State - List
* City - List

## Setup settings `./api/Web.config`

| Name | Value | Description |
| ------ | ------ | ------ |
| connectionStrings | [Doc](https://docs.microsoft.com/pt-br/dotnet/api/system.configuration.configurationmanager.connectionstrings?view=netframework-4.5) | Set your database connection here |
| GoogleRecaptcha | boolean | Enable Google Recaptcha (Login / Register) |
| GoogleRecaptchaUrlApi | string | Google Recaptcha verify endpoint |
| GoogleRecaptchaTokenApi | string | Google Recaptcha back-end secret |
| HCaptcha | boolean | Enable HCaptcha (Login / Register) |
| HCaptchaUrlApi | string | HCaptcha verify endpoint |
| HCaptchaTokenApi | string | HCaptcha back-end secret |
| Mail | boolean | Enable Mail Service (Register) |
| MailServer | string | SMTP server host |
| MailPort | string | SMTP server port |
| MailUser | string | Email login |
| MailFrom | string | Email from |
| MailPass | string | Email password |
| JWTToken | string | Change your JWT token key here |
| HashSalt | string | Change your database password hash salt here |
| CorsHost | string | Change to enable cors host here |
| NotFoundPage | string | Set your "Not Found" redirect page here |
