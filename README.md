# Blog
## Web Api para Gerenciamento de Conteúdo

Esse projeto é uma forma de me forçar a estudar conceitos além do CRUD no ASP NET;

Alguns conceitos que pretendo implementar nessa API :

- JWT AUTHENTICATION
- AUTHORIZATION (ROLE BASED | RESOURCE BASED | POLICY BASED)
- MFA
- API VERSIONING
- HATEOS
- CACHE DISTRIBUÍDO COM REDIS 
- API GATEWAY 
    - REDIRECT REQUESTS | RATE LIMIT ETC...
- BACKGROUND JOBS (HANG FIRE OU QUARTZ)   
- INTEGRATION TESTS 
- CI / CD (GITHUB ACTIONS)
- DOCKER AND K8S

 Fique a vontade para deixar uma sugestão de estudo para que eu possa melhorar.


Em breve será feito a dockerização da aplicação com todos containers que estou usando:

- MailHoc Para envio de e-mail
- SQL SERVER para banco de dados 
- Redis para cache


Abaixo um exemplo de como configurar seu secret | appsettings.json



 ```
 {
   "ConnectionStrings": {
    "DefaultConnection": "Server=Host,Port;Database=DatabaseName;User Id=User;Password=YourPassword;TrustServerCertificate=True"
  },
  "Jwt": {
    "SecretKey": "YOUR SECRET KEY",
    "Issuer": "blog",
    "Audience": "http://blog.com"
  },
  "DefaultUsers": {
    "Admin": {
      "Name": "Admin",
      "Email": "YOUR E-MAIL",
      "Password": "YOUR PASSWORD",
      "Roles": [ "Admin", "Publisher", "User" ]
    },
    "Publisher": {
      "Name": "Publisher",
      "Email": "YOUR E-MAIL",
      "Password": "YOUR PASSWORD",
      "Roles": [ "Publisher" ]
    },
    "User": {
      "Name": "User",
      "Email": "YOUR E-MAIL",
      "Password": "YOUR PASSWORD",
      "Roles": [ "User" ]
    }
  }

}

 ```
