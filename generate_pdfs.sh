#!/bin/bash

# Find directories starting with AZ-204*
for dir in AZ-204*/; do
  # Check if the directory exists and has a 'tex' subfolder
  texPath="${dir}tex"
  if [ -d "$texPath" ]; then
    # Find the first .tex file in the 'tex' folder
    texFile=$(find "$texPath" -maxdepth 1 -type f -name "*.tex" | head -n 1)
    
    if [ -n "$texFile" ]; then
      # Run pdflatex on the .tex file
      echo "Running pdflatex on $texFile"
      pdflatex "$texFile"
    else
      echo "No .tex file found in $texPath"
    fi
  else
    echo "'tex' folder does not exist in $dir"
  fi
done
