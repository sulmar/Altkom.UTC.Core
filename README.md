# Przykłady ze szkolenia .NET Core 2.2

## Polecenia z linii polecen

* Utworzenie nowej aplikacji konsolowej

~~~ bash
dotnet new console
~~~~

* Utworzenie nowej aplikacji webapi

~~~ bash
dotnet new webapi
~~~~

* Utworzenie nowej aplikacji MVC

~~~ bash
dotnet new mvc
~~~~

* Pobranie pakietów

~~~ bash
dotnet restore
~~~~


* Kompilacja

~~~ bash
dotnet run
~~~~


* Uruchomienie aplikacji

~~~
dotnet helloworld.dll
~~~


* Uruchomienie testów jednostkowych
~~~
dotnet test
~~~

* Dodanie pakietu 
~~~ bash
dotnet add package <nazwa>
~~~

## Publikacja aplikacji

* Windows
~~~ bash
dotnet publish -c Release -r win10-x64
~~~

* Linux
~~~ bash
dotnet publish -c Release -r linux-x64
~~~

* MacOS
~~~ bash
dotnet publish -c Release -r osx-x64
~~~


## REST API

| Akcja  | Opis                  |
|--------|-----------------------|
| GET    | Pobierz               |
| POST   | Utwórz                |
| PUT    | Zamień                |
| DELETE | Usuń                  |
| PATCH  | Zmodyfikuj częściowo  |
| HEAD   | Czy istnieje          |



### Włączenie obsługi XML

Plik Startup.cs

~~~ csharp
 public void ConfigureServices(IServiceCollection services)
 {
     services
         .AddMvc(options => options.RespectBrowserAcceptHeader = true)
         .AddXmlSerializerFormatters();
 }
~~~


### Wyłączenie generowania wartości null w jsonie

Plik Startup.cs

~~~ csharp

public void ConfigureServices(IServiceCollection services)
{
  services
    .AddJsonOptions(options =>
    {
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;            
    });
}
~~~

### Serializacja enum jako tekst 

Plik Startup.cs


~~~ csharp

public void ConfigureServices(IServiceCollection services)
{
  services
    .AddJsonOptions(options =>
     {
         options.SerializerSettings.Converters.Add(new StringEnumConverter(camelCaseText: true));                       
     });
}

~~~

### Zapobieganie cyklicznej serializacji

Plik Startup.cs

~~~ csharp
public void ConfigureServices(IServiceCollection services)
{
  services
      .AddJsonOptions(options =>
         {
             options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
         });
   } 
~~~

## Generowanie dokumentacji

### Instalacja

~~~ bash
dotnet add TodoApi.csproj package Swashbuckle.AspNetCore
~~~

### Konfiguracja

Plik Startup.cs

~~~ csharp
public void ConfigureServices(IServiceCollection services)
{
 services
      .AddSwaggerGen(c => c.SwaggerDoc("v1", new Info { Title = "My Api", Version = "1.0" }));         
} 
~~~

~~~ csharp
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
   app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
 }           
~~~


## Asynchroniczność

### Main asynchroniczny w C# 7.0

Program.cs

~~~ csharp

static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

        static async Task MainAsync(string[] args)
        {
                await DoWorkAsync();
         }
~~~

### Main asynchroniczny w C# 7.2

Project.csproj

~~~ xml

<PropertyGroup>
  <LangVersion>latest</LangVersion>
</PropertyGroup>
 
~~~

Program.cs

~~~ csharp
static async Task Main(string[] args)
 {
     await DoWorkAsync();
 }

~~~

## ngrok

Uruchomienie

``` bash
ngrok http 5000
```

Interfejs webowy

```
http://127.0.0.1:4040
```

API

```
http://127.0.0.1:4040/api
```


## Docker

### Hello World
``` bash
docker run hello-world
```

### Uruchomienie polecenia w kontenerze 
docker run ubuntu ls -l

### Uruchomienie basha w kontenerze

``` bash
docker run -it ubuntu bash
```

### Przydatne komendy
- ``` docker images ``` - lista wszystkich obrazów na twojej maszynie
- ``` docker pull <image> ``` - pobranie obrazu
- ``` docker run <image> ``` - uruchomienie obrazu (pobiera jeśli nie ma)
- ``` docker ps ``` - lista wszystkich uruchomionych kontenerów na twojej maszynie
- ``` docker ps -a``` - lista wszystkich przyłączonych ale nie uruchomionych kontenerów
- ``` docker start <containter_name> ``` - uruchomienie kontenera wg nazwy
- ``` docker stop <containter_name> ``` - zatrzymanie kontenera wg nazwy

### Konteneryzacja aplikacji .NET Core

