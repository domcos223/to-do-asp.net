
#  Task Manager App Backend (ASP.NET Core)

Az alábbi dokumentáció bemutatja a Task Manager APP szerveroldalának beüzemelését, működését.
A frontend dokumentációja itt található (https://github.com/domcos223/to-do-reactjs#readme)

# Beüzemelés

Fejlesztői környezet: [Visual Studio 2022](https://visualstudio.microsoft.com/vs/#download) (.NET 6)

Adatbázis létrehozásához szükséges (MSSQL): 
- [SQL Server Management Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15)
- [SQL Server Express LocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver15)


Töltsük vagy klónozzuk le a repository-t melyben ez a dokumentáció megtalálható.
Ha megnyitottuk a fejlesztőkörnyezetben a mappát, az .sln fájlra kattintás után buildeljük a projektet 
és indítsuk el. 

appsettings.json fájlban látható a következő:
```ruby
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "TaskManagerContext": "Server=(localdb)\\mssqllocaldb;Database=TaskManagerDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```
Itt megadjuk a ConnectionStrings alatt, hogy milyen szerverhez csatlakozunk, és milyen néven hozzuk létre az adatbázist.
Ha ellenőrizni szeretnénk az adatokat, SQL Server Management Studio-ban Server Name: (localdb)\\mssqllocaldb és Windows autentikációval nézhetjük meg a létrejött táblákat.

## Felépítés

Szerveroldalon három névteret láthatunk, ezek a következők:

| Namespace             | Tartalom|
| ----------------- |-- |
| TaskManagerApi.Controllers  |ColumnController.cs, TodoController.cs|
| TaskManagerApi.Data | DbInitializer.cs, TaskManagerContext.cs|
| TaskManagerApi.Models  | Column.cs, Todo.cs|

### Todo.cs
```ruby
public class Todo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TodoId { get; set; }
        public int ColumnId { get; set; }

        [StringLength(20)]
        public string Title { get; set; }

        [StringLength(80)]
        public string Description { get; set; }

        public DateTimeOffset DueDate { get; set; }
        public int OrderId { get; set; }

        [JsonIgnore]
        public Column Column { get; set; }

    }
```

Egy Todo entitást definiál benne a következő propertykkel:
- TodoId: adatbázis által generált egyedi azonosító
- ColumnId: tároljuk, hogy a Task melyik Column-hoz tartozik
- Title: A Task címe
- Description: A Task leírása
- DueDate: Határidő
- OrderId: Column-ban tárolja a sorrendet
(illetve láthatunk egy navigation propertyt melyet [JsonIgnore] annotációval láttam el, a loop referencing elkerülése érdekében)

### Column.cs
```ruby
     public Column()
        {
            Todos = new HashSet<Todo>();   
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ColumnId { get; set; }
        [StringLength(20)]
        public string Title { get; set; }    
        public ICollection<Todo> Todos { get; set; }
    
```
- ColumnId : egyedi azonosító
- Title : a Column neve (pl. Todo,In Progress)
- Todos : a Column-hoz tartozó Todo-kat tároljuk (HashSet-ben)

### DbInitializer.cs

Alap és tesztadatok inicializására lett létrehozva, ezekkel az adatokkal "seedeljük" az adatbázist a szerveroldal elindításakor.
Itt adom meg a Column-ökhöz tartozó címeket, ezeket frontenden nem tudom manipulálni jelenleg.

### TaskManagerContext.cs

Adatbázis kontextusa
két DbSet mely megfelelteti az entitások gyűjteményét tábláknak.


## ColumnController.cs

#### Összes Column lekérése

```http
  GET /api/Column
```
List<Column> visszatérési érték, tartalmazza az összes Column-öt.

Csak erre az egy végpontra van szükségünk, mivel oszlopot nem fogunk módosítani, csak megjelenítésre szolgál. Include és a referencia miatt egyben tudjuk átadni a Columnokat és a hozzátartozó Todo-kat.

  
## TodoController.cs

#### Todo hozzáadása


```http
  POST /api/Todo
```
Új Todo hozzáadására átvesz egy új Todo objektumot, melyhez új Id-t generál, és az OrderId-t is létrehozza.

#### Todo szerkesztése
```http
  PUT /api/Todo/EditTodo
```

Todo módosítása átveszi a módosítható részeket paraméterben, ezeket értékül adja, majd felülírja és a változást menti az adatbázisba.


#### Todo mozgatása
```http
  PUT /api/Todo/MoveTodo
```
A Todo-k mozgathatók az oszlopok között a változásokat ezen végponton keresztül tudjuk regisztrálni az adatbázisba.
Átveszi paramtérekben a kiinduló oszlop idját, a cél oszlopét is, a Todo idját és annak Orderidját.

Változás során újra kell rendezni a kiinduló oszlopban maradó taskok sorrendjét, el kell távolítani a mozgatott Todot, és az új oszlopban is rendezni kell az értékeket beszúrás után.

#### Todo törlése
```http
  DELETE /api/Todo/{id}
```

Átveszi paraméterben a törölni kívánt Todo id-ját, ez alapján töröljük az adatbázisból, majd újrarendezzük a változásban érintett Column fennmaradó elemeit.


## Program.cs

```ruby
var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
       policy =>
       {
           policy.WithOrigins("http://localhost:3000")
               .AllowAnyMethod().AllowAnyHeader();
       });

});
```
itt megadjuk a hozzáférést, engedélyt, hogy külső erőforrásokat is tudjunk használni.
Szükséges a frontend-backend összekötéséhez.
A Backend a https://localhost:7202/ címen érhető el, ezt szükséges megadni a frontend részen, az adatok eléréséhez.





