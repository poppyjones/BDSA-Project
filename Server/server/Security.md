# Server
$serverApiClientId = "13d30085-420a-453d-83d8-50a87d6129d0"
$tenantId = "50efc57f-86fb-4e30-ba27-d5fca3eae2af"
$tenantDomain = "isabsahotmail.onmicrosoft.com"
$appIdUri = "api://13d30085-420a-453d-83d8-50a87d6129d0/API.Access"
$scope = "API.Access"

# Client
$redirectUri = "https://localhost:5111/authentication/login-callback"
$clientAppClientId = "e076a6ac-1fa1-4b9a-b47f-6e9ff3a2f2ee"


dotnet new blazorwasm --hosted --auth SingleOrg --api-client-id "13d30085-420a-453d-83d8-50a87d6129d0" --app-id-uri "api://13d30085-420a-453d-83d8-50a87d6129d0/API.Access" --client-id "e076a6ac-1fa1-4b9a-b47f-6e9ff3a2f2ee" --default-scope "API.Access" --domain "isabsahotmail.onmicrosoft.com" --output "ProjectBank" --tenant-id "50efc57f-86fb-4e30-ba27-d5fca3eae2af"