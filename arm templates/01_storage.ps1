New-AzResourceGroup -Name "rg-arm" -Location "uksouth" -Force
New-AzResourceGroupDeployment -ResourceGroupName "rg-arm" -TemplateFile "01_storage.json"  