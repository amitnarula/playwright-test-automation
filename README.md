# Playwright Automation Framework Variants

This repository holds variants of a reusable Playwright automation framework implemented in different languages.

## Current Variant

- `dotnet/` — Playwright automation framework implemented with **.NET Core**, **NUnit**, and **Playwright for .NET**.

## Goal

The idea is to create multiple language-specific variants of the same automation framework concept, such as:

- .NET / C#
- Java
- JavaScript / TypeScript
- Python

Each variant should follow a similar structure and design principles, including:

- Page Object Model (POM)
- Centralized configuration
- Test setup and teardown
- Reporting and logging
- Browser configuration for Chromium, Firefox, and WebKit

## Getting Started

For the existing `.NET` variant, open the `dotnet/README.md` file:

```bash
cd dotnet
code README.md
```

Or view it directly in this repository:

- `dotnet/README.md`

## Contribution

Add new language variants in their own top-level folders, for example:

- `java/`
- `javascript/`
- `python/`

Keep each variant self-contained with its own README and instructions.

## Notes

This root README is intentionally minimal and meant to guide repository visitors to the available framework variants.
