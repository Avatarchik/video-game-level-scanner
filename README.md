#WAŻNE! Aby edytować projekt w Unity należy pobrać toola do instalowania Emgu dla Unity
W configu należy podać ścieżkę do folderu Emgu (folderu wersji) i ścieżkę do folderu Editor w folderze Unity.
Uruchaminie toola
UnityBuildTool.exe [install/project] {ścieżka do projektu} {x86,x64}
- opcja install doda niezbędne pliki w katalogu Unity (trzeba to zrobić tylko raz)
- opcja project przekopiuje wszystkie niezbędne pliki do katalogu projektu (trzeba to robić co projekt)
Ścieżka do projektu jest ścieżką do głównego folderu projektu, NIE folderu Assets!
Po tym wszystkim należy w ustawieniach player'a ustawić Api Compatibility level  na .NET 2.0 (bez Subset w nazwie).


# Create README.md file for Video Game Level Scanner

A README.md file is intended to quickly orient readers to what your project can do. [Learn more](https://daringfireball.net/projects/markdown/syntax) about Markdown.

## Get started with this page
 1. Edit the contents of this page
 2. Commit changes

## Use Visual Studio
 1. Connect with Team Explorer
 2. Clone repository
 3. Add a README.md file
 4. Commit and push changes

## Use command line
Use the following commands inside the folder of your repository:

 1. git add README.md
 2. git commit -m "Adding project documentation" 
 3. git push origin master
