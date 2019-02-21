## Budowa aplikacji Xamarin.Forms z wykorzystaniem języka F# i wzorca MVU

---

## Agenda

* Wprowadzenie do MVU
* Podstawowa struktura aplikacji
* Biblioteka Fabulous
* Demo

---?image=https://elmbridge.github.io/curriculum/images/elm-architecture-4.jpeg&position=center&size=45%

@snap[north]
## Elm architecture
@snapend

@snap[south]
<small>Źródło: https://elmbridge.github.io/curriculum/images/elm-architecture-4.jpeg</small>
@snapend

+++

## Biblioteka Elmish

@quote[`-ish` a suffix used to convey the sense of “having some characteristics of"]

@quote[Elmish implements core abstractions that can be used to build Fable applications following the “model view update” style of architecture, as made famous by Elm.]

<small>Źródło: https://elmish.github.io/elmish/</small>

---

## Biblioteka Fabulous

Elmish dla Xamarin.Forms

---

## Podstawowa struktura aplikacji

@ul
* Model
* Wiadomości
* Funkcja init
* Funkcja update
* Widok
* Połączenie całości
@ulend

+++?code=src/Sample.fs#lang=fsharp&color=#FBF1C7&title=Podstawowa struktura aplikacji
@[10-13](Model)
@[15-21](Wiadomości)
@[23-25](Funkcja init)
@[32-39](Funkcja update)
@[41-56](Widok)
@[58-65](Połączenie całości)

---

## Demo

* Prosta aplikacja
  * ```dotnet new -i Fabulous.Templates```
  * ```dotnet new fabulous-app -o FabulousDemo```
* Bardziej rozbudowana aplikacja: https://github.com/llysyganicz/NotesApp

---

## Linki

* Elmish: https://elmish.github.io/elmish/
* Fabulous: https://fsprojects.github.io/Fabulous/
* Awesome Fabulous: https://github.com/jimbobbennett/Awesome-Fabulous

---

## Dziękuję

### Pytania?