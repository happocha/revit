# Revit Plugins

Minimal scaffold for a Revit API plugin (CreateCube).

## Build & Deploy (Windows VM)
1. Open folder in Visual Studio / Rider.
2. Build Release.
3. Run PowerShell: `./scripts/deploy.ps1 -RevitVersion 2026`.
4. Start Revit → Add-ins → CreateCube.

For CI, register a self-hosted runner on the VM and keep the provided workflow.
