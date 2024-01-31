# Week3Lab
This lab starts at week 3 and will be ongoing for a few weeks.

## Week 3 Lab

In this Lab you will be Modifying the application to provide the following:
1) Add two buttons on the right side of the Active Todos list. One button will be labeled "Complete" and the other "Delete".
2) When the "Complete" button is clicked, the todo should be moved to the Completed Todos list.
3) When the "Delete" button is clicked, the todo should be removed from the list.
4) Add a button to the Completed Todos list labeled "Delete". When clicked, the todo should be removed from the list.
5) Add a button to the Completed Todos list labeled "Activate". When clicked, the todo should be moved back to the Active Todos list.

>  `Bonus:` Use the ImageButton control to display an image instead of text on the buttons.

## Week 4 Lab
Starting with the Week 3 Lab, you will be adding the following features:
1) Refactor the code behind MainPage.xaml.cs to use the MVVM pattern.
   * Create Commands in MainPageViewModel.cs to replace each of the buttons 'Click' event handlers.
      * CreateTodoCommand, CompleteTodoCommand, DeleteTodoCommand, DeleteCompletedTodoCommand, ActivateTodoCommand
      * Make sure each command has a CanExecute method that returns true or false depending on the following:
         * CreateTodoCommand can execute if there is some text in the Entry for the title (bind it to a field in the ViewModel).
         * CompleteTodoCommand and DeleteTodoCommand can execute only if there is a todo selected in the Active Todos list.
         * DeleteCompletedTodoCommand and ActivateTodoCommand can execute only if there is a todo selected in the Completed Todos list.
    
   > We will cover some of this in class.

> `Bonus:` Use the MVVM Toolkit `CommunityToolkit.Mvvm` to encapsulate properties and Commands.

## Getting Started
Find great icons and images at: https://pictogrammers.com/library/mdi/

## Suggestion using ImageButton (for Week 3)
![image](Example.png)