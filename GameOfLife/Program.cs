// See https://aka.ms/new-console-template for more information
using GameOfLife;
//using static System.Console;
//*** vzor "Rabbit" ***
/*  Každá živá buňka s méně než dvěma živými sousedy zemře.
    Každá živá buňka se dvěma nebo třemi živými sousedy zůstává žít.
    Každá živá buňka s více než třemi živými sousedy zemře.
    Každá mrtvá buňka s právě třemi živými sousedy oživne.*/

Console.WriteLine("Spustit souvisle/po klávese => \"y\"/\"kterákoliv klávesa\"");
bool souvisle = (Console.ReadLine().Trim().ToLower() == "y")? true : false;

Hra hra = new Hra(souvisle);
hra.Smycka();

Console.WriteLine("Zmáčkni klávesu pro konec hry");
Console.ReadKey();
