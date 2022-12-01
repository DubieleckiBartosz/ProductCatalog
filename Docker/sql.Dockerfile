FROM mcr.microsoft.com/mssql/server
 
USER root

ARG PROJECT_DIR=/tmp/mssql-scripts

RUN mkdir -p $PROJECT_DIR
WORKDIR $PROJECT_DIR
ENV SA_PASSWORD=hbiorsWE%^34wtfrde
ENV ACCEPT_EULA=Y
COPY ./Database/ProductCatalog-create-db.sql ./
COPY ./Database/ProductCatalog-create-tables.sql ./
COPY ./Database/ProductCatalog-create-storedProcedures.sql ./
COPY ./Database/ProductCatalog-fake-data.sql ./ 
COPY entrypoint.sh ./
COPY setupsql.sh ./

RUN chmod +x setupsql.sh
CMD ["/bin/bash", "entrypoint.sh"]