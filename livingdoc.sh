#!/bin/bash

dotnet tool update --global SpecFlow.Plus.LivingDoc.CLI

dotnet build CattleInformationSystem/CattleInformationSystem.sln 
dotnet test CattleInformationSystem/CattleInformationSystem.sln 

~/.dotnet/tools/livingdoc \
  feature-folder CattleInformationSystem/src/CattleInformationSystem.Specs \
  -t CattleInformationSystem/src/CattleInformationSystem.Specs/bin/Debug/net7.0/TestExecution.json \
  --binding-assemblies CattleInformationSystem/src/CattleInformationSystem.Specs/bin/Debug/net7.0/CattleInformationSystem.Specs.dll \
  --output livingdoc.html

/usr/bin/firefox livingdoc.html
