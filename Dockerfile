# syntax=docker/dockerfile:1

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

# Copy toàn bộ mã nguồn vào container
COPY . /source
WORKDIR /source/API

ARG TARGETARCH

# Build ứng dụng .NET và publish vào /app
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish -a ${TARGETARCH/amd64/x64} --use-current-runtime --self-contained false -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app

# Copy ứng dụng đã publish từ build stage sang container cuối cùng
COPY --from=build /app .

# Copy file .db từ thư mục API sang /app/data
COPY /source/API/*.db /app/data/

# Đảm bảo thư mục /app/data có quyền ghi cho SQLite
RUN mkdir -p /app/data && chmod -R 777 /app/data

ENTRYPOINT ["dotnet", "API.dll"]
