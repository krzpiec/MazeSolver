Write-Output "building backend"
start powershell {cd ./Front/MazeFront/ ; ng serve}
docker-compose up
