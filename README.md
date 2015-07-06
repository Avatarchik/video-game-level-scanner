# video-game-level-scanner
This project is a bachelor thesis project made by Jan Grzybowski and Anna Witwicka.

Abstract of the thesis:

"
  The aim of this project was to develop a system for designing virtual 3D models of the video game levels.
  The system consists of cubes (markers) painted in different colors, placed on specially prepared grid and an application which would detect the markers using a camera. 
  Project has been realized as a desktop application with additional assumption of being able to be ported to smartphone application.

  Markers of the same color placed next to each other correspond to the same room in the game level. 
  The scanner reads the position of the markers and using algorithm described in this thesis creates a matrix describing setting of room in the level. Matrix is used to display the 3D model of the level. The level’s data can be also saved to the file for later usage.
"

Responsibilities:
- Jan Grzybowski
    - room recognition
    - UI (Unity 5)
- Anna Witwicka
    - building the level
    - creating passages between the rooms
    - visiting the rooms

Created using Unity engine and EmguCV.
