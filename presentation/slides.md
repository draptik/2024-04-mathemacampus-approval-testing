---
theme: ./mathema-2023
defaults:
  layout: "default-with-footer"
occasion: "MATHEMA-Campus 2024"
occasionLogoUrl: "/images/mathema-campus-logo.svg"
company: "MATHEMA GmbH"
presenter: "Patrick Drechsler"
contact: "patrick.drechsler@mathema.de"
info: |
  ## TODO
canvasWidth: 980
layout: cover
transition: slide-left
mdc: true
download: true
---

# Revitalizing Legacy Code
# Approval Testing Unleashed

#### Erfahrungen aus der Legacy-App-Portierung und 
#### Einblicke in die Welt von Verify

Patrick Drechsler

---

# Legacy-Context: Domain

- Expertensystem
- User: Ingenieure im Vertrieb
- Besonderheiten:
  - SI-Einheiten
  - Fließkommazahlen
  - Mathematik
    - nicht nur Dreisatz
    - komplexe Formeln (inkl. Integralrechnung)
    - sehr viele Formeln (x * 10^3 LoC)
    - Mathematik ist Core-Domain!

---

# Legacy-Context: Tech-Stack(s)

- Main Stack: **LAMP**
  - **L**inux
  - **A**pache
  - **M**ySQL
  - **P**HP
  - ✨ Angular 1 (!) ✨
- Other Stacks
  - C++
  - MATLAB

---

# Legacy-Context: Architecture

- Frontend (FE): Angular 1
- Backend (BE): PHP
- Bonus: External system calls to FE & BE
  - Let's ignore this for now

---

# Find a "Seam"

- What is a Seam? 👉 M. Feathers "Working Effectively with Legacy Code"
- Example: Redirecting a php request to a new dotnet console application
  ```php
  // Seam which toggles between PHP and .NET
  if ($this->useDotNet) {
    // C# calculation (new)
    return $this->calcDotNet("calculate", $request);
  }
  else {
    // PHP calculation (legacy)
    return new CalcWithPhp($request);
  }
  ```
  ```php
  function calcDotNet($endpointName, Request $request)
  {
    // ...
    $encodedJson = base64_encode($request->getContent());
    $result = shell_exec("".$dotnetProgramm." ".$endpointName."  ".$encodedJson."");
    return $result;
  }
  ```

<style>
.slidev-code {
  font-size: 10px !important;
  line-height: 13px !important;
}
</style>

---

# Porting Code from PHP to C#

- a lot of typing (no AI usage)
- I learned a lot about floating point numbers
- Restructuring / Refactoring:
  - I disagreed with certain decisions in the old system (in my opinion too much inheritance, followed by a lot of if/else in derived classes)
  - 👉 I mapped each product type to an independent type without inheritance (a lot of code duplication)
  - 👉 I added "Stateless constructs": even more code duplication
- took 2-3 months
- no tests during that time

---

# Testing the Ported Code

- Get hundreds of realistic example JSONs from the customer
- Run through the old system, save responses
- Run through the new system, compare responses with saved responses
- Rinse and repeat until the code coverage of the new .NET code is close to 100%

How does that work in detail?

---

# Definitions

- Golden Master Test
- Approval Testing
- Verify
- Regression Test
- Acceptance Test
- Characterization Test (Martin Folwer)

We'll stick with "Approval Testing" and "Verify" for now.
And discuss the others later.

---

# Initial Workflow

- No existing `.verified.` file.

```mermaid
graph LR
run(Run test and<br/>create Received file)
failTest(Fail Test<br/>and show Diff)
closeDiff(Close Diff)
run-->failTest
shouldAccept{Accept ?}
failTest-->shouldAccept
accept(Move Received<br/>to Verified)
shouldAccept-- Yes -->accept
discard(Discard<br/>Received)
shouldAccept-- No -->discard
accept-->closeDiff
discard-->closeDiff
```

---

# Subsequent Workflow

- Existing `.verified.` file is compared with `.received.` file...

```mermaid
graph LR
run(Run test and<br/>create Received file)
closeDiff(Close Diff)
failTest(Fail Test<br/>and show Diff)
run-->isSame
shouldAccept{Accept ?}
failTest-->shouldAccept
accept(Move Received<br/>to Verified)
shouldAccept-- Yes -->accept
discard(Discard<br/>Received)
shouldAccept-- No -->discard

isSame{Compare<br/>Verified +<br/>Received}
passTest(Pass Test and<br/>discard Received)
isSame-- Same --> passTest
isSame-- Different --> failTest
accept-->closeDiff
discard-->closeDiff
```

---

# Demo

```csharp
public record Person(string FirstName, string LastName, int Age);
```

