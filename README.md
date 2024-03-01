dotnet tool install --global dotnet-ef

dotnet ef migrations add MinMigration
dotnet ef database update

**Frågor och funderingar**

Entity Framework: Migrations / EnsureCreated()?

Hur många requests är ok att göra?
Det är ok att göra många requests.

DI och konstruktorer
**Model Binding** https://learn.microsoft.com/en-us/aspnet/core/mvc/models/model-binding
Se upp med privata properties!
Se upp med DI och AddScoped<T>()!
Serialisera/Deserialisera JSON eller annan data
(Vad menar jag med automatiskt?)