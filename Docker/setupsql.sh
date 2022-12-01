SCRIPTS[0]="/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P hbiorsWE%^34wtfrde -d master -i ProductCatalog-create-db.sql"
SCRIPTS[1]="/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P hbiorsWE%^34wtfrde -d ProductCatalogME -i ProductCatalog-create-tables.sql"
SCRIPTS[2]="/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P hbiorsWE%^34wtfrde -d ProductCatalogME -i ProductCatalog-create-storedProcedures.sql"
SCRIPTS[3]="/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P hbiorsWE%^34wtfrde -d ProductCatalogMETests -i ProductCatalog-create-tables.sql"
SCRIPTS[4]="/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P hbiorsWE%^34wtfrde -d ProductCatalogMETests -i ProductCatalog-create-storedProcedures.sql"
SCRIPTS[5]="/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P hbiorsWE%^34wtfrde -d ProductCatalogMETests -i ProductCatalog-fake-data.sql" 

for ((i = 0; i < ${#SCRIPTS[@]}; i++))
do   
    echo "start"
    for x in {1..30};
    do
        ${SCRIPTS[$i]};  
        if [ $? -eq 0 ]
        then
            echo "Operation number ${i} completed"
            break
        else
            echo "Operation number ${i} not ready yet..."
            sleep 1
        fi
    done 
done 