#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Motorola.CodingExercise.Service/Motorola.CodingExercise.Service.csproj", "Motorola.CodingExercise.Service/"]
COPY ["Motoroal.CodingExercise.Repository/Motoroal.CodingExercise.Repository.csproj", "Motoroal.CodingExercise.Repository/"]
RUN dotnet restore "Motorola.CodingExercise.Service/Motorola.CodingExercise.Service.csproj"
COPY . .
WORKDIR "/src/Motorola.CodingExercise.Service"
RUN dotnet build "Motorola.CodingExercise.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Motorola.CodingExercise.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Motorola.CodingExercise.Service.dll"]