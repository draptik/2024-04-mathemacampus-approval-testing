---
theme: ./mathema-2023
defaults:
  layout: "default-with-footer"
occasion: "MATHEMA-Campus 2024"
occasionLogoUrl: "images/mathema-campus-logo.png"
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

# Mein Hintergrund - soweit relevant

- Bevor ich IT gemacht habe:
  - Biomechanik / Bionik
  - vertraut mit Fluid-Dynamik und Rheologie
- Seit ich IT mache:
  - Domain-Driven Design (DDD)
  - Test-Driven Development (TDD)
  - Functional Programming (FP)

---

# Domain-Kontext

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

# Tech-Stack(s)

- Main Stack: LAMP
  - Linux
  - Apache
  - MySQL
  - PHP
  - PLUS: Angular 1
- Other Stacks
  - C++
  - MATLAB

---

# Legacy Architecture

- FE: Angular
- BE: PHP
- Bonus: Weiteres externes System hat auch via BE & FE Daten abgerufen
  - Lassen wir erstmal außen vor

---

# Seam finden

- Was ist ein Seam? Working Effectively with Legacy Code
- PHP-Controller Anfrage nicht an PHP weiterleiten, sondern an eine neue Konsolenanwendung

---

# Code von PHP nach C# portieren

- sehr viel Tipparbeit
- sehr viel über Fließkommazahlen lernen
- Umstruktierung / Refactoring:
  - Ich war nicht einverstanden mit gewissen Entscheidungen im Altsystem (meiner Ansicht nach zuviel Vererbung, gefolgt von ganz viel if/else in abgeleiteten Klassen)
  - Jeder Produkttyp eine unabhängiger Typ ohne Vererbung (dafür viel Code-Duplizierung)
  - Stateless Konstrukte eingeführt
- hat 2-3 Monate gedauert
- in der Zeit "Blindflug"

---

# Portierten Code testen

- Vom Kunden hunderte realistische Beispiel-JSONs anfordern, bis die Code Coverage beim .NET Code bei fast 100% ist
- Durchs Alt-System jagen, Responses aufbewahren
- Durchs Neu-System jagen, Responses mit denen des Alt-Systems vergleichen

Wie geht das im Detail? 👉 Golden Master Testing
