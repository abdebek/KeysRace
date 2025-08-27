# EF Core and Dotnet Commands Related Notes

## Add migrations

```cmd
dotnet ef migrations add InitialCreate --project src/Infrastructure/KeysRace.Infrastructure --startup-project src/Presentation/KeysRace.Api
```

## Apply the Migration

```cmd
dotnet ef database update --startup-project src/Presentation/KeysRace.Api
```
