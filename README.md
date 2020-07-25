# .NET Framework WebAPI Base

- JWT Authentication
- Session
- Database Connection
- Mail Service
- Swagger
- API Route Filter
- Google Recaptcha
- HCaptcha
- Custom Data Annotations

## Services

- Logs - List / Add
- User - List / Login / Register / Add / Edit / Delete
- State - List / Add / Edit / Delete
- City - List / List by State / Add / Edit / Delete

## Swagger

- [Documentation](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
- [Localhost URL](http://127.0.0.1:8080/swagger/ui/index)

## SQL Server

```console
user@host:~$ docker pull mcr.microsoft.com/mssql/server:2019-GA-ubuntu-16.04
user@host:~$ docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=123@456@789aA' -e 'ACCEPT_EULA=Y' -p 1433:1433 --name 'SQLServer' -v sqlvolume:/var/opt/mssql/data -d mcr.microsoft.com/mssql/server:2019-GA-ubuntu-16.04
```

## Setup settings `./api/Web.config`

| Name                    | Value                                                                                                                                          | Description                                  |
| ----------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------- | -------------------------------------------- |
| connectionStrings       | [Documentation](https://docs.microsoft.com/pt-br/dotnet/api/system.configuration.configurationmanager.connectionstrings?view=netframework-4.5) | Set your database connection here            |
| GoogleRecaptcha         | boolean                                                                                                                                        | Enable Google Recaptcha (Login / Register)   |
| GoogleRecaptchaUrlApi   | string                                                                                                                                         | Google Recaptcha verify endpoint             |
| GoogleRecaptchaTokenApi | string                                                                                                                                         | Google Recaptcha back-end secret             |
| HCaptcha                | boolean                                                                                                                                        | Enable HCaptcha (Login / Register)           |
| HCaptchaUrlApi          | string                                                                                                                                         | HCaptcha verify endpoint                     |
| HCaptchaTokenApi        | string                                                                                                                                         | HCaptcha back-end secret                     |
| Mail                    | boolean                                                                                                                                        | Enable Mail Service (Register)               |
| MailServer              | string                                                                                                                                         | SMTP server host                             |
| MailPort                | string                                                                                                                                         | SMTP server port                             |
| MailUser                | string                                                                                                                                         | Email login                                  |
| MailFrom                | string                                                                                                                                         | Email from                                   |
| MailPass                | string                                                                                                                                         | Email password                               |
| JWTToken                | string                                                                                                                                         | Change your JWT token key here               |
| HashSalt                | string                                                                                                                                         | Change your database password hash salt here |
| CorsHost                | string                                                                                                                                         | Change to enable cors host here              |
| NotFoundPage            | string                                                                                                                                         | Set your "Not Found" redirect page here      |