```csharp
// ⚠️ Fact must return Task!
[Fact]
public Task HelloWorldTest()
{
    var homer = new Person("Homer", "Simpson", 39);
    return Verify(homer);
}
```

Verified text file:

```json
{
  FirstName: Homer,
  LastName: Simpson,
  Age: 39
}
```

---

# Verify - Randomness

No problem 👉 "Scrubbers"

- GUIDs (by default)
- TimeStamps (by default)

---
layout: two-cols
---

- TODO: format code 

```csharp
public record Person(
    string FirstName,
    string LastName,
    int Age,
    Guid Id,              // 👈
    DateTime CreatedAt,   // 👈
    DateTime? UpdatedAt); // 👈
```

::right::

```csharp
[Fact]
public Task PersonTest()
{
    var now = DateTime.Now;
    var homer = new Person(
        "Homer",
        "Simpson",
        39,
        Guid.NewGuid(), // 👈
        now,            // 👈
        now);           // 👈

    return Verify(homer);
}
```

```json
{
  FirstName: Homer,
  LastName: Simpson,
  Age: 39,
  Id: Guid_1,            // 👈
  CreatedAt: DateTime_1, // 👈
  UpdatedAt: DateTime_1  // 👈
}
```

---

# Verify - Custom Scrubbers

https://github.com/VerifyTests/Verify/blob/main/docs/scrubbers.md

- Example when generating SVGs using Plotly.NET: Scrub all lines containing `#clip` followed by a word character
- `ScrubLinesWithReplace` and friends

```fsharp
// F#
let settings = VerifySettings ()
settings.ScrubLinesWithReplace (fun line ->
    System.Text.RegularExpressions.Regex.Replace(line, "#clip\w+", "#clipSCRUBBED"))
```
```csharp
// C# (unverified)
var settings = new VerifySettings();
settings.ScrubLinesWithReplace(line =>
    System.Text.RegularExpressions.Regex.Replace(line, "#clip\\w+", "#clipSCRUBBED"));
```

---

# Verify - Floating Point Numbers

- Floating point numbers are always a joy 😿
- Especially when working with different programming languages and platforms
- even dotnet will produce different results depending on the platform (Windows, Linux, macOS)

---

# Verify - Floating Point Numbers

Verify offers different strategies:

  - Custom rounding
    ```csharp
    VerifierSettings.AddExtraSettings(x => x.FloatPrecision = 8);
    ```
  - Custom tests for each platform (if above fails)
    ```csharp
    // ...
    settings.UniqueForOSPlatform()
    // ...
    ```
    Drawback:
    - works on Linux dev machine, CI pipeline, target platform
    - fails on Windows dev machine, until windows dev commits ☹️

---

# Verify - JSON/XML

- TODO: Add Example
- JSON/XML are parsed and compared with .NET standard libraries
- JSON/XML work out of the box

---

# Verify - Setup

- We can define the output folder
- We can define the file extension
- ...

---

# Verify - CI

- works out of the box
- No need to install anything on the CI server

---

# Verify - Diff-Tooling for Devs

- Visual Studio / Windows
- Rider
- Visual Studio Code
- 1st class support for all major IDEs

Very cool: customizable to your needs!

---

# Verify - F# Support

- Example: Plotly.NET (F#)
- works out of the box

---

# Verify - An Example

- TODO: An example showing many features of Verify (incl. web-api)

---

# Verify - For all the languages!

Similar libraries exist for most programming languages.

Overview: https://github.com/approvals

<div style="display: inline">
<img src="/images/lang_cpp.svg" />
<img src="/images/lang_csharp.svg" />
<img src="/images/lang_golang.svg" />
<img src="/images/lang_java.svg" />
<img src="/images/lang_javascript.svg" />
<img src="/images/lang_labview.svg" />
<img src="/images/lang_lua.svg" />
<img src="/images/lang_objective-c.svg" />
<img src="/images/lang_perl.svg" />
<img src="/images/lang_php.svg" />
<img src="/images/lang_python.svg" />
<img src="/images/lang_ruby.svg" />
<img src="/images/lang_swift.svg" />
</div>

<style>
img {
  width: 100px;
  height: 100px;
  margin-bottom: 10px;
  display: inline-block;
}
</style>

---

# Definitions - Revisited

- Synonyms:
  - ✅ Golden Master Test
  - ✅ Approval Test
  - ✅ Snapshot Test
  - ✅ Verification Test / Verify
- NOT a synonym for:
  - **Regression Test**
    - a test which is run to ensure that the code still works after a change
  - **Acceptance Test**
    - a test which is run to ensure that the code works as expected
  - **Characterization Test** (Martin Fowler)
    - a test which is run to understand the behavior of the code

---

# Summary

TODO: