# TECHIN_sudormrf

Egzamino projektas

## Projekto paleidimas

#### 1. Docker konteinerio sukūrimo komanda

```
docker run --name App -e MYSQL_ROOT_PASSWORD=root -d -p 3306:3306 mysql:lts
```


#### 2. User Secrets (lokaliems nustatymams)

Po clone kitame kompiuteryje sukonfiguruokite slaptus nustatymus:

```
dotnet user-secrets init --project App.API/App.API.csproj
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=127.0.0.1;Port=3306;Database=App;Uid=root;Pwd=root;" --project App.API/App.API.csproj
dotnet user-secrets set "Jwt:Issuer" "App_auth" --project App.API/App.API.csproj
dotnet user-secrets set "Jwt:Audience" "App_api_audience" --project App.API/App.API.csproj
dotnet user-secrets set "Jwt:Key" "replace-with-your-own-secret" --project App.API/App.API.csproj
dotnet user-secrets list --project App.API/App.API.csproj
```

#### 3. Unit tests paleidimas
*\*leidžiama iš ./App.Services.Tests direktorijos*
```
dotnet test
```

### DB migracijos komandos

#### Migracijų atnaujinimas rankiniu būdu

```
dotnet ef database update
```

#### Migracijos pridėjimas

*\*leidžiama iš projekto root direktorijos*  
*\*Čia pavyzdys. Kuriant naują migraciją reikia pakeisti pavadinimą*
```
dotnet ef migrations add UpdateUserTable -p ./App.Data/ -s ./App.API/
```

### Swagger nuoroda

http://localhost:5141/swagger/index.html


