# Budowa aplikacji Xamarin.Forms z wykorzystaniem języka F# i wzorca MVU

Łukasz Łysyganicz
e-mail: llysyganicz@pgs-soft.com

---

# Agenda

* Wprowadzenie do MVU
* Podstawowa struktura aplikacji
* Biblioteka Fabulous
* Demo

---

# Elm architecture

![Elm architecture](https://elmbridge.github.io/curriculum/images/elm-architecture-4.jpeg)

Źródło: https://elmbridge.github.io/curriculum/images/elm-architecture-4.jpeg

+++

# Biblioteka Elmish

> `-ish` a suffix used to convey the sense of “having some characteristics of"

> Elmish implements core abstractions that can be used to build Fable applications following the “model view update” style of architecture, as made famous by Elm.

Źródło: https://elmish.github.io/elmish/

---

# Biblioteka Fabulous

Elmish dla Xamarin.Forms

---

# Podstawowa struktura aplikacji

* Model
* Wiadomości
* Funkcja init
* Funkcja update
* Widok
* Połączenie całości

+++

# Model

```
type Model = 
  { Count : int
    Step : int
    TimerOn: bool }
```

+++

# Wiadomości

```
type Msg = 
  | Increment 
  | Decrement 
  | Reset
  | SetStep of int
  | TimerToggled of bool
  | TimedTick
```

+++

# Funkcja `init`

```
let initModel = { Count = 0; Step = 1; TimerOn = false }

let init () = initModel, Cmd.none
```

+++

# Funkcja `update`

```
let update msg model =
match msg with
  | Increment -> { model with Count = model.Count + model.Step }, Cmd.none
  | Decrement -> { model with Count = model.Count - model.Step }, Cmd.none
  | Reset -> init ()
  | SetStep n -> { model with Step = n }, Cmd.none
  | TimerToggled on -> { model with TimerOn = on }, (if on then timerCmd else Cmd.none)
  | TimedTick -> 
    if model.TimerOn then 
      { model with Count = model.Count + model.Step }, timerCmd
    else 
      model, Cmd.none
```

+++

# Widok

```
let view (model: Model) dispatch =
  View.ContentPage(
    content = View.StackLayout(padding = 20.0, verticalOptions = LayoutOptions.Center,
      children = [ 
        View.Label(text = sprintf "%d" model.Count, horizontalOptions = LayoutOptions.Center, widthRequest=200.0, horizontalTextAlignment=TextAlignment.Center)
        View.Button(text = "Increment", command = (fun () -> dispatch Increment), horizontalOptions = LayoutOptions.Center)
        View.Button(text = "Decrement", command = (fun () -> dispatch Decrement), horizontalOptions = LayoutOptions.Center)
        View.Label(text = "Timer", horizontalOptions = LayoutOptions.Center)
        View.Switch(isToggled = model.TimerOn, toggled = (fun on -> dispatch (TimerToggled on.Value)), horizontalOptions = LayoutOptions.Center)
        View.Slider(minimumMaximum = (0.0, 10.0), value = double model.Step, valueChanged = (fun args -> dispatch (SetStep (int (args.NewValue + 0.5)))),
          horizontalOptions = LayoutOptions.FillAndExpand)
        View.Label(text = sprintf "Step size: %d" model.Step, horizontalOptions = LayoutOptions.Center) 
        View.Button(text = "Reset", horizontalOptions = LayoutOptions.Center, command = (fun () -> dispatch Reset), canExecute = (model <> initModel))
      ]))
```

+++

# Połączenie całości

```
type App () as app = 
  inherit Application ()
					
  let runner =
    Program.mkProgram init update view
    |> Program.withConsoleTrace
    |> Program.runWithDynamicView app
```

---

# Demo

* Prosta aplikacja
  * ```dotnet new -i Fabulous.Templates```
  * ```dotnet new fabulous-app -o FabulousDemo```
* Bardziej rozbudowana aplikacja: https://github.com/llysyganicz/NotesApp

---

# Linki

* Elmish: https://elmish.github.io/elmish/
* Fabulous: https://fsprojects.github.io/Fabulous/
* Awesome Fabulous: https://github.com/jimbobbennett/Awesome-Fabulous

---

## Dziękuję

### Pytania?