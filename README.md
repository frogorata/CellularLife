![C#](https://img.shields.io/badge/language-C%23-blue)
![.NET](https://img.shields.io/badge/.NET-Framework-purple)
![Status](https://img.shields.io/badge/status-educational-green)
# CellularLife

CellularLife is a C# implementation of Conway's Game of Life | a zero-player cellular automaton simulation based on mathematical rules.


This project simulates a cellular automaton where cells evolve based on simple mathematical rules.

---

##  About the Project

CellularLife is an educational project that demonstrates:

- Object-Oriented Programming in C#
- Grid-based simulation logic
- Cellular automaton rules
- Generation updates

Each cell can be either alive or dead, and the world evolves step by step according to Conway’s rules.

---

## ⚙️ How It Works

The simulation follows Conway's Game of Life rules:

1. Any live cell with fewer than two live neighbours dies (underpopulation).
2. Any live cell with two or three live neighbours lives on.
3. Any live cell with more than three live neighbours dies (overpopulation).
4. Any dead cell with exactly three live neighbours becomes a live cell (reproduction).

The grid updates generation by generation.

---

##  How to Run

### Option 1 — Using Visual Studio (Recommended)

1. Install **Visual Studio 2022**
2. Make sure `.NET Desktop Development` workload is installed
3. Open the `.sln` file
4. Press **F5** to run

---

### Option 2 — Using .NET CLI

Make sure .NET SDK is installed:

```bash
dotnet --version
