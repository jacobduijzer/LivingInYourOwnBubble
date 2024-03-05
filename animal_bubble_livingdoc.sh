#!/bin/bash

dotnet tool update --global SpecFlow.Plus.LivingDoc.CLI

dotnet build CattleInformationSystem.Animals/CattleInformationSystem.Animals.sln 
dotnet test CattleInformationSystem.Animals/CattleInformationSystem.Animals.sln 

~/.dotnet/tools/livingdoc \
  feature-folder CattleInformationSystem.Animals/src/CattleInformationSystem.Animals.Specs \
  -t CattleInformationSystem.Animals/src/CattleInformationSystem.Animals.Specs/bin/Debug/net8.0/TestExecution.json \
  --binding-assemblies CattleInformationSystem.Animals/src/CattleInformationSystem.Animals.Specs/bin/Debug/net8.0/CattleInformationSystem.Animals.Specs.dll \
  --output livingdoc_animals_bubble.html

echo "Generated the report 'livingdoc_animals_bubble.html'."
