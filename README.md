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

