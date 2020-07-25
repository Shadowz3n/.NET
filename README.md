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

## Setup settings (./api/Web.config)

| Name | Description |
| ------ | ------ |
| connectionStrings | Set your database connection here |
| GoogleRecaptcha | [true/false] Enable Google Recaptcha (Login / Register) |
| GoogleRecaptchaUrlApi | Google Recaptcha verify endpoint |
| GoogleRecaptchaTokenApi | Google Recaptcha back-end secret |
| HCaptcha | [(]true/false] Enable HCaptcha (Login / Register) |
| HCaptchaUrlApi | HCaptcha verify endpoint |
| HCaptchaTokenApi | HCaptcha back-end secret |
| Mail | [true/false] Enable Mail Service (Register) |
| MailServer | SMTP server host |
| MailPort | SMTP server port |
| MailUser | Email login |
| MailFrom | Email from |
| MailPass | Email password |
| JWTToken | Change your JWT token key here |
| HashSalt | Change your database password hash salt here |
| CorsHost | Change to enable cors host here |
| NotFoundPage | Set your "Not Found" redirect page here |
