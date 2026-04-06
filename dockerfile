FROM mcr.microsoft.com/dotnet/sdk:10.0

WORKDIR /sandbox

COPY runner.sh .
RUN chmod +x runner.sh

ENTRYPOINT ["./runner.sh"]