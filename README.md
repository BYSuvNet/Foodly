# Foodly 
## Enkelt Asp.Net Applikation 

* Vad är en komponent i Blazor?
    De byggblock vi använder för att skapa hemsidor.
* Hur skapar jag en komponent?
    Skapa en fil med filändelsen .razor. Filnamnet blir namnet på komponenten. Fyll komponenten med razor syntax och kod. Klart!
* Hur fungerar routing i blazor?
    Router-komponenten ser till så att rätt komponent hittas utifrån en viss URL
* Hur kan jag se till att en komponent blir en websida?
    To create a page in Blazor, create a component and add the @page Razor directive to specify the route for the component. 
    Ex: @page "/mypage"
* Hur kan jag med kod gå till en vissa sida?
    Använd NavigationManager! Exemplet:
        @page "/"
        @inject NavigationManager NavigationManager

        <button @onclick="Navigate">Navigate</button>

        @code {
            void Navigate() {
                NavigationManager.NavigateTo("counter");
            }
        }