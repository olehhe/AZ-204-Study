# Get all child directories starting with 'AZ-204*'
$directories = Get-ChildItem -Directory -Filter "AZ-204*"

foreach ($dir in $directories) {
    # Construct the path to the 'tex' folder
    $texPath = Join-Path $dir.FullName 'tex'

    # Check if the 'tex' folder exists
    if (Test-Path $texPath) {
        # Find the .tex file in the 'tex' folder
        $texFile = Get-ChildItem -Path $texPath -Filter '*.tex' | Select-Object -First 1
        
        if ($texFile) {
            # Run pdflatex on the .tex file
            Write-Host "Running pdflatex on $($texFile.FullName)"
            Start-Process -FilePath 'pdflatex' -ArgumentList $texFile.FullName -Wait
        } else {
            Write-Host "No .tex file found in $texPath"
        }
    } else {
        Write-Host "'tex' folder does not exist in $($dir.FullName)"
    }
}
