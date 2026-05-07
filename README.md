# azure_dotnet

Azure cloud coding on .Net framework

ARM Deployment:-

Create Storage account:-

```
az deployment group create --resource-group rg-arm --template-file ./01_storage.json
```

Delete Storage account:-

```
az storage account delete --name stjayarm --resource-group rg-arm --yes
```
