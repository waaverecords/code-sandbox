FROM mcr.microsoft.com/dotnet/sdk:10.0

WORKDIR /code-sandbox

COPY execute-code.sh .
RUN chmod +x execute-code.sh

ENTRYPOINT ["./execute-code.sh"]