# TECHIN_sudormrf

Egzamino projektas

## Projekto paleidimas

#### 1. Paleisti docker konteinerį su MySql db serveriu
Docker konteinerio sukūrimo komanda

```
docker run --name App -e MYSQL_ROOT_PASSWORD=root -d -p 3306:3306 mysql:lts
```

#### 2. Pridėti User Secrets

```
dotnet user-secrets init --project App.API/App.API.csproj
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=127.0.0.1;Port=3306;Database=App;Uid=root;Pwd=root;" --project App.API/App.API.csproj
dotnet user-secrets set "Jwt:Issuer" "App_auth" --project App.API/App.API.csproj
dotnet user-secrets set "Jwt:Audience" "App_api_audience" --project App.API/App.API.csproj
dotnet user-secrets set "Jwt:Key" "replace-with-your-own-secret" --project App.API/App.API.csproj
dotnet user-secrets list --project App.API/App.API.csproj
```

#### 3. Paleisti programą
*\*leidžiama iš ./App.API direktorijos*
```
dotnet run
```

#### 4. UI prieinamas per swagger portalą

http://localhost:5141/swagger/index.html


#### 5. Unit tests paleidimas
*\*leidžiama iš ./App.Services.Tests direktorijos*
```
dotnet test
```

## API endpoints

### Auth

- `POST /api/Auth/login`

### User

- `POST /api/User/create-user`
- `GET /api/User/get-user/{id}`
- `POST /api/User/change-password`
- `POST /api/User/change-email`
- `DELETE /api/User/delete-user/{id}`

### UserTasks

- `POST /UserTasks/create-task`
- `GET /UserTasks`
- `GET /UserTasks/by-userid/{userId}`
- `GET /UserTasks/my-tasks/{taskId}`
- `GET /UserTasks/my-tasks/last`
- `GET /UserTasks/last/by-userid/{userId}`
- `GET /UserTasks/my-tasks`
- `PUT /UserTasks/{taskId}`
- `DELETE /UserTasks/last/by-userid/{userId}`
- `DELETE /UserTasks/{taskId}`
- `DELETE /UserTasks/my-tasks/{taskId}`

## Dev užrašai

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


