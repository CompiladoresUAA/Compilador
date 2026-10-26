# Jucs= — IDE and Compiler from Scratch

A desktop IDE built with **WPF (.NET 6)** that wraps a hand-written compiler for a small C-like teaching language. The compiler front end and code generator are written in **Python**; the IDE launches them as child processes and displays the output of each phase.

Project developed for the Compilers course (UAA).

## Features

- Code editor based on [AvalonEdit](https://github.com/icsharpcode/AvalonEdit) with syntax highlighting, light/dark themes and adjustable font size.
- File management: open, save, save as, close.
- Compilation pipeline, with one view per phase:
  1. **Lexical analysis** — hand-written scanner (`scan.py`) producing the token list and lexical errors.
  2. **Syntax analysis** — recursive-descent parser (`parse.py`) producing the syntax tree and syntax errors.
  3. **Semantic analysis** — symbol table construction and type checking (`analyze.py`, `symtab.py`), shown as an annotated tree plus a hash-table view of the symbol table.
  4. **Intermediate code** — generation of Tiny Machine (TM) code (`cgen.py`, `code_.py`).
- Error panel and symbol table (hash table) panel.
- A Tiny Machine simulator (`TM.C`, compiled as `maquina.exe`) to run the generated code.

## Architecture

```
┌────────────────────────── IDE (C# / WPF) ──────────────────────────┐
│ MainWindow  ──►  runs `python scan.py <file>` / `python parse.py`  │
│ phases/*View ──► reads the result files and renders them           │
└───────────────┬────────────────────────────────────────────────────┘
                │ files in the working directory
┌───────────────▼────────────────────────────────────────────────────┐
│ Compiler (Python)                                                  │
│ scan.py ─► Archivo_Tokens.txt / Archivo_Tokens2.txt / Errores      │
│ parse.py ─► arbol.json, Errores_Sintaxis.txt                       │
│ analyze.py + symtab.py ─► tabHash.json, Archivo_TabSym.txt,        │
│                           Archivo_ErrorSem.txt                     │
│ cgen.py + code_.py ─► intermediateCode.tm ─► TM (maquina.exe)      │
└────────────────────────────────────────────────────────────────────┘
```

The IDE and the compiler communicate through text/JSON files rather than pipes: each phase writes its output to the working directory, and the corresponding view reads it back. The IDE uses the Strategy pattern (`phases/StrategyPattern`) to build the syntax and semantic tree views, and the Decorator pattern (`ErrorViews/DecoratorPattern`) for the error views.

## Repository layout

| Path | Description |
| --- | --- |
| `IDE/MainWindow.xaml(.cs)` | Main window: editor, menus, phase tabs, process control |
| `IDE/phases/` | Views for the lexical, syntax, semantic and hash-table phases, plus tree strategies |
| `IDE/ErrorViews/` | Error panel |
| `IDE/Common/` | Shared helpers and tab styles |
| `IDE/avalonfiles/` | Syntax-highlighting definitions (`keywords.xml` dark, `keywords2.xml` light) |
| `IDE/bin/Debug/net6.0-windows/*.py` | The Python compiler (scanner, parser, analyzer, code generator) |
| `IDE/bin/Debug/net6.0-windows/TM.C` | Tiny Machine simulator source |
| `pruebas*.txt`, `test.txt`, `t.txt` | Sample programs used to test the phases |

> Note: the Python compiler sources currently live in the IDE's `bin/Debug` output folder, because the IDE invokes them from its working directory.

## Requirements

- Windows (the IDE uses WPF and launches the compiler through `cmd.exe`)
- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) (or Visual Studio 2022)
- Python 3.10+ available on `PATH` as `python`
- Python package: `nltk` (used by the parser to build/draw trees)
- NuGet packages (restored automatically): `AvalonEdit 6.2.0.78`, `Newtonsoft.Json 13.0.3`
- Optional: a C compiler to build `TM.C` if you do not want to use the bundled `maquina.exe`

## Build and run

```bash
pip install nltk

cd IDE
dotnet build
dotnet run
```

Or open `IDE/IDE.sln` in Visual Studio 2022 and press **F5**.

Then:

1. **Archivo → Abrir** to load a source file (or type code in the editor and save it).
2. Click **Compilar**.
3. Use the phase menu (Léxico, Sintáctico, Semántico, Código Intermedio) and the bottom panel (Errores, Tabla Hash) to inspect the results.

To run the compiler without the IDE, from the folder containing the `.py` files:

```bash
python scan.py path/to/program.txt   # lexical analysis, writes Archivo_Tokens*.txt
python parse.py                      # parse, semantic check and code generation
```

## The language

C-like syntax with `main`, typed declarations, assignments, control flow and console I/O.

- **Types:** `int`, `real`, `boolean`
- **Keywords:** `main if then else end do while repeat until cin cout`
- **Operators:** `+ - * / %`, `< <= > >= = <>`, assignment `:=`, increment/decrement `++ --`
- **Delimiters:** `( ) { } , ;`
- **Comments:** `// line` and `/* block */`

Example:

```
main {
  int x, y;
  real a;
  x := 0;
  a := 3.24;
  do
    x := x + 5;
    cout x;
  until (x >= 100);
  while (y < 10) {
    y := y + 1;
  }
  if (x > 5) then
    y := x;
  else
    y := 0;
  end
}
```

## Output files

| File | Produced by | Content |
| --- | --- | --- |
| `Archivo_Tokens.txt`, `Archivo_Tokens2.txt` | scanner | Token listing for display / for the parser |
| `Archivo_Errores.txt` | scanner | Lexical errors |
| `Errores_Sintaxis.txt` | parser | Syntax errors |
| `arbol.json` | parser | Syntax tree for the tree view |
| `tabHash.json`, `Archivo_TabSym.txt` | analyzer | Symbol table |
| `Archivo_ErrorSem.txt` | analyzer | Semantic errors |
| `intermediateCode.tm` | code generator | Tiny Machine program |

## Known limitations

- Windows only; the Python scripts are launched via `cmd.exe` and paths use `\`.
- The theme loader references `../../../avalonfiles/*.xml` relative to the build output, so the IDE must be run from `bin/Debug/net6.0-windows`.
- The "Ayuda" and "Resultados" menu entries are placeholders.
- Compiled artifacts (`bin/`, `obj/`, `.vs/`, `__pycache__/`) are tracked in git; a `.gitignore` would be advisable.

## Authors

Compiladores UAA team — see the [GitHub organization](https://github.com/CompiladoresUAA).
