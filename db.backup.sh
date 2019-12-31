backupDate="$(date +'%Y-%m-%d_%H:%M:%S')"
echo backups/${backupDate}_kbs.sql
# -Fc if commpress
docker exec $(docker ps -q -f name=kbs_db) bash -c "pg_dump -d kbs -U postgres " > ./backups/${backupDate}kbs.sql