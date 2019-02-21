// Copyright 2018 Fabulous contributors. See LICENSE.md for license.
namespace CounterApp

open System.Diagnostics
open Fabulous.Core
open Fabulous.DynamicViews
open Xamarin.Forms

module App = 
    type Model = 
      { Count : int 
        Step : int
        TimerOn: bool }

    type Msg = 
        | Increment 
        | Decrement 
        | Reset
        | SetStep of int
        | TimerToggled of bool
        | TimedTick

    let initModel () = { Count = 0; Step = 1; TimerOn=false }

    let init () = initModel () , Cmd.none

    let timerCmd () = 
        async { do! Async.Sleep 200
                return TimedTick }
        |> Cmd.ofAsyncMsg

    let update msg model =
        match msg with
        | Increment -> { model with Count = model.Count + model.Step }, Cmd.none
        | Decrement -> { model with Count = model.Count - model.Step }, Cmd.none
        | Reset -> init ()
        | SetStep n -> { model with Step = n }, Cmd.none
        | TimerToggled on -> { model with TimerOn = on }, (if on then timerCmd() else Cmd.none)
        | TimedTick -> if model.TimerOn then { model with Count = model.Count + model.Step }, timerCmd() else model, Cmd.none 

    let view (model: Model) dispatch =  
        View.ContentPage(
          content=View.StackLayout(padding=30.0,verticalOptions = LayoutOptions.Center,
            children=[ 
              // If you want the button to disappear when in the initial condition then use this:
              yield View.Label(text = sprintf "%d" model.Count, horizontalOptions = LayoutOptions.Center, widthRequest=200.0, horizontalTextAlignment=TextAlignment.Center)
              yield View.Button(text="Increment", command= (fun () -> dispatch Increment))
              yield View.Button(text="Decrement", command= (fun () -> dispatch Decrement)) 
              yield View.StackLayout(padding=20.0, orientation=StackOrientation.Horizontal, horizontalOptions=LayoutOptions.Center,
                              children = [ View.Label(text="Timer")
                                           View.Switch(isToggled=model.TimerOn, toggled=(fun on -> dispatch (TimerToggled on.Value))) ])
              yield View.Slider(minimumMaximum=(0.0, 10.0), value= double model.Step, valueChanged=(fun args -> dispatch (SetStep (int (args.NewValue + 0.5)))))
              yield View.Label(text=sprintf "Step size: %d" model.Step, horizontalOptions=LayoutOptions.Center) 
              //if model <> initModel () then 
              yield View.Button(text="Reset", horizontalOptions=LayoutOptions.Center, command=(fun () -> dispatch Reset), canExecute = (model <> initModel () ))
            ]))   
             
    let program = 
        Program.mkProgram init update view 
        |> Program.withConsoleTrace

type CounterApp () as app = 
    inherit Application ()

    let runner = App.program |> Program.runWithDynamicView app