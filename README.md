Mars Plateau Rover Explorer

This project simulates robotic rovers exploring a rectangular plateau on Mars. Rovers receive commands from Earth to move across a grid while respecting plateau boundaries and avoiding collisions.

Problem

The plateau is divided into a grid. Each rover has a position defined by:

X coordinate

Y coordinate

Direction (N, E, S, W)

Example:

1 2 N

This means the rover is at position (1,2) facing North.

Commands

Rovers are controlled using a sequence of commands:

L – Turn left 90°

R – Turn right 90°

M – Move forward one grid point in the current direction

Example command sequence:

LMLMLMLMM

Input Example

5 5

1 2 N
LMLMLMLMM

3 3 E
MMRMMRMRRM

Expected Output

1 3 N
5 1 E

Architecture

The project follows a simple layered structure:

Domain – core entities

Application – command handling and rover control

Tests – unit tests validating rover behaviour

Design patterns used include the Command Pattern and Value Objects for coordinates.