* Utwórz plik Dockerfile

~~~
FROM microsoft/dotnet:2.0-sdk
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# copy and build everything else
COPY . ./
RUN dotnet publish -c Release -o out
ENTRYPOINT ["dotnet", "out/Hello.dll"]
~~~

## Entity Framework Core

### Instalacja

~~~ bash
dotnet add package Microsoft.EntityFrameworkCore
~~~

### Instalacja dostawcy bazy danych SQL Server
~~~ bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
~~~


### Przydatne komendy
- ``` dotnet ef ``` - weryfikacja instalacji
- ``` dotnet ef migrations add {migration} ``` - utworzenie migracji
- ``` dotnet ef migrations remove ``` - usunięcie ostatniej migracji
- ``` dotnet ef migrations list ``` - wyświetlenie listy wszystkich migracji
- ``` dotnet ef migrations script ``` - wygenerowanie skryptu do aktualizacji bazy danych do najnowszej wersji
- ``` dotnet ef database update ``` - aktualizacja bazy danych do najnowszej wersji
- ``` dotnet ef database update -verbose ``` - aktualizacja bazy danych do najnowszej wersji + wyświetlanie logu
- ``` dotnet ef database update {migration} ``` - aktualizacja bazy danych do podanej migracji
- ``` dotnet ef database drop ``` - usunięcie bazy danych
- ``` dotnet ef dbcontext info ``` - wyświetlenie informacji o DbContext (provider, nazwa bazy danych, źródło)
- ``` dotnet ef dbcontext list ``` - wyświetlenie listy DbContextów
- ``` dotnet ef dbcontext scaffold {connectionstring} Microsoft.EntityFrameworkCore.SqlServer -o Models ``` - wygenerowanie modelu na podstawie bazy danych

## Autentyfikacja

### Basic
Headers 

| Key   | Value  |
|---|---|
| Authorization | Basic {Base64(login:password)}  |

### Token
Headers 

| Key   | Value  |
|---|---|
| Authorization | Bearer {token}  |


### JWT

https://github.com/sulmar/dotnet-core-jwt


## Kondycja

### Instalacja

~~~ bash
 dotnet add package Microsoft.AspNetCore.Diagnostics.HealthChecks
~~~


### Konfiguracja

Startup.cs

~~~ csharp 
public void ConfigureServices(IServiceCollection services)
{
   services.AddHealthChecks();
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
 app.UseHealthChecks("/health");
}

~~~

### Dodanie własnej obsługi

RandomHealthCheck.cs

~~~ csharp

public class RandomHealthCheck  : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            if (DateTime.UtcNow.Minute % 2 == 0)
            {
                return Task.FromResult(HealthCheckResult.Healthy());
            }

            return Task.FromResult(HealthCheckResult.Unhealthy(description: "failed"));
        }
    }
~~~

Startup.cs

~~~ csharp

public void ConfigureServices(IServiceCollection services)
{
 services.AddHealthChecks()
             .AddCheck<RandomHealthCheck>("random");
}

~~~

### Dashboard

Instalacja

~~~ bash
dotnet add package AspNetCore.HealthChecks.UI
~~~
          

Startup.cs

~~~ csharp

public void ConfigureServices(IServiceCollection services)
{
   services.AddHealthChecksUI();
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    app.UseHealthChecks("/health",  new HealthCheckOptions()
      {
          Predicate = _ => true,
          ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
      });
}
~~~

appsettings.json

~~~ json

 "HealthChecks-UI": {
    "HealthChecks": [
      {
        "Name": "Http and UI on single project",
        "Uri": "http://localhost:5000/health"
      }
    ],
    "Webhooks": [],
    "EvaluationTimeOnSeconds": 10,
    "MinimumSecondsBetweenFailureNotifications": 60
  }
  
~~~

Wskazówka: Przejdź na http://localhost:5000/healthchecks-ui aby zobaczyc panel

### Kondycja SQL Server

~~~ bash
dotnet add package AspNetCore.HealthChecks.SqlServer 
~~~

Startup.cs

~~~ csharp

public void ConfigureServices(IServiceCollection services)
{
 services.AddHealthChecksUI()
    .AddSqlServer(Configuration.GetConnectionStrings("MyConnection");
}

~~~


### Kondycja DbContext

~~~ bash
dotnet add package Microsoft.Extensions.Diagnostics.HealthChecks.EntityFrameworkCore
~~~

Startup.cs

~~~ csharp

public void ConfigureServices(IServiceCollection services)
{
  services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyConnection"));
            
    services.AddHealthChecks()
            .AddDbContextCheck<AppDbContext>();

}

~~~




## Walidacja


