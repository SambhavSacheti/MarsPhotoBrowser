# Mars Photo Browser - [Live Demo](https://calm-wave-02550eb10.azurestaticapps.net/)
![Azure Static Web Apps CI/CD](https://github.com/SambhavSacheti/MarsPhotoBrowser/workflows/Azure%20Static%20Web%20Apps%20CI/CD/badge.svg) 

A Blazor WebAssembly client application to browse photos taken by Curiosity Mars rover.  

![](https://github.com/SambhavSacheti/MarsPhotoBrowser/blob/master/Docs/MarsPhotoBrowser1.png)

## Introduction

 Are you interesed in Mars? Do you know that the `Curiosty Mars rover` is still taking pictures of the martian surface. The NASA Open Data team publish [Mars Rover API](https://api.nasa.gov/). The blazor application show martian pictures bu consuming the api.
 
 ## Features
 
 * Browse by [Martian Sol](https://en.wikipedia.org/wiki/Sol_(day_on_Mars)) - A day on Mars
 * Browse by `Earth Date`
 
 Please Note: On any given day if the rover has taken more than 25 photos then use the navigation at the bottom of the page to switch page. Also, the rover takes multiple pictures in the same setting and causes a illusion that duplicate pictures are shown.
 
 
## Application Logic
 
* Read Nasa Url and API Key from the appsettings.Json configuration file.
* Load Mission Manifest for rovers by calling the NASA API.
  * Currrently only `Curiosity` is supported. We can easily load pictures from `Opportunity`, `Spirit` or future rovers.
* Setup the Calendar - disable dates when the rover didn't take any pictures.
* Load latest photos taken by the rover.

The Live Demo is running on [Azure Static Web App](https://azure.microsoft.com/en-us/services/app-service/static/).

## Technologies and articles
* [VS Code](https://code.visualstudio.com/) 
* [Blazor Webassembly](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
* [Radzen Blazor Components](https://blazor.radzen.com/)
* [Chris Sainty's article on Fragment Routing with Blazor](https://chrissainty.com/fragment-routing-with-blazor/)
* [Azure Static Web Apps with .NET and Blazor](https://devblogs.microsoft.com/aspnet/azure-static-web-apps-with-blazor/)
